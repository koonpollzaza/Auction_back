using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

public partial class Criterion
{
    [Key]
    public int Id { get; set; }

    public int? AuctionCategoryId { get; set; }

    public int? MetaCriteriaTypeId { get; set; }

    public bool? IsCheck { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public bool IsDelete { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]

    [ForeignKey("AuctionCategoryId")]
    [InverseProperty("Criteria")]
    public virtual AuctionCategory? AuctionCategory { get; set; }

    [InverseProperty("Criteria")]
    public virtual ICollection<CriteriaUser> CriteriaUsers { get; set; } = new List<CriteriaUser>();

    [ForeignKey("MetaCriteriaTypeId")]
    [InverseProperty("Criteria")]
    public virtual MetaCriteriaType? MetaCriteriaType { get; set; }
}
