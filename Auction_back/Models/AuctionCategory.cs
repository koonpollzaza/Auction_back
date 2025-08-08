using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

[Table("AuctionCategory")]
public partial class AuctionCategory
{
    [Key]
    public int Id { get; set; }

    public int? AuctionId { get; set; }

    [Column("MetaCategoryID")]
    public int? MetaCategoryId { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public bool IsDelete { get; set; }
    [JsonIgnore]
    [ForeignKey("AuctionId")]
    [InverseProperty("AuctionCategories")]
    public virtual Auction? Auction { get; set; }

    [InverseProperty("AuctionCategory")]
    public virtual ICollection<Criterion> Criteria { get; set; } = new List<Criterion>();

    [ForeignKey("MetaCategoryId")]
    [InverseProperty("AuctionCategories")]
    public virtual MetaCategory? MetaCategory { get; set; }
}
