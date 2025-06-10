using System;
using System.Collections.Generic;

namespace DoughManager.Data.Migrations;

public class UserState
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string State { get; set; } = null!;

    public DateTime? LastUpdated { get; set; }
}
