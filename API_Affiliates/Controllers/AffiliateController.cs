using API_Affiliates.Models;
using API_Affiliates.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Affiliates.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigins")]
    public class AffiliateController : ControllerBase
    {
        private readonly IAffiliateService _service;
        public AffiliateController(IAffiliateService service)
        {
            _service = service;
        }

        // GET: api/<AffiliateController>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var list = await _service.GetListAffiliate();
                if (list.IsNullOrEmpty()) return NoContent();
                return Ok(list.ToList());
            }
            catch (Exception ex) { throw new Exception(); }

        }

        // GET api/<AffiliateController>/5
        [HttpGet("{DNI}")]
        public async Task<IActionResult> Get([FromRoute] string DNI)
        {
            try
            {
                var result = await _service.GetAffiliate(DNI);
                if (result == null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex) { throw new Exception(); }

        }

        // POST api/<AffiliateController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNew([FromBody] Affiliate model)
        {
            try
            {
                if (model == null) return BadRequest();
                var result = await _service.Save(model);
                return Ok(result);
            }
            catch (Exception ex) { throw new Exception(); }

        }

        // PUT api/<AffiliateController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Affiliate model)
        {
            try
            {
                if (model == null) return BadRequest();
                var result = await _service.Update(model);
                return Ok(result);
            }
            catch (Exception ex) { throw new Exception(); }
        }

        // DELETE api/<AffiliateController>/5
        [Authorize]
        [HttpDelete("{DNI}")]
        public async Task<IActionResult> Delete([FromRoute] string DNI)
        {
            try
            {
                if (DNI == null) return BadRequest();
                var result = await _service.Delete(DNI);
                return Ok(result);
            }
            catch (Exception ex) { throw new Exception(); }

        }
    }
}
