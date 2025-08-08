using Auction_back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaMoneyTypeController : ControllerBase
    {
        private readonly AuctiondbContext _context;
        public MetaMoneyTypeController(AuctiondbContext context)
        {
            _context = context;
        }

        // GET: api/MetaMoneyType
        [HttpGet]
        public ActionResult<IEnumerable<MetaMoneyType>> GetMetaMoneyTypes()
        {
            try
            {
                var metaMoneyTypes = _context.MetaMoneyTypes
                    .Where(m => !m.IsDelete)
                    .ToList();

                return Ok(metaMoneyTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // GET: api/MetaMoneyType/5
        [HttpGet("{id}")]
        public ActionResult<MetaMoneyType> GetMetaMoneyType(int id)
        {
            try
            {
                var metaMoneyType = _context.MetaMoneyTypes
                    .FirstOrDefault(m => m.Id == id && !m.IsDelete);

                if (metaMoneyType == null)
                {
                    return NotFound(new { message = "MetaMoneyType not found" });
                }

                return Ok(metaMoneyType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}