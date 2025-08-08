using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models
{
    public class CreditMetadata
    {
    }
    [MetadataType(typeof(CreditMetadata))]
    public partial class Credit
    {
        public Credit Create(CriteriaUser criteriaUser, DateTime datenow)
        {
            this.CriteriaUser = criteriaUser;
            this.IsDelete = false;
            this.CreateBy = "pon";
            this.CreateDate = datenow;
            this.UpdateBy = "pon";
            this.UpdateDate = datenow;
            return this;
        }

        public Credit Edit(CriteriaUser criteriaUser, DateTime datenow, AuctiondbContext context, Credit updateData)
        {
            this.CriteriaUser = criteriaUser;
            this.UpdateBy = "pon_Edit";
            this.UpdateDate = datenow;
            this.Credit1 = updateData.Credit1;
            // ... เพิ่มเติม property ที่ต้องการ map
            context.Credits.Update(this);
            return this;
        }
    }
}