using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mohafezApi.Core;

namespace mohafezApi.Services.TeacherService
{
    public interface ITeacherService : BaseInterface
    {
         Task<dynamic> GetTeachersByCountry(string country);
          Task<dynamic> GetTeachersByGender(string Gender);
    }
}