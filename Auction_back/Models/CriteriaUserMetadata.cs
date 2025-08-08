using System.ComponentModel.DataAnnotations;

namespace Auction_back.Models
{
    public class CriteriaUserMetadata
    {
    }
    [MetadataType(typeof(CriteriaUserMetadata))]
    public partial class CriteriaUser
    {
        public CriteriaUser Create(Criterion criterion, DateTime datenow)
        {
            this.Criteria = criterion;
            this.IsDelete = false;
            this.CreateBy = "pon";
            this.CreateDate = datenow;
            this.UpdateBy = "pon";
            this.UpdateDate = datenow;

            if (this.Credits != null)
            {
                foreach (Credit credit in this.Credits)
                {
                    credit.Create(this, datenow);
                }
            }
            if (this.MarginalCredits != null)
            {
                foreach (MarginalCredit marginalCredit in this.MarginalCredits)
                {
                    marginalCredit.Create(this, datenow);
                }
            }
            if (this.Debits != null)
            {
                foreach (Debit debit in this.Debits)
                {
                    debit.Create(this, datenow);
                }
            }
            if (this.AuctionAmounts != null)
            {
                foreach (AuctionAmount auctionAmount in this.AuctionAmounts)
                {
                    auctionAmount.Create(this, datenow);
                }
            }
            return this;
        }

        public CriteriaUser Edit(Criterion criterion, DateTime datenow, AuctiondbContext context, CriteriaUser updateData)
        {
            this.Criteria = criterion;
            this.UpdateBy = "pon_Edit";
            this.UpdateDate = datenow;
            this.MetaUserId = updateData.MetaUserId;
            // ... เพิ่มเติม property ที่ต้องการ map

            // Credits
            if (updateData.Credits != null)
            {
                // Get IDs of credits that should remain
                var updateCreditIds = updateData.Credits.Where(c => c.Id > 0).Select(c => c.Id).ToList();

                // Soft delete credits that are not in the update request
                foreach (Credit existingCredit in this.Credits.Where(c => c.Id > 0 && !updateCreditIds.Contains(c.Id)))
                {
                    existingCredit.IsDelete = true;
                    existingCredit.UpdateBy = "Edit";
                    existingCredit.UpdateDate = datenow;
                    context.Credits.Update(existingCredit);
                }

                // Update or create credits
                foreach (Credit updateCredit in updateData.Credits)
                {
                    if (updateCredit.Id > 0)
                    {
                        Credit? existingCredit = this.Credits.FirstOrDefault(c => c.Id == updateCredit.Id && !c.IsDelete);
                        if (existingCredit != null)
                        {
                            existingCredit.Edit(this, datenow, context, updateCredit);
                        }
                    }
                    else
                    {
                        updateCredit.CriteriaUserId = this.Id;
                        updateCredit.Create(this, datenow);
                        context.Credits.Add(updateCredit);
                    }
                }
            }
            else
            {
                // If no credits in update, soft delete all existing credits
                foreach (Credit existingCredit in this.Credits.Where(c => !c.IsDelete))
                {
                    existingCredit.IsDelete = true;
                    existingCredit.UpdateBy = "Edit";
                    existingCredit.UpdateDate = datenow;
                    context.Credits.Update(existingCredit);
                }
            }

            // MarginalCredits
            if (updateData.MarginalCredits != null)
            {
                // Get IDs of marginal credits that should remain
                var updateMCIds = updateData.MarginalCredits.Where(mc => mc.Id > 0).Select(mc => mc.Id).ToList();

                // Soft delete marginal credits that are not in the update request
                foreach (MarginalCredit existingMC in this.MarginalCredits.Where(mc => mc.Id > 0 && !updateMCIds.Contains(mc.Id)))
                {
                    existingMC.IsDelete = true;
                    existingMC.UpdateBy = "Edit";
                    existingMC.UpdateDate = datenow;
                    context.MarginalCredits.Update(existingMC);
                }

                // Update or create marginal credits
                foreach (MarginalCredit updateMC in updateData.MarginalCredits)
                {
                    if (updateMC.Id > 0)
                    {
                        MarginalCredit? existingMC = this.MarginalCredits.FirstOrDefault(mc => mc.Id == updateMC.Id && !mc.IsDelete);
                        if (existingMC != null)
                        {
                            existingMC.Edit(this, datenow, context, updateMC);
                        }
                    }
                    else
                    {
                        updateMC.CriteriaUserId = this.Id;
                        updateMC.Create(this, datenow);
                        context.MarginalCredits.Add(updateMC);
                    }
                }
            }
            else
            {
                // If no marginal credits in update, soft delete all existing marginal credits
                foreach (MarginalCredit existingMC in this.MarginalCredits.Where(mc => !mc.IsDelete))
                {
                    existingMC.IsDelete = true;
                    existingMC.UpdateBy = "Edit";
                    existingMC.UpdateDate = datenow;
                    context.MarginalCredits.Update(existingMC);
                }
            }

            // Debits
            if (updateData.Debits != null)
            {
                // Get IDs of debits that should remain
                var updateDebitIds = updateData.Debits.Where(d => d.Id > 0).Select(d => d.Id).ToList();

                // Soft delete debits that are not in the update request
                foreach (Debit existingDebit in this.Debits.Where(d => d.Id > 0 && !updateDebitIds.Contains(d.Id)))
                {
                    existingDebit.IsDelete = true;
                    existingDebit.UpdateBy = "Edit";
                    existingDebit.UpdateDate = datenow;
                    context.Debits.Update(existingDebit);
                }

                // Update or create debits
                foreach (Debit updateDebit in updateData.Debits)
                {
                    if (updateDebit.Id > 0)
                    {
                        Debit? existingDebit = this.Debits.FirstOrDefault(d => d.Id == updateDebit.Id && !d.IsDelete);
                        if (existingDebit != null)
                        {
                            existingDebit.Edit(this, datenow, context, updateDebit);
                        }
                    }
                    else
                    {
                        updateDebit.CriteriaUserId = this.Id;
                        updateDebit.Create(this, datenow);
                        context.Debits.Add(updateDebit);
                    }
                }
            }
            else
            {
                // If no debits in update, soft delete all existing debits
                foreach (Debit existingDebit in this.Debits.Where(d => !d.IsDelete))
                {
                    existingDebit.IsDelete = true;
                    existingDebit.UpdateBy = "Edit";
                    existingDebit.UpdateDate = datenow;
                    context.Debits.Update(existingDebit);
                }
            }

            // AuctionAmounts
            if (updateData.AuctionAmounts != null)
            {
                // Get IDs of auction amounts that should remain
                var updateAAIds = updateData.AuctionAmounts.Where(aa => aa.Id > 0).Select(aa => aa.Id).ToList();

                // Soft delete auction amounts that are not in the update request
                foreach (AuctionAmount existingAA in this.AuctionAmounts.Where(aa => aa.Id > 0 && !updateAAIds.Contains(aa.Id)))
                {
                    existingAA.IsDelete = true;
                    existingAA.UpdateBy = "Edit";
                    existingAA.UpdateDate = datenow;
                    context.AuctionAmounts.Update(existingAA);
                }

                // Update or create auction amounts
                foreach (AuctionAmount updateAA in updateData.AuctionAmounts)
                {
                    if (updateAA.Id > 0)
                    {
                        AuctionAmount? existingAA = this.AuctionAmounts.FirstOrDefault(a => a.Id == updateAA.Id && !a.IsDelete);
                        if (existingAA != null)
                        {
                            existingAA.Edit(this, datenow, context, updateAA);
                        }
                    }
                    else
                    {
                        updateAA.CriteriaUserId = this.Id;
                        updateAA.Create(this, datenow);
                        context.AuctionAmounts.Add(updateAA);
                    }
                }
            }
            else
            {
                // If no auction amounts in update, soft delete all existing auction amounts
                foreach (AuctionAmount existingAA in this.AuctionAmounts.Where(aa => !aa.IsDelete))
                {
                    existingAA.IsDelete = true;
                    existingAA.UpdateBy = "Edit";
                    existingAA.UpdateDate = datenow;
                    context.AuctionAmounts.Update(existingAA);
                }
            }

            context.CriteriaUsers.Update(this);
            return this;
        }
    }
}