using System.ComponentModel.DataAnnotations;

namespace Auction_back.Models
{
    public class AuctionCategoryMetadata
    {
    }
    [MetadataType(typeof(AuctionCategoryMetadata))]
    public partial class AuctionCategory
    {
        public AuctionCategory Create(Auction auction, DateTime datenow)
        {
            this.Auction = auction;
            this.IsDelete = false;
            this.CreateBy = "pon";
            this.CreateDate = datenow;
            this.UpdateBy = "pon";
            this.UpdateDate = datenow;

            if (this.Criteria != null)
            {
                foreach (Criterion criteria in this.Criteria)
                {
                    criteria.Create(this, datenow);
                }
            }
            return this;
        }

        public AuctionCategory Edit(Auction auction, DateTime datenow, AuctiondbContext context, AuctionCategory updateData)
        {
            this.Auction = auction;
            this.UpdateBy = "pon_Edit";
            this.UpdateDate = datenow;
            this.MetaCategoryId = updateData.MetaCategoryId;
            // ... เพิ่มเติม property ที่ต้องการ map

            if (this.Criteria != null && updateData.Criteria != null)
            {
                // Get IDs of criteria that should remain
                var updateCriteriaIds = updateData.Criteria.Where(c => c.Id > 0).Select(c => c.Id).ToList();

                // DON'T delete criteria records - just let them handle their own IsCheck logic
                // Instead, find criteria that are not in updateData and set their IsCheck to false
                foreach (Criterion existingCriteria in this.Criteria.Where(c => c.Id > 0 && !updateCriteriaIds.Contains(c.Id)))
                {
                    // Create a dummy updateData with IsCheck = false for this criteria
                    var dummyUpdateCriteria = new Criterion
                    {
                        Id = existingCriteria.Id,
                        IsCheck = false,
                        MetaCriteriaTypeId = existingCriteria.MetaCriteriaTypeId
                    };

                    // Let the criteria handle its own uncheck logic
                    existingCriteria.Edit(this, datenow, context, dummyUpdateCriteria);
                }

                // Update existing criteria
                foreach (Criterion criteria in this.Criteria.Where(c => !c.IsDelete))
                {
                    Criterion? updateCriteria = updateData.Criteria.FirstOrDefault(c => c.Id == criteria.Id);
                    if (criteria.Id > 0 && updateCriteria != null)
                    {
                        criteria.Edit(this, datenow, context, updateCriteria);
                    }
                }

                // Add new Criteria (id == 0)
                foreach (Criterion newCriteria in updateData.Criteria.Where(c => c.Id == 0))
                {
                    newCriteria.AuctionCategoryId = this.Id;
                    newCriteria.Create(this, datenow);
                    context.Criteria.Add(newCriteria);
                }
            }
            context.AuctionCategories.Update(this);
            return this;
        }
    }
}