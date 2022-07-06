using AutoMapper;
using Cvecara.Entities;
using Cvecara.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json", "applciation/xml")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "Zaposleni")]
        [HttpHead]
        public ActionResult<List<User>> GetUsers()
        {
            var users = userRepository.GetAllUsers();
            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<User>(user));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            try
            {

                User userEntity = mapper.Map<User>(user);
                User confirmation = userRepository.CreateUser(userEntity);
                userRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetUser", "User", new { userId = confirmation.userID});


                return Created(location, mapper.Map<User>(confirmation));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<User> UpdateUser([FromBody] User user)
        {
            try
            {
                var oldUser = userRepository.GetUserById(user.userID);

                if (oldUser == null)
                {
                    return NotFound();
                }

                User userEntity = mapper.Map<User>(user);
                mapper.Map(userEntity, oldUser);
                userRepository.SaveChanges();

                return Ok(mapper.Map<User>(user));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                var user = userRepository.GetUserById(userId);

                if (user == null)
                {
                    return NotFound();
                }

                userRepository.DeleteUser(userId);
                userRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
