﻿// Decompiled with JetBrains decompiler
// Type: LiquidFuelTankClusterConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using System.Collections.Generic;
using TUNING;
using UnityEngine;

public class LiquidFuelTankClusterConfig : IBuildingConfig
{
  public const string ID = "LiquidFuelTankCluster";
  public const float FuelCapacity = 900f;

  public override string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

  public override BuildingDef CreateBuildingDef()
  {
    float[] fuelTankDryMass = BUILDINGS.ROCKETRY_MASS_KG.FUEL_TANK_DRY_MASS;
    string[] construction_materials = new string[1]
    {
      SimHashes.Steel.ToString()
    };
    EffectorValues tieR2 = NOISE_POLLUTION.NOISY.TIER2;
    EffectorValues none = BUILDINGS.DECOR.NONE;
    EffectorValues noise = tieR2;
    BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("LiquidFuelTankCluster", 5, 5, "rocket_cluster_liquid_fuel_tank_kanim", 1000, 60f, fuelTankDryMass, construction_materials, 9999f, BuildLocationRule.Anywhere, none, noise);
    BuildingTemplates.CreateRocketBuildingDef(buildingDef);
    buildingDef.SceneLayer = Grid.SceneLayer.Building;
    buildingDef.OverheatTemperature = 2273.15f;
    buildingDef.Floodable = false;
    buildingDef.AttachmentSlotTag = GameTags.Rocket;
    buildingDef.ObjectLayer = ObjectLayer.Building;
    buildingDef.UtilityInputOffset = new CellOffset(2, 3);
    buildingDef.InputConduitType = ConduitType.Liquid;
    buildingDef.RequiresPowerInput = false;
    buildingDef.attachablePosition = new CellOffset(0, 0);
    buildingDef.CanMove = true;
    buildingDef.Cancellable = false;
    buildingDef.ShowInBuildMenu = false;
    return buildingDef;
  }

  public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
  {
    BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof (RequiresFoundation), prefab_tag);
    go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[1]
    {
      new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, (AttachableBuilding) null)
    };
    go.AddOrGet<LoopingSounds>();
    go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
  }

  public override void DoPostConfigureComplete(GameObject go)
  {
    Storage storage = go.AddOrGet<Storage>();
    storage.capacityKg = BUILDINGS.ROCKETRY_MASS_KG.FUEL_TANK_WET_MASS[0];
    storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>()
    {
      Storage.StoredItemModifier.Hide,
      Storage.StoredItemModifier.Seal,
      Storage.StoredItemModifier.Insulate
    });
    FuelTank fuelTank = go.AddOrGet<FuelTank>();
    fuelTank.consumeFuelOnLand = !DlcManager.FeatureClusterSpaceEnabled();
    fuelTank.storage = storage;
    fuelTank.physicalFuelCapacity = storage.capacityKg;
    go.AddOrGet<CopyBuildingSettings>();
    go.AddOrGet<DropToUserCapacity>();
    ManualDeliveryKG manualDeliveryKg = go.AddOrGet<ManualDeliveryKG>();
    manualDeliveryKg.SetStorage(storage);
    manualDeliveryKg.refillMass = storage.capacityKg;
    manualDeliveryKg.capacity = storage.capacityKg;
    manualDeliveryKg.operationalRequirement = Operational.State.None;
    manualDeliveryKg.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
    ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
    conduitConsumer.conduitType = ConduitType.Liquid;
    conduitConsumer.consumptionRate = 10f;
    conduitConsumer.capacityTag = GameTags.Liquid;
    conduitConsumer.capacityKG = storage.capacityKg;
    conduitConsumer.forceAlwaysSatisfied = true;
    conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
    BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, (string) null, ROCKETRY.BURDEN.MODERATE_PLUS);
    storage.showUnreachableStatus = false;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    go.GetComponent<KPrefabID>().prefabInitFn += LiquidFuelTankClusterConfig.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (LiquidFuelTankClusterConfig.\u003C\u003Ec.\u003C\u003E9__5_0 = new KPrefabID.PrefabFn((object) LiquidFuelTankClusterConfig.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CDoPostConfigureComplete\u003Eb__5_0)));
  }
}
