﻿// Decompiled with JetBrains decompiler
// Type: DualSliderSideScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DualSliderSideScreen : SideScreenContent
{
  private IDualSliderControl target;
  public List<SliderSet> sliderSets;

  protected virtual void OnSpawn()
  {
    base.OnSpawn();
    for (int index = 0; index < this.sliderSets.Count; ++index)
      this.sliderSets[index].SetupSlider(index);
  }

  public override bool IsValidForTarget(GameObject target) => target.GetComponent<IDualSliderControl>() != null;

  public override void SetTarget(GameObject new_target)
  {
    if (Object.op_Equality((Object) new_target, (Object) null))
    {
      Debug.LogError((object) "Invalid gameObject received");
    }
    else
    {
      this.target = new_target.GetComponent<IDualSliderControl>();
      if (this.target == null)
      {
        Debug.LogError((object) "The gameObject received does not contain a Manual Generator component");
      }
      else
      {
        this.titleKey = this.target.SliderTitleKey;
        for (int index = 0; index < this.sliderSets.Count; ++index)
          this.sliderSets[index].SetTarget((ISliderControl) this.target);
      }
    }
  }
}