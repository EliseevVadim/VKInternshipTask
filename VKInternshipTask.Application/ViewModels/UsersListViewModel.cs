using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKInternshipTask.Application.ViewModels
{
    public class UsersListViewModel
    {
        public IList<UserViewModel> Users { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentCount { get; set; }
    }
}
