using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Domain.Enums;

namespace VKInternshipTask.Domain.Entities
{
    public class UserState
    {
        public int Id { get; set; }
        public UserStateCode Code { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
