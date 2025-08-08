using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

[Table("Credit")]
public partial class Credit
{
    [Key]
    public int Id { get; set; }

    public int? CriteriaUserId { get; set; }

    public int? Credit1 { get; set; }

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
    [ForeignKey("CriteriaUserId")]
    [InverseProperty("Credits")]
    public virtual CriteriaUser? CriteriaUser { get; set; }
}
