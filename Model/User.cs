using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string RoleName
        {
            get
            {
                return Role.Name;
            }
        }
        public Role Role { get; set; }
        public Pasture Pasture { get; set; }
    }
}
