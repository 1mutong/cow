using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class DairyTask
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public TaskType TaskType { get; set; }
        /// <summary>
        /// 任务类型ID
        /// </summary>
        public int TaskTypeID { get { return Convert.ToInt32(TaskType); } }
        /// <summary>
        /// 任务类型-显示用
        /// </summary>
        public string TaskTypeText { get; set; }
        /// <summary>
        /// 牧场ID
        /// </summary>
        public int PastureID { get; set; }

        public int ID { get; set; }


        /// <summary>
        /// 任务指导
        /// </summary>
        public string TaskInstruction 
        { 
            get
            {
                string s;
                switch (this.TaskType)
                {
                    case TaskType.InseminationTask:
                        s = "请观测牛群发情状况，并给发情牛配种。";
                        break;
                    case TaskType.InitialInspectionTask:
                        s = "请检测该牛妊娠情况。";
                        break;
                    case TaskType.ReInspectionTask:
                        s = "请检测该牛妊娠情况。";
                        break;
                    case TaskType.Day21ToBornTask:
                        s = "请进行该牛产前21天护理。补充维生素A、D、E，肌肉注射亚硒酸钠。";
                        break;
                    case TaskType.Day7ToBornTask:
                        s = "请进行该牛产前7天护理。进产房，调群。";
                        break;
                    case TaskType.Day3AfterBornTask:
                        s = "请进行该牛产后 1-3天护理。观察产后胎衣，如胎衣不下，需进行处置。";
                        break;
                    case TaskType.Day10AfterBornTask:
                        s = "请进行该牛产后10天护理。";
                        break;
                    case TaskType.Day15AfterBornTask:
                        s = "请进行该牛产后15天护理。排恶露； 清宫用药。";
                        break;
                    case TaskType.ImmuneTask:
                        s = "请进行免疫或驱虫。";
                        break;
                    case TaskType.QuarantineTask:
                        s = "请进行检疫。";
                        break;
                    case TaskType.GroupingTask:
                        s = "请把牛分入相应群、舍。";
                        break;
                    case Model.TaskType.CalfTask:
                        s = "请按照规程饲喂犊牛。";
                        break;
                    default:
                        s = "";
                        break;
                }
                return s;
            }
        }
        /// <summary>
        /// 任务应开始时间
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// 任务应完成时间
        /// </summary>
        public DateTime DeadLine { get; set; }
        /// <summary>
        /// 实际任务完成时间长度
        /// </summary>
        public TimeSpan TaskTime { get; set; }
        /// <summary>
        /// 实际任务完成时间
        /// </summary>
        public DateTime CompleteTime { get; set; }
        /// <summary>
        /// 任务回填时间
        /// </summary>
        public DateTime InputTime { get; set; }
        /// <summary>
        /// 任务角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 任务人ID
        /// </summary>
        public int OperatorID { get; set; }
        /// <summary>
        /// 任务状态，0起始，1完成
        /// </summary>
        public DairyTaskStatus Status { get; set; }
        /// <summary>
        /// 耳号
        /// </summary>
        public int EarNum { get; set; }

        public string DisplayEarNum { get; set; }
    }

    /// <summary>
    /// 任务类型枚举
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// 发情/配种任务单
        /// </summary>
        InseminationTask = 0,
        /// <summary>
        /// 妊检初检任务单
        /// </summary>
        InitialInspectionTask = 1,
        /// <summary>
        /// 妊检复检任务单
        /// </summary>
        ReInspectionTask = 2,
        /// <summary>
        /// 产前21天任务单
        /// </summary>
        Day21ToBornTask = 3,
        /// <summary>
        /// 产前7天任务单
        /// </summary>
        Day7ToBornTask = 4,
        /// <summary>
        ///产后3天任务单
        /// </summary>
        Day3AfterBornTask = 5,
        /// <summary>
        /// 产后10天任务单
        /// </summary>
        Day10AfterBornTask = 6,
        /// <summary>
        /// 产后15天任务单
        /// </summary>
        Day15AfterBornTask = 7,
        /// <summary>
        /// 免疫任务单
        /// </summary>
        ImmuneTask = 8,
        /// <summary>
        /// 检疫任务单
        /// </summary>
        QuarantineTask = 9,
        /// <summary>
        ///分群任务单
        /// </summary>
        GroupingTask = 10,
        /// <summary>
        ///犊牛饲喂任务单
        /// </summary>
        CalfTask = 11
    }

    public enum DairyTaskStatus
    {
        Initial=0,
        Completed=1
    }
}
