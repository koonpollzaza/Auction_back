using Auction_back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaAuctionTypeController : ControllerBase
    {
        private readonly AuctiondbContext _context;
        public MetaAuctionTypeController(AuctiondbContext context)
        {
            _context = context;
        }

        // GET: api/MetaAuctionType
        [HttpGet]
        public ActionResult<IEnumerable<MetaAuctionType>> GetMetaAuctionTypes()
        {
            try
            {
                var metaAuctionTypes = _context.MetaAuctionTypes
                    .Where(m => !m.IsDelete)
                    .ToList();

                return Ok(metaAuctionTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // GET: api/MetaAuctionType/5
        [HttpGet("{id}")]
        public ActionResult<MetaAuctionType> GetMetaAuctionType(int id)
        {
            try
            {
                var metaAuctionType = _context.MetaAuctionTypes
                    .FirstOrDefault(m => m.Id == id && !m.IsDelete);

                if (metaAuctionType == null)
                {
                    return NotFound(new { message = "MetaAuctionType not found" });
                }

                return Ok(metaAuctionType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}