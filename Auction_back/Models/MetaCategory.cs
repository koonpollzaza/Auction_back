using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

[Table("MetaCategory")]
public partial class MetaCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? CategoryName { get; set; }

    public int? CategoryHeaderId { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("MetaCategory")]
    public virtual ICollection<AuctionCategory> AuctionCategories { get; set; } = new List<AuctionCategory>();
}
