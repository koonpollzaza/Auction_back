using System.ComponentModel.DataAnnotations;

namespace Auction_back.Models
{
    public class AuctionMetadata
    {
    }
    [MetadataType(typeof(AuctionMetadata))]
    public partial class Auction
    {
        public Auction Create(AuctiondbContext context)
        {
            DateTime datenow = DateTime.Now;
            this.IsDelete = false;
            CreateBy = "pon";
            CreateDate = datenow;
            UpdateBy = "pon";
            UpdateDate = datenow;

            if (this.AuctionCategories != null)
            {
                foreach (AuctionCategory cat in this.AuctionCategories)
                {
                    cat.Create(this, datenow);
                }
            }

            context.Auctions.Add(this);
            return this;
        }

        public Auction Edit(AuctiondbContext context, Auction updateData)
        {
            if (this.Id <= 0)
            {
                throw new InvalidOperationException("Cannot edit Auction without valid Id.");
            }

            // Map property ที่ต้องการแก้ไขจาก updateData มายัง this
            this.MetaAuctionTypeId = updateData.MetaAuctionTypeId;
            this.AuctionName = updateData.AuctionName;
            this.Status = updateData.Status;
            this.Description = updateData.Description;
            this.StartDate = updateData.StartDate;
            this.EndDate = updateData.EndDate;
            // ไม่ควร map Id, CreateBy, CreateDate, IsDelete, UpdateBy, UpdateDate ตรง ๆ
            // ไม่ควร map AuctionCategories ตรง ๆ (ใช้ cascade ด้านล่าง)

            DateTime datenow = DateTime.Now;
            this.UpdateBy = "pon_Edit";
            this.UpdateDate = datenow;

            if (this.AuctionCategories != null && updateData.AuctionCategories != null)
            {
                // Get IDs of categories that should remain
                var updateCategoryIds = updateData.AuctionCategories.Where(c => c.Id > 0).Select(c => c.Id).ToList();

                // Soft delete categories that are not in the update request
                foreach (AuctionCategory existingCat in this.AuctionCategories.Where(c => c.Id > 0 && !updateCategoryIds.Contains(c.Id)))
                {
                    existingCat.IsDelete = true;
                    existingCat.UpdateBy = "pon_Edit";
                    existingCat.UpdateDate = datenow;
                    context.AuctionCategories.Update(existingCat);
                }

                // Update existing categories
                foreach (AuctionCategory cat in this.AuctionCategories.Where(c => !c.IsDelete))
                {
                    AuctionCategory? updateCat = updateData.AuctionCategories.FirstOrDefault(c => c.Id == cat.Id);
                    if (cat.Id > 0 && updateCat != null)
                    {
                        cat.Edit(this, datenow, context, updateCat);
                    }
                }

                // Add new AuctionCategory (id == 0)
                foreach (AuctionCategory newCat in updateData.AuctionCategories.Where(c => c.Id == 0))
                {
                    newCat.AuctionId = this.Id;
                    newCat.Create(this, datenow);
                    context.AuctionCategories.Add(newCat);
                }
            }

            context.Auctions.Update(this);
            return this;
        }

        public Auction Delete(AuctiondbContext context, Auction auction)
        {
            if (this.Id <= 0)
            {
                throw new InvalidOperationException("Cannot delete Auction without valid Id.");
            }

            DateTime datenow = DateTime.Now;

            auction.IsDelete = true;
            auction.UpdateBy = "pon_Delete";
            auction.UpdateDate = datenow;

            foreach (var category in auction.AuctionCategories)
            {
                category.IsDelete = true;
                category.UpdateBy = "pon_Delete";
                category.UpdateDate = datenow;

                foreach (var criterion in category.Criteria)
                {
                    criterion.IsDelete = true;
                    criterion.UpdateBy = "pon_Delete";
                    criterion.UpdateDate = datenow;

                    foreach (var user in criterion.CriteriaUsers)
                    {
                        user.IsDelete = true;
                        user.UpdateBy = "pon_Delete";
                        user.UpdateDate = datenow;

                        foreach (var credit in user.Credits)
                        {
                            credit.IsDelete = true;
                            credit.UpdateBy = "pon_Delete";
                            credit.UpdateDate = datenow;
                        }

                        foreach (var debit in user.Debits)
                        {
                            debit.IsDelete = true;
                            debit.UpdateBy = "pon_Delete";
                            debit.UpdateDate = datenow;
                        }

                        foreach (var marginal in user.MarginalCredits)
                        {
                            marginal.IsDelete = true;
                            marginal.UpdateBy = "pon_Delete";
                            marginal.UpdateDate = datenow;
                        }

                        foreach (var amount in user.AuctionAmounts)
                        {
                            amount.IsDelete = true;
                            amount.UpdateBy = "pon_Delete";
                            amount.UpdateDate = datenow;
                        }
                    }
                }
            }

            this.IsDelete = true;
            this.UpdateBy = "pon_Delete";
            this.UpdateDate = datenow;

            context.Auctions.Update(this);
            return this;
        }
    }
}