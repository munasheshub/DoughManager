
using DoughManager.Data.EntityModels;
using System;


namespace DoughManager.Data.BaseEntityModels
{
    public class AuditedEntity : BaseEntity<int>
    {
      public int? CreatorId { get; set; }

      public Account? Creator { get; set; }
      public Account? Deleter { get; set; }

      public DateTime? CreationTime { get; set; }

      public int? DeleterId { get; set; }

      public DateTime? DeletionTime { get; set; }

      public bool IsDeleted { get; set; }

      public void PrepareEntityForCreation(Account account)
      {
        this.CreationTime = new DateTime?(DateTime.UtcNow);
        this.CreatorId = new int?(account.Id);
      }

      public void PrepareEntityForDeletion(Account account)
      {
        this.DeletionTime = new DateTime?(DateTime.UtcNow);
        this.DeleterId = new int?(account.Id);
      }
    }
}
