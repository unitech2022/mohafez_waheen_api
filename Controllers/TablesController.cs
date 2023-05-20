using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mohafezApi.Models;
using mohafezApi.Services.TablesService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace mohafezApi.Controllers
{

// dotnet ef migrations add InitialCreate
 // update database 
// dotnet ef database update
// create
// dotnet new webapi -n name 


    [Route("table")]
    public class TablesController : ControllerBase
    {
        private readonly ITablesService _repository;
        private IMapper _mapper;

        public TablesController(ITablesService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add-table")]
        public async Task<ActionResult> AddTable([FromForm] Table table)
        {
            // if (table == null)
            // {
            //     return NotFound();
            // }

            await _repository.AddAsync(table);

            return Ok(table);
        }


        [HttpGet]
        [Route("get-tables")]
        public async Task<ActionResult> GetTable([FromQuery] int teacherId)
        {

            return Ok(await _repository.GetTablesByTeacherId(teacherId));
        }



        [HttpPost]
        [Route("delete-table")]
        public async Task<ActionResult> DeleteTable([FromForm] int tableId)
        {
            Table table = await _repository.DeleteAsync(tableId);

            if (table == null)
            {

                return NotFound();
            }

            return Ok(table);
        }

    }
}