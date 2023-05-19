using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mohafezApi.Models;
using mohafezApi.Services.TeacherService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace mohafezApi.Controllers
{
    [Route("teacher")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _repository;
        private IMapper _mapper;

        public TeachersController(ITeacherService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add-teacher")]
        public async Task<ActionResult> AddTeacher([FromForm] Teacher teacher)
        {
            // if (teacher == null)
            // {
            //     return NotFound();
            // }

            await _repository.AddAsync(teacher);

            return Ok(teacher);
        }


        [HttpGet]
        [Route("get-teachers")]
        public async Task<ActionResult> GetTeachers([FromQuery] string Country)
        {

            return Ok(await _repository.GetTeachersByCountry(Country));
        }



        [HttpPost]
        [Route("delete-teacher")]
        public async Task<ActionResult> DeleteTeacher([FromForm] int teacherId)
        {
            Teacher teacher = await _repository.DeleteAsync(teacherId);

            if (teacher == null)
            {

                return NotFound();
            }

            return Ok(teacher);
        }

    }
}