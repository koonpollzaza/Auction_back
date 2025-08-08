using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

[Table("AuctionAmount")]
public partial class AuctionAmount
{
    [Key]
    public int Id { get; set; }

    public int? CriteriaUserId { get; set; }

    public string? Amount { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("CriteriaUserId")]
    [InverseProperty("AuctionAmounts")]
    public virtual CriteriaUser? CriteriaUser { get; set; }
}
