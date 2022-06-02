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
        [Route("get_operate_info")]
        public IActionResult getOperateInfo()
        {
            OperateModel dm = new OperateModel(_configuration);
            return null;
        }

        [HttpPost]
        [Route("add_operate_order")]
        public IActionResult addOperateOrder()
        {
            OperateModel dm = new OperateModel(_configuration);

            return null;
        }
    }

}