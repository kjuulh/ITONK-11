using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSO_Control_Service;
using PSO_Control_Service.Models;

namespace PSO_Control_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareModelsController : ControllerBase
    {
        private readonly PSO_Context _context;

        public ShareModelsController(PSO_Context context)
        {
            _context = context;
        }

        // GET: api/ShareModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShareModel>>> GetShareModel()
        {
            return await _context.ShareModel.ToListAsync();
        }

        // GET: api/ShareModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShareModel>> GetShareModel(int id)
        {
            var shareModel = await _context.ShareModel.FindAsync(id);

            if (shareModel == null)
            {
                return NotFound();
            }

            return shareModel;
        }

        // PUT: api/ShareModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShareModel(int id, ShareModel shareModel)
        {
            if (id != shareModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(shareModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShareModels
        [HttpPost]
        public async Task<ActionResult<ShareModel>> PostShareModel(ShareModel shareModel)
        {
            _context.ShareModel.Add(shareModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShareModel", new { id = shareModel.Id }, shareModel);
        }

        // DELETE: api/ShareModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShareModel>> DeleteShareModel(int id)
        {
            var shareModel = await _context.ShareModel.FindAsync(id);
            if (shareModel == null)
            {
                return NotFound();
            }

            _context.ShareModel.Remove(shareModel);
            await _context.SaveChangesAsync();

            return shareModel;
        }

        private bool ShareModelExists(int id)
        {
            return _context.ShareModel.Any(e => e.Id == id);
        }
    }
}
