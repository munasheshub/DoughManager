using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;

namespace DoughManager.Data.EntityModels;

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DiscrepancyRecord : AuditedEntity
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public double Amount { get; set; }
    public string Notes { get; set; }
    public DiscrepancyType Type { get; set; }
    public ShiftType Shift { get; set; }
    public Account? User { get; set; }
    
}

public enum DiscrepancyType
{
    NoDiscrepancies,
    Shortage,
    Overage,
    Damage,
    Theft,
    Other
}

public enum ShiftType
{
    Morning,
    Afternoon
}
