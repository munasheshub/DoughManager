
using DoughManager.Data.EntityModels;
using System;


namespace DoughManager.Data.BaseEntityModels
{
    public class FullyAuditedEntity : AuditedEntity
    {
      public int? LastModifierId { get; set; }
      public Account? LastModifier { get; set; }

      public DateTime? LastModificationTime { get; set; }

      public void PrepareEntityForUpdate(Account account)
      {
        this.LastModificationTime = new DateTime?(DateTime.Now);
        this.LastModifierId = new int?(account.Id);
      }
    }
}
