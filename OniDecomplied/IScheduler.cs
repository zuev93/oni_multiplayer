﻿// Decompiled with JetBrains decompiler
// Type: IScheduler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using System;

public interface IScheduler
{
  SchedulerHandle Schedule(
    string name,
    float time,
    Action<object> callback,
    object callback_data = null,
    SchedulerGroup group = null);
}