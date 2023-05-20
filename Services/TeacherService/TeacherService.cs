using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mohafezApi.Data;
using mohafezApi.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace mohafezApi.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {

        private readonly IMapper _mapper;

        private readonly AppDBcontext _context;


        public TeacherService(IMapper mapper, IConfiguration config, AppDBcontext context)
        {
            _mapper = mapper;

            _context = context;
        }
        public async Task<dynamic> AddAsync(dynamic type)
        {
            await _context.Teachers!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;
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
            var teachers = await _context.Teachers!.ToListAsync();

            return teachers;
        }

        public async Task<dynamic> GetTeachersByCountry(string country)
        {
            var teachers = await _context.Teachers!.Where(t => t.Country == country).ToListAsync();

            return teachers;
        }

        public async Task<dynamic> GetTeachersByGender(string Gender)
        {
               var teachers = await _context.Teachers!.Where(t => t.Gender == Gender).ToListAsync();

            return teachers;
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