using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WMS_API.Models;
using Microsoft.Extensions.Logging;

namespace WMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController :ControllerBase
    {

        IConfiguration _configuration;
        private readonly ILogger _logger;
        public CostController(IConfiguration configuration,ILogger<CostController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("machinecost/{equip}/{starttime}/{endtime}")]
        public IActionResult getTotalCost()
        {
            return null;
        }
    }

}