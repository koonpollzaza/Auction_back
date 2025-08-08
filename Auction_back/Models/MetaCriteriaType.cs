using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Auction_back.Models;

[Table("MetaCriteriaType")]
public partial class MetaCriteriaType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? CriteriaTypeName { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("MetaCriteriaType")]
    public virtual ICollection<Criterion> Criteria { get; set; } = new List<Criterion>();
}
