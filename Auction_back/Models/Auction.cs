using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

[Table("Auction")]
public partial class Auction
{
    [Key]
    public int Id { get; set; }

    public int? MetaAuctionTypeId { get; set; }

    public string? AuctionName { get; set; }

    public bool? Status { get; set; }

    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Auction")]
    public virtual ICollection<AuctionCategory> AuctionCategories { get; set; } = new List<AuctionCategory>();

    [ForeignKey("MetaAuctionTypeId")]
    [InverseProperty("Auctions")]
    public virtual MetaAuctionType? MetaAuctionType { get; set; }
}
