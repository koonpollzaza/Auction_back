using System.ComponentModel.DataAnnotations;

namespace Auction_back.Models
{
    public class CriteriaMetadata
    {
    }
    [MetadataType(typeof(AuctionMetadata))]
    public partial class Criterion
    {
        public Criterion Create(AuctionCategory auctionCategory, DateTime datenow)
        {
            this.AuctionCategory = auctionCategory;
            this.IsCheck = this.IsCheck;
            this.IsDelete = false;
            this.CreateBy = "pon";
            this.CreateDate = datenow;
            this.UpdateBy = "pon";
            this.UpdateDate = datenow;

            if (this.CriteriaUsers != null)
            {
                foreach (CriteriaUser criteriaUser in this.CriteriaUsers)
                {
                    criteriaUser.Create(this, datenow);
                }
            }
            return this;
        }

        public Criterion Edit(AuctionCategory auctionCategory, DateTime datenow, AuctiondbContext context, Criterion updateData)
        {
            this.AuctionCategory = auctionCategory;
            this.UpdateBy = "pon_Edit";
            this.UpdateDate = datenow;
            this.MetaCriteriaTypeId = updateData.MetaCriteriaTypeId;

            // Handle IsCheck status changes
            bool wasChecked = this.IsCheck ?? false;
            bool nowChecked = updateData.IsCheck ?? false;

            // Case 1: Uncheck (true -> false) - Keep Criteria record but delete CriteriaUser and related data
            if (wasChecked && !nowChecked)
            {
                this.IsCheck = false;

                // Delete CriteriaUser and all related data
                if (this.CriteriaUsers != null)
                {
                    foreach (CriteriaUser existingCU in this.CriteriaUsers.Where(cu => !cu.IsDelete))
                    {
                        // Soft delete all related sub-entities
                        foreach (var credit in existingCU.Credits.Where(c => !c.IsDelete))
                        {
                            credit.IsDelete = true;
                            credit.UpdateBy = "pon_Edit";
                            credit.UpdateDate = datenow;
                            context.Credits.Update(credit);
                        }
                        foreach (var debit in existingCU.Debits.Where(d => !d.IsDelete))
                        {
                            debit.IsDelete = true;
                            debit.UpdateBy = "pon_Edit";
                            debit.UpdateDate = datenow;
                            context.Debits.Update(debit);
                        }
                        foreach (var marginal in existingCU.MarginalCredits.Where(m => !m.IsDelete))
                        {
                            marginal.IsDelete = true;
                            marginal.UpdateBy = "pon_Edit";
                            marginal.UpdateDate = datenow;
                            context.MarginalCredits.Update(marginal);
                        }
                        foreach (var amount in existingCU.AuctionAmounts.Where(a => !a.IsDelete))
                        {
                            amount.IsDelete = true;
                            amount.UpdateBy = "pon_Edit";
                            amount.UpdateDate = datenow;
                            context.AuctionAmounts.Update(amount);
                        }

                        // Soft delete the CriteriaUser as well
                        existingCU.IsDelete = true;
                        existingCU.UpdateBy = "pon_Edit";
                        existingCU.UpdateDate = datenow;
                        context.CriteriaUsers.Update(existingCU);
                    }
                }
            }
            // Case 2: Check back (false -> true) - Change IsCheck to true and create new CriteriaUser and related data
            else if (!wasChecked && nowChecked)
            {
                this.IsCheck = true;

                // Create new CriteriaUser and related data from updateData
                if (updateData.CriteriaUsers != null)
                {
                    foreach (CriteriaUser newCU in updateData.CriteriaUsers)
                    {
                        newCU.CriteriaId = this.Id;
                        newCU.Create(this, datenow);
                        context.CriteriaUsers.Add(newCU);
                    }
                }
            }
            // Case 3: Both checked (true -> true) - Normal update
            else if (wasChecked && nowChecked)
            {
                this.IsCheck = updateData.IsCheck;

                if (this.CriteriaUsers != null && updateData.CriteriaUsers != null)
                {
                    // Get IDs of criteria users that should remain
                    var updateCriteriaUserIds = updateData.CriteriaUsers.Where(cu => cu.Id > 0).Select(cu => cu.Id).ToList();

                    // Soft delete criteria users that are not in the update request
                    foreach (CriteriaUser existingCU in this.CriteriaUsers.Where(cu => cu.Id > 0 && !updateCriteriaUserIds.Contains(cu.Id)))
                    {
                        existingCU.IsDelete = true;
                        existingCU.UpdateBy = "pon_Edit";
                        existingCU.UpdateDate = datenow;

                        // Soft delete all related sub-entities
                        foreach (var credit in existingCU.Credits)
                        {
                            credit.IsDelete = true;
                            credit.UpdateBy = "pon_Edit";
                            credit.UpdateDate = datenow;
                        }
                        foreach (var debit in existingCU.Debits)
                        {
                            debit.IsDelete = true;
                            debit.UpdateBy = "pon_Edit";
                            debit.UpdateDate = datenow;
                        }
                        foreach (var marginal in existingCU.MarginalCredits)
                        {
                            marginal.IsDelete = true;
                            marginal.UpdateBy = "pon_Edit";
                            marginal.UpdateDate = datenow;
                        }
                        foreach (var amount in existingCU.AuctionAmounts)
                        {
                            amount.IsDelete = true;
                            amount.UpdateBy = "pon_Edit";
                            amount.UpdateDate = datenow;
                        }

                        context.CriteriaUsers.Update(existingCU);
                    }

                    // Update existing criteria users
                    foreach (CriteriaUser criteriaUser in this.CriteriaUsers.Where(cu => !cu.IsDelete))
                    {
                        CriteriaUser? updateCriteriaUser = updateData.CriteriaUsers.FirstOrDefault(cu => cu.Id == criteriaUser.Id);
                        if (criteriaUser.Id > 0 && updateCriteriaUser != null)
                        {
                            criteriaUser.Edit(this, datenow, context, updateCriteriaUser);
                        }
                    }

                    // Add new CriteriaUser (id == 0)
                    foreach (CriteriaUser newCU in updateData.CriteriaUsers.Where(cu => cu.Id == 0))
                    {
                        newCU.CriteriaId = this.Id;
                        newCU.Create(this, datenow);
                        context.CriteriaUsers.Add(newCU);
                    }
                }
            }
            // Case 4: Both unchecked (false -> false) - just update basic properties
            else
            {
                this.IsCheck = updateData.IsCheck;
            }

            // Always update the Criteria record itself (never delete it)
            context.Criteria.Update(this);
            return this;
        }
    }
}