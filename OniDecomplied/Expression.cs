﻿// Decompiled with JetBrains decompiler
// Type: Expression
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using System.Diagnostics;

[DebuggerDisplay("{face.hash} {priority}")]
public class Expression : Resource
{
  public Face face;
  public int priority;

  public Expression(string id, ResourceSet parent, Face face)
    : base(id, parent, (string) null)
  {
    this.face = face;
  }
}
