﻿// Decompiled with JetBrains decompiler
// Type: CodexMessageDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D13CBB0B-55A9-4EF0-9BB5-5C2436A6B8EE
// Assembly location: D:\dev\OniMod\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class CodexMessageDialog : MessageDialog
{
  [SerializeField]
  private LocText description;
  private CodexUnlockedMessage message;

  public override bool CanDisplay(Message message) => typeof (CodexUnlockedMessage).IsAssignableFrom(message.GetType());

  public override void SetMessage(Message base_message)
  {
    this.message = (CodexUnlockedMessage) base_message;
    ((TMP_Text) this.description).text = this.message.GetMessageBody();
  }

  public override void OnClickAction()
  {
  }

  protected virtual void OnCleanUp()
  {
    base.OnCleanUp();
    this.message.OnCleanUp();
  }
}
