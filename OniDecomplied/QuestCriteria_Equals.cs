﻿// Decompiled with JetBrains decompiler
// Type: QuestCriteria_Equals
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class QuestCriteria_Equals : QuestCriteria
{
  public QuestCriteria_Equals(
    Tag id,
    float[] targetValues,
    int requiredCount = 1,
    HashSet<Tag> acceptedTags = null,
    QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
    : base(id, targetValues, requiredCount, acceptedTags, flags)
  {
  }

  protected override bool ValueSatisfies_Internal(float current, float target) => (double) Mathf.Abs(target - current) <= (double) Mathf.Epsilon;
}