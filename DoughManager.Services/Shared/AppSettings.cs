// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Shared.AppSettings
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

#nullable enable
using DoughManager.Services.Shared;

namespace DoughManager.Services.Shared;

public class AppSettings
{
  public string Secret { get; set; } = string.Empty;
  public string EmailFrom { get; set; }
  public string SmtpHost { get; set; }
  public string SmtpPort { get; set; }
  public string SmtpUser { get; set; }
  public string SmtpPass { get; set; }
}
