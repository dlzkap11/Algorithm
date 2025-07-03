using HelloEmpty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloEmpty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpPost]
        public List<HelloMessage> Get()
        {
            List<HelloMessage> messages = new List<HelloMessage>();

            messages.Add(new HelloMessage() { Message = "Hello Wep API 1 !" });
            messages.Add(new HelloMessage() { Message = "Hello Wep API 2 !" });
            messages.Add(new HelloMessage() { Message = "Hello Wep API 3 !" });


            return messages;
        }

    }
}
