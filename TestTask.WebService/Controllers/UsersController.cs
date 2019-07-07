using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.WebService.Services;
using AutoMapper;
using TestTask.WebService.ViewModels;
using TestTask.LoggerService;

namespace TestTask.WebService.Controllers
{
    // Response format change (XML/JSON):
    // 1) With FormatFilter
    // api/users?format=xml or api/users?format=json
    // 2) With Headers
    // Accept: application/xml or application/json

    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;
        public UsersController(IMapper mapper, ILoggerManager logger)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromServices] IUserService userService)
        {
            try
            {
                var users = await userService.GetAllUsersAsync();

                if (users == null)
                {
                    logger.LogError($"Users not found.");
                    return NotFound();
                }

                var userVMs = mapper.Map<IEnumerable<UserViewModel>>(users);
                return Ok(userVMs);

            }
            catch (Exception ex)
            {
                logger.LogError($"GetAllUsers action error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserById([FromServices] IUserService userService, int id)
        {
            try
            {
                var user = await userService.GetUserByIdAsync(id);
                

                if (user == null)
                {
                    logger.LogError($"User with id: {id} not found.");
                    return NotFound();
                }                

                var userVM = mapper.Map<UserViewModel>(user);
                return Ok(userVM);
            }
            catch (Exception ex)
            {
                logger.LogError($"GetUserById action error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        #region Postman query params for UserAuthorizedViewModel with "email" property 
        // Query params in Postman:
        // Header: Content-type: application/json
        // Body type: raw, JSON(application/json),
        // Body: {"username":"Bret", "email":"Sincere@april.biz"}
        #endregion
        // POST api/users/{id} 
        [HttpPost("{id}")]
        public async Task<IActionResult> GetUserByIdWithCredentials([FromServices] IUserService userService, [FromBody] UserCredentials userCredentials, int id)
        {
            try
            {
                var user = await userService.GetUserByIdAsync(id);
                
                if (user == null)
                {
                    logger.LogError($"User with id: {id} not found.");
                    return NotFound();
                }

                if (user.username != userCredentials.username || user.email != userCredentials.email)
                {
                    logger.LogError($"User with id: {id} and credentials username: {userCredentials.username}, email: {userCredentials.email} not found.");
                    return StatusCode(403, "{\"status\":403, \"title\":\"Forbidden\"}");
                }

                var userVM = mapper.Map<UserAuthorizedViewModel>(user);
                return Ok(userVM);
            }
            catch (Exception ex)
            {
                logger.LogError($"GetUserByIdWithCredentials action error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{userId}/albums", Name = "AlbumsByUserId")]
        public async Task<IActionResult> GetAlbumsByUserId([FromServices] IAlbumService albumService, int userId)
        {
            try
            {
                var albums = await albumService.GetAlbumsByUserIdAsync(userId);

                if (albums == null)
                {
                    logger.LogError($"Albums for user with id: {userId} not found.");
                    return NotFound();
                }

                var albumVMs = mapper.Map<IEnumerable<AlbumViewModel>>(albums);
                return Ok(albumVMs);
            }
            catch (Exception ex)
            {
                logger.LogError($"GetAlbumsByUserId action error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}