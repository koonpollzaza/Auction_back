using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models
{
    public class DebitMetadata
    {
    }
    [MetadataType(typeof(DebitMetadata))]
    public partial class Debit
    {
        public Debit Create(CriteriaUser criteriaUser, DateTime datenow)
        {
            this.CriteriaUser = criteriaUser;
            this.IsDelete = false;
            this.CreateBy = "pon";
            this.CreateDate = datenow;
            this.UpdateBy = "pon";
            this.UpdateDate = datenow;
            return this;
        }

        public Debit Edit(CriteriaUser criteriaUser, DateTime datenow, AuctiondbContext context, Debit updateData)
        {
            this.CriteriaUser = criteriaUser;
            this.UpdateBy = "pon_Edit";
            this.UpdateDate = datenow;
            this.MetaMoneyTypeId = updateData.MetaMoneyTypeId;
            this.Cash = updateData.Cash;
            // ... เพิ่มเติม property ที่ต้องการ map
            context.Debits.Update(this);
            return this;
        }
    }
}