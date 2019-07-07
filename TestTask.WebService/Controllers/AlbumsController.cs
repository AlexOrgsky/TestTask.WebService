using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask.LoggerService;
using TestTask.WebService.Services;
using TestTask.WebService.ViewModels;

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
    public class AlbumsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;
        public AlbumsController(IMapper mapper, ILoggerManager logger)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums([FromServices]  IAlbumService albumService)
        {
            try
            {
                var albums = await albumService.GetAllAlbumsAsync();

                if (albums == null)
                {
                    logger.LogError($"Albums not found.");
                    return NotFound();
                }

                var albumVMs = mapper.Map<IEnumerable<AlbumViewModel>>(albums);
                return Ok(albumVMs);
            }
            catch (Exception ex)
            {
                logger.LogError($"GetAllAlbums action error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "AlbumById")]
        public async Task<IActionResult> GetAlbumById([FromServices] IAlbumService albumService, int id)
        {
            try
            {
                var album = await albumService.GetAlbumByIdAsync(id);

                if (album == null)
                {
                    logger.LogError($"Album with id: {id} not found.");
                    return NotFound();
                }

                var albumVM = mapper.Map<AlbumViewModel>(album);
                return Ok(albumVM);
            }
            catch (Exception ex)
            {
                logger.LogError($"GetAlbumById action error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}