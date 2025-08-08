using Auction_back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaUserController : ControllerBase
    {
        private readonly AuctiondbContext _context;
        public MetaUserController(AuctiondbContext context)
        {
            _context = context;
        }

        // GET: api/MetaUser
        [HttpGet]
        public ActionResult<IEnumerable<MetaUser>> GetMetaUsers()
        {
            try
            {
                var metaUsers = _context.MetaUsers
                    .Where(m => !m.IsDelete)
                    .ToList();

                return Ok(metaUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // GET: api/MetaUser/5
        [HttpGet("{id}")]
        public ActionResult<MetaUser> GetMetaUser(int id)
        {
            try
            {
                var metaUser = _context.MetaUsers
                    .FirstOrDefault(m => m.Id == id && !m.IsDelete);

                if (metaUser == null)
                {
                    return NotFound(new { message = "MetaUser not found" });
                }

                return Ok(metaUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}