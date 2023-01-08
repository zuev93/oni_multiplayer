﻿// Decompiled with JetBrains decompiler
// Type: UpdateRocketSpeedParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine;

internal class UpdateRocketSpeedParameter : LoopingSoundParameterUpdater
{
  private List<UpdateRocketSpeedParameter.Entry> entries = new List<UpdateRocketSpeedParameter.Entry>();

  public UpdateRocketSpeedParameter()
    : base(HashedString.op_Implicit("rocketSpeed"))
  {
  }

  public override void Add(LoopingSoundParameterUpdater.Sound sound) => this.entries.Add(new UpdateRocketSpeedParameter.Entry()
  {
    rocketModule = ((Component) sound.transform).GetComponent<RocketModule>(),
    ev = sound.ev,
    parameterId = ((SoundDescription) ref sound.description).GetParameterId(this.parameter)
  });

  public override void Update(float dt)
  {
    foreach (UpdateRocketSpeedParameter.Entry entry in this.entries)
    {
      if (!Object.op_Equality((Object) entry.rocketModule, (Object) null))
      {
        LaunchConditionManager conditionManager = entry.rocketModule.conditionManager;
        if (!Object.op_Equality((Object) conditionManager, (Object) null))
        {
          ILaunchableRocket component = ((Component) conditionManager).GetComponent<ILaunchableRocket>();
          if (component != null)
          {
            EventInstance ev = entry.ev;
            ((EventInstance) ref ev).setParameterByID(entry.parameterId, component.rocketSpeed, false);
          }
        }
      }
    }
  }

  public override void Remove(LoopingSoundParameterUpdater.Sound sound)
  {
    for (int index = 0; index < this.entries.Count; ++index)
    {
      if (this.entries[index].ev.handle == sound.ev.handle)
      {
        this.entries.RemoveAt(index);
        break;
      }
    }
  }

  private struct Entry
  {
    public RocketModule rocketModule;
    public EventInstance ev;
    public PARAMETER_ID parameterId;
  }
}