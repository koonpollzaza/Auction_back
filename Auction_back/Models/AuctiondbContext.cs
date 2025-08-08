using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

public partial class AuctiondbContext : DbContext
{
    public AuctiondbContext()
    {
    }

    public AuctiondbContext(DbContextOptions<AuctiondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<AuctionAmount> AuctionAmounts { get; set; }

    public virtual DbSet<AuctionCategory> AuctionCategories { get; set; }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<CriteriaUser> CriteriaUsers { get; set; }

    public virtual DbSet<Criterion> Criteria { get; set; }

    public virtual DbSet<Debit> Debits { get; set; }

    public virtual DbSet<MarginalCredit> MarginalCredits { get; set; }

    public virtual DbSet<MetaAuctionType> MetaAuctionTypes { get; set; }

    public virtual DbSet<MetaCategory> MetaCategories { get; set; }

    public virtual DbSet<MetaCriteriaType> MetaCriteriaTypes { get; set; }

    public virtual DbSet<MetaMoneyType> MetaMoneyTypes { get; set; }

    public virtual DbSet<MetaUser> MetaUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-DPDCLK2; Initial Catalog=Auctiondb; Integrated Security=True; Pooling=False; Encrypt=False; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Auction_1");

            entity.HasOne(d => d.MetaAuctionType).WithMany(p => p.Auctions).HasConstraintName("FK_Auction_Auction");
        });

        modelBuilder.Entity<AuctionAmount>(entity =>
        {
            entity.HasOne(d => d.CriteriaUser).WithMany(p => p.AuctionAmounts).HasConstraintName("FK_AuctionAmount_CriteriaUser");
        });

        modelBuilder.Entity<AuctionCategory>(entity =>
        {
            entity.HasOne(d => d.Auction).WithMany(p => p.AuctionCategories).HasConstraintName("FK_AuctionCategory_Auction");

            entity.HasOne(d => d.MetaCategory).WithMany(p => p.AuctionCategories).HasConstraintName("FK_AuctionCategory_MetaCategory");
        });

        modelBuilder.Entity<Credit>(entity =>
        {
            entity.HasOne(d => d.CriteriaUser).WithMany(p => p.Credits).HasConstraintName("FK_Credit_CriteriaUser");
        });

        modelBuilder.Entity<CriteriaUser>(entity =>
        {
            entity.HasOne(d => d.Criteria).WithMany(p => p.CriteriaUsers).HasConstraintName("FK_CriteriaUser_Criteria");

            entity.HasOne(d => d.MetaUser).WithMany(p => p.CriteriaUsers).HasConstraintName("FK_CriteriaUser_MetaUser");
        });

        modelBuilder.Entity<Criterion>(entity =>
        {
            entity.HasOne(d => d.AuctionCategory).WithMany(p => p.Criteria).HasConstraintName("FK_Criteria_AuctionCategory");

            entity.HasOne(d => d.MetaCriteriaType).WithMany(p => p.Criteria).HasConstraintName("FK_Criteria_MetaCriteriaType");
        });

        modelBuilder.Entity<Debit>(entity =>
        {
            entity.HasOne(d => d.CriteriaUser).WithMany(p => p.Debits).HasConstraintName("FK_Debit_CriteriaUser");

            entity.HasOne(d => d.MetaMoneyType).WithMany(p => p.Debits).HasConstraintName("FK_Debit_MetaMoneyType");
        });

        modelBuilder.Entity<MarginalCredit>(entity =>
        {
            entity.HasOne(d => d.CriteriaUser).WithMany(p => p.MarginalCredits).HasConstraintName("FK_MarginalCredit_CriteriaUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
