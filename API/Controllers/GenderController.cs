using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainModels;
using AutoMapper;
using Core.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IStudent _repo;
        private readonly IMapper _mapper;
        public GenderController(IStudent repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //GetGender
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGendersAsync()
        {
            var genders = await _repo.GetGendersAsync();
            
            if(genders == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GenderDto>>(genders));
        }
    }
}