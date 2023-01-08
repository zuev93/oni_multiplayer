﻿// Decompiled with JetBrains decompiler
// Type: LightBugCrystalBabyConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using STRINGS;
using UnityEngine;

public class LightBugCrystalBabyConfig : IEntityConfig
{
  public const string ID = "LightBugCrystalBaby";

  public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

  public GameObject CreatePrefab()
  {
    GameObject lightBug = LightBugCrystalConfig.CreateLightBug("LightBugCrystalBaby", (string) CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.BABY.NAME, (string) CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.BABY.DESC, "baby_lightbug_kanim", true);
    EntityTemplates.ExtendEntityToBeingABaby(lightBug, Tag.op_Implicit("LightBugCrystal"));
    return lightBug;
  }

  public void OnPrefabInit(GameObject prefab)
  {
  }

  public void OnSpawn(GameObject inst)
  {
  }
}
