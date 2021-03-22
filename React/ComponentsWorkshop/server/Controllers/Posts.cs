using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Posts : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            var list = new List<Post>();
            for (int i = 1; i <= 11; i++)
            {
                list.Add(new Post { Author = $"Author {i}", Description = $"Description {i}", Id = Guid.NewGuid().ToString() });
            }

            return list.AsEnumerable();
        }

        // GET api/<Posts>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
