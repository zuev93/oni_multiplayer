﻿// Decompiled with JetBrains decompiler
// Type: UpdateDistanceToImpactParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine;

internal class UpdateDistanceToImpactParameter : LoopingSoundParameterUpdater
{
  private List<UpdateDistanceToImpactParameter.Entry> entries = new List<UpdateDistanceToImpactParameter.Entry>();

  public UpdateDistanceToImpactParameter()
    : base(HashedString.op_Implicit("distanceToImpact"))
  {
  }

  public override void Add(LoopingSoundParameterUpdater.Sound sound) => this.entries.Add(new UpdateDistanceToImpactParameter.Entry()
  {
    comet = ((Component) sound.transform).GetComponent<Comet>(),
    ev = sound.ev,
    parameterId = ((SoundDescription) ref sound.description).GetParameterId(this.parameter)
  });

  public override void Update(float dt)
  {
    foreach (UpdateDistanceToImpactParameter.Entry entry in this.entries)
    {
      if (!Object.op_Equality((Object) entry.comet, (Object) null))
      {
        float soundDistance = entry.comet.GetSoundDistance();
        EventInstance ev = entry.ev;
        ((EventInstance) ref ev).setParameterByID(entry.parameterId, soundDistance, false);
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
    public Comet comet;
    public EventInstance ev;
    public PARAMETER_ID parameterId;
  }
}
