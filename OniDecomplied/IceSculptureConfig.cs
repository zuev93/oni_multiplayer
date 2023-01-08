﻿// Decompiled with JetBrains decompiler
// Type: IceSculptureConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using TUNING;
using UnityEngine;

public class IceSculptureConfig : IBuildingConfig
{
  public const string ID = "IceSculpture";

  public override BuildingDef CreateBuildingDef()
  {
    float[] tieR4 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
    string[] construction_materials = new string[1]{ "Ice" };
    EffectorValues none = NOISE_POLLUTION.NONE;
    EffectorValues decor = new EffectorValues()
    {
      amount = 20,
      radius = 8
    };
    EffectorValues noise = none;
    BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("IceSculpture", 2, 2, "icesculpture_kanim", 10, 120f, tieR4, construction_materials, 273.15f, BuildLocationRule.OnFloor, decor, noise);
    buildingDef.Floodable = false;
    buildingDef.Overheatable = false;
    buildingDef.AudioCategory = "Metal";
    buildingDef.BaseTimeUntilRepair = -1f;
    buildingDef.ViewMode = OverlayModes.Decor.ID;
    buildingDef.DefaultAnimState = "slab";
    buildingDef.PermittedRotations = PermittedRotations.FlipH;
    return buildingDef;
  }

  public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
  {
    go.AddOrGet<BuildingComplete>().isArtable = true;
    go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
  }

  public override void DoPostConfigureComplete(GameObject go) => go.AddComponent<Sculpture>().defaultAnimName = "slab";
}