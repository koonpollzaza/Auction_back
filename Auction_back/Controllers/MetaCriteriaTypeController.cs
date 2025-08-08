using Auction_back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaCriteriaTypeController : ControllerBase
    {
        private readonly AuctiondbContext _context;
        public MetaCriteriaTypeController(AuctiondbContext context)
        {
            _context = context;
        }

        // GET: api/MetaCriteriaType
        [HttpGet]
        public ActionResult<IEnumerable<MetaCriteriaType>> GetMetaCriteriaTypes()
        {
            try
            {
                var metaCriteriaTypes = _context.MetaCriteriaTypes
                    .Where(m => !m.IsDelete)
                    .ToList();

                return Ok(metaCriteriaTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // GET: api/MetaCriteriaType/5
        [HttpGet("{id}")]
        public ActionResult<MetaCriteriaType> GetMetaCriteriaType(int id)
        {
            try
            {
                var metaCriteriaType = _context.MetaCriteriaTypes
                    .FirstOrDefault(m => m.Id == id && !m.IsDelete);

                if (metaCriteriaType == null)
                {
                    return NotFound(new { message = "MetaCriteriaType not found" });
                }

                return Ok(metaCriteriaType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}