using API_Affiliates.Models;
using API_Affiliates.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<AffiliateController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Affiliate> Get()
        {
            return _service.GetListAffiliate();
        }

        // GET api/<AffiliateController>/5
        [HttpGet("{DNI}")]
        public IActionResult Get(string DNI)
        {
            var result = _service.GetAffiliate(DNI);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // POST api/<AffiliateController>
        [Authorize]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AffiliateController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AffiliateController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
