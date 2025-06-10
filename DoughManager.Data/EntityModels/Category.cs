using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;

namespace DoughManager.Data.EntityModels;

public class Category : BaseEntity<int>
{

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
