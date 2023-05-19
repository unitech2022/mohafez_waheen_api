using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mohafezApi.Models;
using mohafezApi.ViewModels;

namespace mohafezApi.ViewModel
{
    public class HomeUserResponse
    {   
        public UserDetailResponse? UserDetail { get; set; }
        public List<Teacher>? Teachers { get; set; }

    }
}