using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mohafezApi.Core;

namespace mohafezApi.Services.TablesService
{
    public interface ITablesService : BaseInterface
    {
         Task<dynamic> GetTablesByTeacherId(string teacherId);

         Task<dynamic> GetTablesByTeacherIdUser(string teacherId);
         Task<dynamic> ChangeStatusTable(int tableId,int status);
    }
}