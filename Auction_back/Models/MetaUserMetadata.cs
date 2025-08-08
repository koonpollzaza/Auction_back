using System.ComponentModel.DataAnnotations;

namespace Auction_back.Models
{
    public class MetaUserMetadata
    {
    }
    [MetadataType(typeof(MetaUserMetadata))]
    public partial class MetaUser
    {
        // public MetaUser Create(AuctiondbContext context)
        // {
        //     return this;
        // }
    }
}