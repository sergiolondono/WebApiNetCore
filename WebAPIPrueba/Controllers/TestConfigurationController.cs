using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebAPIPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public ActionResult<string> get()
        {
            return _configuration["varPrueba"];
        }
    }
}