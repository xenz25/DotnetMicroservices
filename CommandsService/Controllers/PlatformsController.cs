using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/commands/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> GetAll()
        {
            string body = await new StreamReader(Request.Body).ReadToEndAsync();
            Console.WriteLine(body);
            return Ok("Hello!");
        } 
        
    }
}