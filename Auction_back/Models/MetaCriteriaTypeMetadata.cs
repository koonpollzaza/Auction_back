using System.ComponentModel.DataAnnotations;

namespace Auction_back.Models
{
    public class MetaCriteriaTypeMetadata
    {
    }
    [MetadataType(typeof(MetaCriteriaTypeMetadata))]
    public partial class MetaCriteriaType
    {
        // public MetaCriteriaType Create(AuctiondbContext context)
        // {
        //     return this;
        // }
    }
}