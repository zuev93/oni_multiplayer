﻿// Decompiled with JetBrains decompiler
// Type: LogicPressureSensorGasConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using System.Collections.Generic;
using TUNING;
using UnityEngine;

public class LogicPressureSensorGasConfig : IBuildingConfig
{
  public static string ID = "LogicPressureSensorGas";

  public override BuildingDef CreateBuildingDef()
  {
    string id = LogicPressureSensorGasConfig.ID;
    float[] tieR0_1 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
    string[] refinedMetals = MATERIALS.REFINED_METALS;
    EffectorValues none = NOISE_POLLUTION.NONE;
    EffectorValues tieR0_2 = TUNING.BUILDINGS.DECOR.PENALTY.TIER0;
    EffectorValues noise = none;
    BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, 1, 1, "switchgaspressure_kanim", 30, 30f, tieR0_1, refinedMetals, 1600f, BuildLocationRule.Anywhere, tieR0_2, noise);
    buildingDef.Overheatable = false;
    buildingDef.Floodable = false;
    buildingDef.Entombable = false;
    buildingDef.ViewMode = OverlayModes.Logic.ID;
    buildingDef.AudioCategory = "Metal";
    buildingDef.SceneLayer = Grid.SceneLayer.Building;
    buildingDef.AlwaysOperational = true;
    buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
    {
      LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), (string) STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT, (string) STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT_ACTIVE, (string) STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT_INACTIVE, true)
    };
    SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
    SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
    GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicPressureSensorGasConfig.ID);
    return buildingDef;
  }

  public override void DoPostConfigureComplete(GameObject go)
  {
    LogicPressureSensor logicPressureSensor = go.AddOrGet<LogicPressureSensor>();
    logicPressureSensor.rangeMin = 0.0f;
    logicPressureSensor.rangeMax = 20f;
    logicPressureSensor.Threshold = 1f;
    logicPressureSensor.ActivateAboveThreshold = false;
    logicPressureSensor.manuallyControlled = false;
    logicPressureSensor.desiredState = Element.State.Gas;
    go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
  }
}