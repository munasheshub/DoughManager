// Decompiled with JetBrains decompiler
// Type: MoringaBakery.Api.Controllers.BaseController
// Assembly: MoringaBakery.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B2D6541-2FB5-408C-932B-3F249436BB09
// Assembly location: C:\Users\munas\OneDrive\Desktop\moringaBakery (2)\moringaBakery\MoringaBakery.Api.dll

#nullable enable
using DoughManager.Data.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;

[Controller]
public abstract class BaseController : ControllerBase
{
  public Account Account => (Account) this.HttpContext.Items[(object) nameof (Account)];
}
