﻿// Decompiled with JetBrains decompiler
// Type: DeployingPioneerLanderFXConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using UnityEngine;

public class DeployingPioneerLanderFXConfig : IEntityConfig
{
  public const string ID = "DeployingPioneerLanderFX";

  public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

  public GameObject CreatePrefab()
  {
    GameObject entity = EntityTemplates.CreateEntity("DeployingPioneerLanderFX", "DeployingPioneerLanderFX", false);
    ClusterFXEntity clusterFxEntity = entity.AddOrGet<ClusterFXEntity>();
    clusterFxEntity.kAnimName = "pioneer01_kanim";
    clusterFxEntity.animName = "landing";
    clusterFxEntity.animPlayMode = (KAnim.PlayMode) 0;
    return entity;
  }

  public void OnPrefabInit(GameObject inst)
  {
  }

  public void OnSpawn(GameObject inst)
  {
  }
}