using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Steamworks;

namespace MultiplayerMod.steam
{

    public class Server : KMonoBehaviour
    {

        public static readonly SteamNetworkingConfigValue_t BufferSizeConfig = new()
        {
            m_eValue = ESteamNetworkingConfigValue.k_ESteamNetworkingConfig_SendBufferSize,
            m_eDataType = ESteamNetworkingConfigDataType.k_ESteamNetworkingConfig_Int32,
            m_val = new SteamNetworkingConfigValue_t.OptionValue
            {
                m_int32 = 10 * 1024 * 1024
            }
        };

        private readonly Dictionary<CSteamID, HSteamNetConnection> _clients = new();
        private HSteamNetPollGroup _hNetPollGroup;

        private bool _isServerStarted;
        public CSteamID SteamId => SteamGameServer.GetSteamID();

        private CallResult<LobbyCreated_t> lobbyCreatedCallback;
        public CSteamID lobbyId;
        private Callback<SteamServersConnected_t> steamServersConnected;
        private Callback<SteamNetConnectionStatusChangedCallback_t> netConnectionStatusChanged;

        private void Update()
        {
            if (!_isServerStarted)
                return;

            // Run Steam client callbacks
            GameServer.RunCallbacks();
            SteamGameServerNetworkingSockets.RunCallbacks();
            ReceiveNetworkData();
        }

        private void OnEnable()
        {
            Debug.Log("Multiplayer.RestartAppIfNecessary");

            SteamAPI.RestartAppIfNecessary(new AppId_t(457140));
            Debug.Log("Multiplayer.Server.Init");
            SteamAPI.Init();
            Debug.Log("Multiplayer.Server.Done");

            SteamNetworkingUtils.InitRelayNetworkAccess();

            if (!SteamManager.Initialized)
                throw new Exception("Steam manager is not initialized");

            steamServersConnected = Callback<SteamServersConnected_t>.CreateGameServer(_ => {
                Debug.Log($"[Server] GameServer connected to Steam: {SteamId}");
                SteamGameServer.SetMaxPlayerCount(4);
                SteamGameServer.SetPasswordProtected(false);
                SteamGameServer.SetServerName($"{SteamFriends.GetPersonaName()}'s game");
                Debug.Log("[Server] Invoke ServerCreated");
                var steamId = SteamId;
                SteamMatchmaking.SetLobbyGameServer(lobbyId, 0, 0, steamId);
                Debug.Log($"[Server] SetLobbyData {lobbyId} -> {steamId}");
                ServerCreated?.Invoke();
            });
            netConnectionStatusChanged = Callback<SteamNetConnectionStatusChangedCallback_t>.CreateGameServer(Steam_HandleIncomingConnection);
        }

        public event System.Action ServerCreated;
        public event Action<CSteamID> ClientJoined;
        public event Action<CSteamID, SerializedMessage.TypedMessage> OnCommandReceived;

        public void BroadcastCommand(Command command, object payload = null)
        {
            BroadcastCommand(null, command, payload);
        }

        public void BroadcastCommand(CSteamID? initiatorId, Command command, object payload = null)
        {
            foreach (var (cSteamID, hSteamNetConnection) in _clients)
            {
                if (cSteamID == initiatorId) continue;

                using var message = new SerializedMessage(command, payload);
                var result = SteamGameServerNetworkingSockets.SendMessageToConnection(
                    hSteamNetConnection,
                    message.IntPtr,
                    message.Size,
                    Steamworks.Constants.k_nSteamNetworkingSend_Reliable,
                    out var messageOut
                );
                if (result != EResult.k_EResultOK && messageOut == 0)
                {
                    Debug.Log($"Failed ({result}) to send message {initiatorId}. Message out is {messageOut}.");
                    Debug.Log(
                        $"Args are {hSteamNetConnection} {message.IntPtr} {message.Size} {Steamworks.Constants.k_nSteamNetworkingSend_Reliable}"
                    );
                }
            }
        }

        protected override void OnSpawn()
        {
            Debug.Log("Multiplayer server startup");
            Debug.Log(SteamFriends.GetPersonaName());

            if (!GameServer.Init(0, 27020, 27015, EServerMode.eServerModeNoAuthentication, "0.0.1.0"))
            {
                // SteamGameServer.Init() failed, log an error and return
                Debug.LogError("SteamGameServer.Init() failed.");
                return;
            }

            SteamGameServer.SetModDir("OxygenNotIncluded");
            SteamGameServer.SetProduct("OxygenNotIncluded Multiplayer");
            SteamGameServer.SetGameDescription("OxygenNotIncluded Multiplayer");

            SteamGameServer.LogOnAnonymous();
            SteamNetworkingUtils.InitRelayNetworkAccess();
            SteamGameServer.SetAdvertiseServerActive(true);

            SteamGameServerNetworkingSockets.CreateListenSocketP2P(0, 1, new[] { BufferSizeConfig });
            _hNetPollGroup = SteamGameServerNetworkingSockets.CreatePollGroup();

            var lobbyResult = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
            lobbyCreatedCallback = CallResult<LobbyCreated_t>.Create((t, _) => {
                lobbyId = new CSteamID(t.m_ulSteamIDLobby);
                Debug.Log($"[Server] CreateLobby result: {t.m_eResult}");
            });
            lobbyCreatedCallback.Set(lobbyResult);

            _isServerStarted = true;
        }

        protected override void OnCleanUp()
        {
            Debug.Log("Server.OnDestroy");
            GameServer.Shutdown();
            _isServerStarted = false;
        }

        private void Steam_HandleIncomingConnection(SteamNetConnectionStatusChangedCallback_t pCallback)
        {
            // Connection handle
            var hConn = pCallback.m_hConn;

            // Full connection info
            var info = pCallback.m_info;

            // Previous state.  (Current state is in m_info.m_eState)
            var eOldState = pCallback.m_eOldState;
            var steamID = pCallback.m_info.m_identityRemote.GetSteamID();

            // Check if a client has connected
            if (info.m_hListenSocket != HSteamListenSocket.Invalid &&
                eOldState == ESteamNetworkingConnectionState.k_ESteamNetworkingConnectionState_None &&
                info.m_eState == ESteamNetworkingConnectionState.k_ESteamNetworkingConnectionState_Connecting)
            {
                var res = SteamGameServerNetworkingSockets.AcceptConnection(hConn);
                if (res != EResult.k_EResultOK)
                {
                    Debug.Log($"AcceptConnection returned {res}");
                    SteamGameServerNetworkingSockets.CloseConnection(
                        hConn,
                        (int)ESteamNetConnectionEnd.k_ESteamNetConnectionEnd_AppException_Generic,
                        "Failed to accept connection",
                        false
                    );
                    return;
                }

                SteamGameServerNetworkingSockets.SetConnectionPollGroup(hConn, _hNetPollGroup);

                if (_clients.ContainsKey(steamID))
                    _clients.Remove(steamID);
                _clients.Add(steamID, hConn);
                Debug.Log($"Connection accepted from {steamID}");
                ClientJoined?.Invoke(steamID);
            } else if ((eOldState ==
                        ESteamNetworkingConnectionState.k_ESteamNetworkingConnectionState_Connecting ||
                        eOldState == ESteamNetworkingConnectionState
                            .k_ESteamNetworkingConnectionState_Connected) &&
                       info.m_eState == ESteamNetworkingConnectionState
                           .k_ESteamNetworkingConnectionState_ClosedByPeer)
            {
                // Handle disconnecting a client
                _clients.Remove(steamID);
                Debug.Log($"Connection closed from {steamID}");
            }
        }

        private void ReceiveNetworkData()
        {
            var messages = new IntPtr[128];
            var numMessages =
                SteamGameServerNetworkingSockets.ReceiveMessagesOnPollGroup(_hNetPollGroup, messages, 128);
            for (var idxMsg = 0; idxMsg < numMessages; idxMsg++)
            {
                var message =
                    (SteamNetworkingMessage_t)Marshal.PtrToStructure(
                        messages[idxMsg],
                        typeof(SteamNetworkingMessage_t)
                    );

                var msg = SerializedMessage.TypedMessage.DeserializeMessage(
                    message.m_pData,
                    message.m_cbSize
                );

                OnCommandReceived?.Invoke(message.m_identityPeer.GetSteamID(), msg);

                SteamNetworkingMessage_t.Release(messages[idxMsg]);
            }
        }
    }

}
