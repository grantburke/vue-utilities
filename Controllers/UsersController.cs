using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vue_utilities.Models;

namespace vue_utilities.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly UsersContext _db;

        public UsersController(ILogger<UsersController> logger, UsersContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<User> Get(int page, int per_page, string search)
        {
            var users = _db.Users
                .Skip((page - 1) * per_page)
                .Take(per_page);
            
            if (string.IsNullOrEmpty(search)) return users;

            search = search.ToLower();

            return users.Where(w => w.FirstName.ToLower().Contains(search) ||
                                    w.LastName.ToLower().Contains(search) ||
                                    w.Address.ToLower().Contains(search) ||
                                    w.City.ToLower().Contains(search) ||
                                    w.State.ToLower().Contains(search) ||
                                    w.Zip.ToLower().Contains(search));
        }
    }
}
