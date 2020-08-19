using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public PhotosController(IDatingRepository repo,IMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId
        ,PhotoForCreationDto photoForCreationDto){
        try
            {

            if (userId!= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var userFromRepo = await _repo.GetUser(userId);
            var file = photoForCreationDto.file;
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    photoForCreationDto.Url = dbPath;
                    var photo = _mapper.Map<Photo>(photoForCreationDto);

                    if(!userFromRepo.Photos.Any(u => u.IsMain))
                        photo.IsMain = true;
                    
                    userFromRepo.Photos.Add(photo);
                    if (await _repo.SaveAll()){
                        return Ok(new { dbPath });
                    }else
                    {
                        return BadRequest("Could not add the photo");
                    }

                } else
                    {
                        return BadRequest("Could not add the photo");
                    }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }   
    }
}