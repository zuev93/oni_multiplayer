﻿// Decompiled with JetBrains decompiler
// Type: CameraFollowHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("KMonoBehaviour/scripts/CameraFollowHelper")]
public class CameraFollowHelper : KMonoBehaviour
{
  private void LateUpdate()
  {
    if (!Object.op_Inequality((Object) CameraController.Instance, (Object) null))
      return;
    CameraController.Instance.UpdateFollowTarget();
  }
}
