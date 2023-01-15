﻿// Decompiled with JetBrains decompiler
// Type: GasSourceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("KMonoBehaviour/scripts/GasSourceManager")]
public class GasSourceManager : KMonoBehaviour, IChunkManager
{
  public static GasSourceManager Instance;

  protected virtual void OnPrefabInit() => GasSourceManager.Instance = this;

  public SubstanceChunk CreateChunk(
    SimHashes element_id,
    float mass,
    float temperature,
    byte diseaseIdx,
    int diseaseCount,
    Vector3 position)
  {
    return this.CreateChunk(ElementLoader.FindElementByHash(element_id), mass, temperature, diseaseIdx, diseaseCount, position);
  }

  public SubstanceChunk CreateChunk(
    Element element,
    float mass,
    float temperature,
    byte diseaseIdx,
    int diseaseCount,
    Vector3 position)
  {
    return GeneratedOre.CreateChunk(element, mass, temperature, diseaseIdx, diseaseCount, position);
  }
}