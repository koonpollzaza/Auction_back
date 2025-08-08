using Auction_back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaCategoryController : ControllerBase
    {
        private readonly AuctiondbContext _context;
        public MetaCategoryController(AuctiondbContext context)
        {
            _context = context;
        }

        // GET: api/MetaCategory
        [HttpGet]
        public ActionResult<IEnumerable<MetaCategory>> GetMetaCategories()
        {
            try
            {
                var metaCategories = _context.MetaCategories
                    .Where(m => !m.IsDelete)
                    .ToList();

                return Ok(metaCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // GET: api/MetaCategory/5
        [HttpGet("{id}")]
        public ActionResult<MetaCategory> GetMetaCategory(int id)
        {
            try
            {
                var metaCategory = _context.MetaCategories
                    .FirstOrDefault(m => m.Id == id && !m.IsDelete);

                if (metaCategory == null)
                {
                    return NotFound(new { message = "MetaCategory not found" });
                }

                return Ok(metaCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}