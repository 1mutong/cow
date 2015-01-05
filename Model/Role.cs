using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class Role
    {
        /// <summary>
        /// 场长
        /// </summary>
        public const int FARM_ADMIN_ID = 8;
        /// <summary>
        /// 平台管理员
        /// </summary>
        public const int PLATFORM_ADMIN_ID = 9;
        /// <summary>
        /// 兽医
        /// </summary>
        public const int FARM_DOCTOR_ID = 5;
        /// <summary>
        /// 饲养员
        /// </summary>
        public const int FARM_FEEDER_ID = 3;
        /// <summary>
        /// 配种员
        /// </summary>
        public const int FARM_INSEMINATOR_ID = 1;
        public int ID { get; set; }
        public string Name { get; set; }
        public int SupervisorID { get; set; }
        public bool CanBreed { get; set; }
        public bool CanFeed { get; set; }
        public bool CanMedical { get; set; }
        public bool CanMilk { get; set; }
        /// <summary>
        /// 是平台管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 是场长
        /// </summary>
        public bool IsDirector { get; set; }

        public List<string> Menus { get; set; }
    }
}
