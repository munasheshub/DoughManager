using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;

namespace DoughManager.Data.EntityModels;

public class Customer : BaseEntity<int>
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
}
