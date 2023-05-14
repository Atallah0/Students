using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainModels;
using AutoMapper;
using Core.Models;
using Core.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudent _repo;
        private readonly IMapper _mapper;
        private readonly IImage _imageRepo;
        public StudentController(IStudent repo, IMapper mapper, IImage imageRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _imageRepo = imageRepo;
        }

        //GetStudents
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _repo.GetStudentsAsync();
            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        //GetStudent
        [HttpGet]
        [Route("[controller]/{id}"), ActionName("GetStudentByIdAsync")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] int id)
        {
            var student = await _repo.GetStudentAsync(id);

            if(student == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StudentDto>(student));
        }

        //UpdateStudent
        [HttpPut]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> UpdateStudentDetailsAsync([FromRoute] int id, [FromBody] UpdateStudentRequestDto request)
        {
            if(await _repo.Exists(id))
            {
                var updateStudent = await _repo.UpdateStudentAsync(id, _mapper.Map<Student>(request));
                if(updateStudent != null)
                {
                    return Ok(_mapper.Map<StudentDto>(updateStudent)); //
                }
            }
            return NotFound();
        }

        //DeleteStudent
        [HttpDelete]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> DeleteStudentByIdAsync([FromRoute] int id)
        {
            if(await _repo.Exists(id))
            {
                var student = await _repo.DeleteStudenteAsync(id);
                return Ok(_mapper.Map<StudentDto>(student));
            }
            return NotFound();
        }
    
        //CreateStudent
        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddNewStudentAsync([FromBody] AddStudentRequestDto request)
        {
            var student = await _repo.AddStudentAsync(_mapper.Map<Student>(request));
            return CreatedAtAction(nameof(GetStudentByIdAsync), new {id = student.Id},
                _mapper.Map<StudentDto>(student));
        }
    
        //UploadImage
        [HttpPost]
        [Route("[controller]/{id}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] int id, IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
                ".jpeg",
                ".png",
                ".gif",
                ".jpg"
            };
            if(profileImage != null && profileImage.Length > 0 )
            {
                var extinsion = Path.GetExtension(profileImage.FileName);
                if(validExtensions.Contains(extinsion))
                {
                    if(await _repo.Exists(id))
                    {
                        var fileName = Path.GetExtension(profileImage.FileName);
                        var fileImagePath =  await _imageRepo.UploadImageAsync(profileImage, fileName);
                        if(await _repo.UpdateProfileImageAsync(id, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }
                return BadRequest("This is not a valid image format");
            }
            return NotFound();
        }
    }    
}