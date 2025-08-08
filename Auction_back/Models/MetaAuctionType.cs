using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

[Table("MetaAuctionType")]
public partial class MetaAuctionType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? AuctionTypeName { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("MetaAuctionType")]
    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
