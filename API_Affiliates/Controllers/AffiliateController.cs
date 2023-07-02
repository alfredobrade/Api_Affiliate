using API_Affiliates.Models;
using API_Affiliates.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace API_Affiliates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigins")]
    public class AffiliateController : ControllerBase
    {
        private readonly IAffiliateService _service;
        public AffiliateController(IAffiliateService service)
        {
            _service = service;
        }

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

        [HttpGet("{DNI}")]
        public async Task<IActionResult> GetAffiliate([FromRoute] string DNI)
        {
            try
            {
                var result = await _service.GetAffiliate(DNI);
                if (result == null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex) { throw new Exception(); }

        }

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
            catch (Exception) { throw; }

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Affiliate model)
        {
            try
            {
                if (id == 0 || id != model.Id) return BadRequest();
                if (model == null) return BadRequest();
                var result = await _service.Update(model);
                return Ok(result);
            }
            catch (Exception ex) { throw new Exception(); }
        }

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
