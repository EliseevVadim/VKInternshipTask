using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Domain.Enums;

namespace VKInternshipTask.Domain.Entities
{
    public class UserGroup
    {
        public int Id { get; set; }
        public UserGroupCode Code { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
