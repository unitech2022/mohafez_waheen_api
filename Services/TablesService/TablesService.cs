using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mohafezApi.Data;
using mohafezApi.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace mohafezApi.Services.TablesService
{
    public class TablesService : ITablesService
    {

        private readonly IMapper _mapper;

        private readonly AppDBcontext _context;


        public TablesService(IMapper mapper, IConfiguration config, AppDBcontext context)
        {
            _mapper = mapper;

            _context = context;
        }
        public async Task<dynamic> AddAsync(dynamic type)
        {
            await _context.Tables!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;
        }

        public async Task<dynamic> ChangeStatusTable(int tableId,int status)
        {
            Table? table = await _context.Tables!.FirstOrDefaultAsync(x => x.Id == tableId);
            table!.Status=status;
             _context.SaveChanges();
             return table;
            
        }

        public async Task<dynamic> DeleteAsync(int typeId)
        {
            Teacher? teacher = await _context.Teachers!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (teacher != null)
            {
                _context.Teachers!.Remove(teacher);

                await _context.SaveChangesAsync();
            }

            return teacher!;
        }

        public async Task<dynamic> GetItems()
        {
            var teachers = await _context.Tables!.ToListAsync();

            return teachers;
        }

        public async Task<dynamic> GetTablesByTeacherId(string teacherId)
        { 
           
            List<DateTime> categories=new List<DateTime>();

            var teachers = await _context.Tables!.Where(t => t.UserId == teacherId).ToListAsync();
             categories = teachers.Select(i => i.DateToday.Date).Distinct().ToList();

            return new{
                categories=categories,
                teachers=teachers
            };
        }

        public async Task<dynamic> GetTablesByTeacherIdUser(string teacherId)
        {
                List<Table> tables=new List<Table>();

            var allTables = await _context.Tables!.Where(t => t.UserId == teacherId).ToListAsync();

            foreach (var item in allTables)
            {
                int result = DateTime.Compare(item.DateToday, DateTime.Now);
                if(result >= 0){
                    tables.Add(item);
                }
            }
           

            return tables ;
        }

        public Task<dynamic> GitById(int typeId)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(dynamic category)
        {
            throw new NotImplementedException();
        }
    }
}