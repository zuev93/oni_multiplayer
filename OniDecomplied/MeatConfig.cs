﻿// Decompiled with JetBrains decompiler
// Type: MeatConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using STRINGS;
using UnityEngine;

public class MeatConfig : IEntityConfig
{
  public const string ID = "Meat";

  public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

  public GameObject CreatePrefab()
  {
    GameObject looseEntity = EntityTemplates.CreateLooseEntity("Meat", (string) ITEMS.FOOD.MEAT.NAME, (string) ITEMS.FOOD.MEAT.DESC, 1f, false, Assets.GetAnim(HashedString.op_Implicit("creaturemeat_kanim")), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
    EntityTemplates.ExtendEntityToFood(looseEntity, TUNING.FOOD.FOOD_TYPES.MEAT);
    return looseEntity;
  }

  public void OnPrefabInit(GameObject inst)
  {
  }

  public void OnSpawn(GameObject inst)
  {
  }
}