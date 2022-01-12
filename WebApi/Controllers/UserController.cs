using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> UserList = new()
        {
            new User
            {
                Id = 1,
                Email = "mahmut.tuncer@mail.com",
                UserName = "Mahmut Tuncer",
                UserRole = UserRole.Specialist
            },
            new User
            {
                Id = 2,
                Email = "nihat.dogan@mail.com",
                UserName = "Nihat Doğan",
                UserRole = UserRole.Specialist
            },
            new User
            {
                Id = 3,
                Email = "ismail.turut@mail.com",
                UserName = "İsmail Türüt",
                UserRole = UserRole.Specialist
            },
            new User
            {
                Id = 4,
                Email = "selcuk.bilgen@mail.com",
                UserName = "Selçuk Bilgen",
                UserRole = UserRole.Client
            }
        };

        [HttpGet]
        public ActionResult<List<User>> GetBooks()
        {
            var userList = UserList.OrderBy(x => x.Id).ToList();
            return Ok(userList);
        }
        
        // FromRoute
        [HttpGet("{id}")] 
        public ActionResult<User> GetById(int id)
        {
            var user = UserList.SingleOrDefault(b => b.Id == id);
            if (user is null)
                return NotFound();
            
            return Ok(user);
        }
        
        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            var user = UserList.SingleOrDefault(u => u.Email == newUser.Email);

            if (user is not null)
                return BadRequest("Kullanıcı daha önce kayıt olmuş");

            UserList.Add(newUser);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = UserList.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return BadRequest("Kullanıcı bulunamadı");

            user.Email = updatedUser.Email != default ? updatedUser.Email : user.Email;
            user.UserName = updatedUser.UserName != default ? updatedUser.UserName : user.UserName;
            user.UserRole = updatedUser.UserRole != default ? updatedUser.UserRole : user.UserRole;

            return Ok();
        }
        
        // From Query
        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] int id)
        {
            var user = UserList.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return BadRequest();

            UserList.Remove(user);
            return Ok();
        }
    }
}