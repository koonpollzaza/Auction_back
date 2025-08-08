using System.ComponentModel.DataAnnotations;

namespace Auction_back.Models
{
    public class MetaCategoryMetadata
    {
    }
    [MetadataType(typeof(MetaCategoryMetadata))]
    public partial class MetaCategory
    {
        // public MetaCategory Create(AuctiondbContext context)
        // {
        //     return this;
        // }
    }
}