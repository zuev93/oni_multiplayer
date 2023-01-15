﻿// Decompiled with JetBrains decompiler
// Type: ConduitSecondaryInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("KMonoBehaviour/scripts/ConduitSecondaryInput")]
public class ConduitSecondaryInput : KMonoBehaviour, ISecondaryInput
{
  [SerializeField]
  public ConduitPortInfo portInfo;

  public bool HasSecondaryConduitType(ConduitType type) => this.portInfo.conduitType == type;

  public CellOffset GetSecondaryConduitOffset(ConduitType type) => this.portInfo.conduitType == type ? this.portInfo.offset : CellOffset.none;
}