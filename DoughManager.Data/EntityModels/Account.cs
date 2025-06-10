using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;
using DoughManager.Data.Shared;

namespace DoughManager.Data.EntityModels;

public class Account : FullyAuditedEntity
{

    public string FullName { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public Role Role { get; set; }

    public bool IsActive { get; set; }
    public DateTime? ResetCodeExpTime { get; set; }
    public string? ResetCode { get; set; }

    public string? Code { get; set; }
    public DiscrepancyRecord? DiscrepancyRecord { get; set; }
}
    
