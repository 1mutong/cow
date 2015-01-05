using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.BLL
{
    public class TaskBLL
    {
        TaskDAL taskDAL = new TaskDAL();
        MedicalDAL dalMedical = new MedicalDAL();
        UserBLL bllUser = new UserBLL();

        /// <summary>
        /// 获取某人未完成的最近任务
        /// </summary>
        /// <param name="operatorID">任务人ID</param>
        /// <returns>未完成任务List</returns>
        public List<DairyTask> GetRecentUnfinishedTaskList(int operatorID, int pastureID)
        {
            List<DairyTask> list = new List<DairyTask>();
            DataTable tb = taskDAL.GetRecentUnfinishedTasksByOperator(operatorID, pastureID);
            foreach (DataRow item in tb.Rows)
            {
                list.Add(WrapTaskItem(item));
            }
            return list;
        }
        
        /// <summary>
        /// 获取牧场所有任务
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <returns></returns>
        public List<DairyTask> GetAllTasks(int pastureID)
        {
            List<DairyTask> list = new List<DairyTask>();
            //以下代码功能怀疑有误，暂时注释掉，sopy
            //if (UserBLL.Instance.CurrentUser.Role.CanBreed)
            //{
            //    DairyTask task = new DairyTask();
            //    task.EarNum = -1;
            //    task.TaskType = TaskType.InseminationTask;
            //    task.Status = DairyTaskStatus.Initial;
            //    task.ArrivalTime = DateTime.Now;
            //    task.RoleID = UserBLL.Instance.CurrentUser.Role.ID;
            //    task.PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            //    task.OperatorID = Convert.ToInt32(UserBLL.Instance.CurrentUser.ID);
            //    list.Add(task);
            //}

            DataTable tb = taskDAL.GetAllTasks(pastureID);
            foreach (DataRow item in tb.Rows)
            {
                list.Add(WrapTaskItem(item));
            }
            return list;
        }

        /// <summary>
        /// 获取牧场所有未完成的最近任务
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <returns></returns>
        public List<DairyTask> GetUnfinishedTasks(int pastureID)
        {
            List<DairyTask> list = new List<DairyTask>();
            DataTable tb = taskDAL.GetRecentUnfinishedTaskList(pastureID);
            foreach (DataRow item in tb.Rows)
            {
                list.Add(WrapTaskItem(item));
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DairyTask GetTaskByID(int id)
        {
            DataTable tb = taskDAL.GetTaskByID(id);
            if (tb.Rows.Count == 1)
            {
                return WrapTaskItem(tb.Rows[0]);
            }
            else
            {
                throw new Exception("No unique task found!");
            }
        }

        /// <summary>
        /// 封装任务项
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>任务</returns>
        public DairyTask WrapTaskItem(DataRow row)
        {
            DairyTask t = new DairyTask();
            if (row != null)
            {
                t.ID = Convert.ToInt32(row["ID"]);
                t.ArrivalTime = Convert.ToDateTime(row["StartTime"]);
                t.DeadLine = Convert.ToDateTime(row["DeadLine"]);
                switch (Convert.ToInt32(row["TaskStatus"]))
                {
                    case 0:
                        t.Status = DairyTaskStatus.Initial;
                        break;
                    case 1:
                        t.Status = DairyTaskStatus.Completed;
                        break;
                    default:
                        break;
                }

                int taskTypeNum = Convert.ToInt32(row["TaskType"]);
                switch (taskTypeNum)
                {
                    case 0:
                        t.TaskType = TaskType.InseminationTask;
                        break;
                    case 1:
                        t.TaskType = TaskType.InitialInspectionTask;
                        break;
                    case 2:
                        t.TaskType = TaskType.ReInspectionTask;
                        break;
                    case 3:
                        t.TaskType = TaskType.Day21ToBornTask;
                        break;
                    case 4:
                        t.TaskType = TaskType.Day7ToBornTask;
                        break;
                    case 5:
                        t.TaskType = TaskType.Day3AfterBornTask;
                        break;
                    case 6:
                        t.TaskType = TaskType.Day10AfterBornTask;
                        break;
                    case 7:
                        t.TaskType = TaskType.Day15AfterBornTask;
                        break;
                    case 8:
                        t.TaskType = TaskType.ImmuneTask;
                        break;
                    case 9:
                        t.TaskType = TaskType.QuarantineTask;
                        break;
                    case 10:
                        t.TaskType = TaskType.GroupingTask;
                        break;
                    case 11:
                        t.TaskType = TaskType.CalfTask;
                        break;
                    default:
                        break;
                }
                if (row["RoleID"] != DBNull.Value)
                {
                    t.RoleID = Convert.ToInt32(row["RoleID"]);
                }
                if (row["OperatorID"] != DBNull.Value)
                {
                    t.OperatorID = Convert.ToInt32(row["OperatorID"]);
                }
                if (row["FinishedTime"] != DBNull.Value)
                {
                    t.CompleteTime = Convert.ToDateTime(row["FinishedTime"]);
                }
                if (row["InputTime"] != DBNull.Value)
                {
                    t.InputTime = Convert.ToDateTime(row["InputTime"]);
                }
                if (row["EarNum"] != DBNull.Value)
                {
                    t.EarNum = Convert.ToInt32(row["EarNum"]);
                    t.DisplayEarNum = CowBLL.ConvertEarNumToDisplayEarNum(t.EarNum);
                }
                if (row["PastureID"] != DBNull.Value)
                {
                    t.PastureID = Convert.ToInt32(row["PastureID"]);
                }
            }
            return t;
        }

        //以下为在各个任务单产生，回填活动，所需要的接口
        /// <summary>
        /// 创建一件任务
        /// </summary>
        /// <param name="myTask">DairyTask</param>
        public bool AddTask(DairyTask myTask)
        {
            bool isSuccessful = false;
            
            TaskDAL dal = new TaskDAL();
            int i = dal.InsertTask(myTask);
            if (i > 0)
            {
                myTask.ID = i;
                isSuccessful = true;
            }

            return isSuccessful;
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="myTask">DairyTask</param>
        public bool UpdateTask(DairyTask myTask)
        {
            bool isSuccessful = false;
            TaskDAL dal = new TaskDAL();
            int i = dal.UpdateTask(myTask);
            if (i == 1)
            {
                isSuccessful = true;
            }
            return isSuccessful;
        }

        /// <summary>
        /// 删除无用的之前的妊检任务单，可能发生返情重配
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <param name="isInitial">初检true,复检false</param>
        /// <returns>删除记录数</returns>
        public int RemovePreviousInspectionTasks(int earNum, bool isInitial)
        {
            int i = 0;
            if (isInitial)
            {
                i = taskDAL.DeletePreviousInitialInspectionTask(earNum);
            }
            else
            {
                i = taskDAL.DeletePreviousReInspectionTask(earNum);
            }
            return i;
        }

        /// <summary>
        /// 删除无用的之前的产前任务单，可能发生流产,早产
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <param name="isInitial">初检true,复检false</param>
        /// <returns>删除记录数</returns>
        public int RemovePreviousBeforeBornTasks(int earNum)
        {
            return taskDAL.DeleteBeforeBornTasks(earNum);
        }

        /// <summary>
        /// 取牧场某牛的饲养员、配种员或兽医的责任人ID
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <param name="earNum">耳号</param>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public int AssignTask(int pastureID,int earNum,int roleID)
        {
            int userID;
            CowGroupBLL g = new CowGroupBLL();
            CowBLL c = new CowBLL();
            Cow cc = c.GetCowInfo(earNum);
            CowGroup cowGroup = g.GetCowGroupList(pastureID).Find(p => p.ID == cc.GroupID);
            if (cowGroup != null && cowGroup.InsemOperatorID != 0)
            {
                userID = cowGroup.InsemOperatorID;
            }
            else
            {
                //牛群未分配，则分配给场长
                userID=UserBLL.Instance.GetUsers(UserBLL.Instance.CurrentUser.Pasture.ID, Role.FARM_ADMIN_ID)[0].ID;
            }

            return userID;
        }

        /// <summary>
        /// 点击[繁殖-发情配种]，增加发情/配种任务
        /// </summary>
        public void CompleteInsemination(DairyTask task, Insemination insemination)
        {
            CowInfo cow = new CowInfo(insemination.EarNum);
            // 添加任务记录，添加即已完成 Todo
            task.Status = DairyTaskStatus.Completed;
            task.InputTime = DateTime.Now;
            taskDAL.InsertTask(task);

            // 删除无效的妊检任务单
            RemovePreviousInspectionTasks(insemination.EarNum, true);
            RemovePreviousInspectionTasks(insemination.EarNum, false);

            // 产生新妊检任务单
            DairyTask initialInspectionTask = new DairyTask();
            initialInspectionTask.TaskType = TaskType.InitialInspectionTask;
            initialInspectionTask.Status = DairyTaskStatus.Initial;
            initialInspectionTask.ArrivalTime = task.CompleteTime.AddDays(DairyParameterBLL.GetCurrentParameterDictionary(cow.FarmCode)[FarmInfo.PN_DAYSOFINITIALINSPECTION]);

            // 3 days to complete task
            initialInspectionTask.DeadLine = initialInspectionTask.ArrivalTime.AddDays(3.0);
            initialInspectionTask.EarNum = insemination.EarNum;

            // 分配配种员
            initialInspectionTask.OperatorID = AssignTask(task.PastureID, task.EarNum, Role.FARM_INSEMINATOR_ID);

            initialInspectionTask.RoleID = task.RoleID;
            initialInspectionTask.PastureID = task.PastureID;
            initialInspectionTask.InputTime = DateTime.Now;
            taskDAL.InsertTask(initialInspectionTask);
            
            // 添加配种记录
            InseminationBLL insemBLL = new InseminationBLL();
            insemBLL.InsertInseminationInfo(insemination);

            // 更新牛繁殖状态
            CowBLL cowBLL = new CowBLL();
            cowBLL.UpdateCowBreedStatus(insemination.EarNum, "已配未检");
        }

        /// <summary>
        /// 妊检初检任务
        /// </summary>
        public void CompleteInitialInspection(DairyTask task, InitialInspection initialInspetion)
        {
            //添加初检记录
            InitialInspectionBLL iBLL = new InitialInspectionBLL();
            initialInspetion.InseminationID = this.taskDAL.GetInseminationID(initialInspetion.EarNum);
            iBLL.InsertInitialInspection(initialInspetion);

            // 更新初检任务记录
            UpdateTask(task);
            
            CowBLL cowBLL = new CowBLL();

            if (initialInspetion.InspectResult == 1)
            {
                //初检+
                //生成新复检任务单
                DairyTask reinspectionTask = new DairyTask();
                reinspectionTask.EarNum = task.EarNum;
                reinspectionTask.PastureID = task.PastureID;
                //分配配种员
                reinspectionTask.OperatorID = task.OperatorID;//复检和初检同一人

                reinspectionTask.Status = DairyTaskStatus.Initial;
                float initialInspectionDays = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID)[FarmInfo.PN_DAYSOFINITIALINSPECTION];
                float reInspectionDays = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID)[FarmInfo.PN_DAYSOFREINSEPECTION];
                reinspectionTask.ArrivalTime = task.ArrivalTime.AddDays(reInspectionDays - initialInspectionDays);
                reinspectionTask.DeadLine = reinspectionTask.ArrivalTime.AddDays(3.0);
                reinspectionTask.TaskType = TaskType.ReInspectionTask;
                reinspectionTask.InputTime = DateTime.Now;
                AddTask(reinspectionTask);
                //更新牛繁殖状态
                cowBLL.UpdateCowBreedStatus(task.EarNum, "初检+");
            }
            else
            {
                //更新牛繁殖状态
                cowBLL.UpdateCowBreedStatus(task.EarNum, "初检-");
            }
        }

        /// <summary>
        /// 妊检复检任务
        /// </summary>
        public void CompleteReInspection(DairyTask task, ReInspection reInspection)
        {
            // 添加复检记录
            ReInspectionBLL rBLL = new ReInspectionBLL();
            reInspection.InseminationID = this.taskDAL.GetInseminationID(reInspection.EarNum);
            rBLL.InsertReInspection(reInspection);

            //更新任务记录，标记完成
            task.Status = DairyTaskStatus.Completed;
            //task.CompleteTime = DateTime.Now;
            task.InputTime = DateTime.Now;

            UpdateTask(task);

            CowBLL cowBLL = new CowBLL();

            if (reInspection.ReInspectResult == 1)
            {
                //复检+
                //生成产前21天任务单
                DairyTask day21ToBornTask = new DairyTask();
                day21ToBornTask.EarNum = task.EarNum;
                day21ToBornTask.PastureID = task.PastureID;
                //分配兽医
               
                day21ToBornTask.OperatorID = AssignTask(task.PastureID,task.EarNum,Role.FARM_DOCTOR_ID);

                day21ToBornTask.Status = DairyTaskStatus.Initial;
                float normalCalvingDays = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID)[FarmInfo.PN_NORMALCALVINGDAYS];
                float reInspectionDays = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID)[FarmInfo.PN_DAYSOFREINSEPECTION];
                day21ToBornTask.ArrivalTime = task.ArrivalTime.AddDays(-reInspectionDays + normalCalvingDays - 14);//NormalCalvingDays是进产房天数
                day21ToBornTask.DeadLine = day21ToBornTask.ArrivalTime.AddDays(1.0);
                day21ToBornTask.TaskType = TaskType.Day21ToBornTask;
                day21ToBornTask.InputTime = DateTime.Now;
                AddTask(day21ToBornTask);
                // 更新牛繁殖状态
                cowBLL.UpdateCowBreedStatus(task.EarNum, "复检+");
            }
            else
            {
                //更新牛繁殖状态
                cowBLL.UpdateCowBreedStatus(task.EarNum, "复检-");
            }
        }

        /// <summary>
        /// 产前21天任务
        /// </summary>
        public void CompleteDay21ToBorn(DairyTask task)
        {
            // 更新任务记录，标记完成
            UpdateTask(task);

            // 生成产前7天任务单
            DairyTask day7ToBornTask = new DairyTask();
            day7ToBornTask.EarNum = task.EarNum;
            day7ToBornTask.PastureID = task.PastureID;
            // 分配兽医
            day7ToBornTask.OperatorID = task.OperatorID;//兽医不变

            day7ToBornTask.Status = DairyTaskStatus.Initial;
            float initialInspectionDays = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID)[FarmInfo.PN_DAYSOFINITIALINSPECTION];
            float reInspectionDays = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID)[FarmInfo.PN_DAYSOFREINSEPECTION];
            day7ToBornTask.ArrivalTime = task.ArrivalTime.AddDays(14.0);
            day7ToBornTask.DeadLine = day7ToBornTask.ArrivalTime.AddDays(3.0);
            day7ToBornTask.TaskType = TaskType.Day7ToBornTask;
            day7ToBornTask.InputTime = DateTime.Now;
            AddTask(day7ToBornTask); 
        }

        /// <summary>
        /// 产前7天任务
        /// </summary>
        public void CompleteDay7ToBorn(DairyTask task, int cowHouseId, int cowGroupId)
        {
            // 更新任务记录，标记完成            
            UpdateTask(task);

            // 产生调群任务，进产房
            DairyTask groupingTask = new DairyTask();
            groupingTask.TaskType = TaskType.GroupingTask;
            DateTime time = DateTime.Now;
            groupingTask.ArrivalTime = time;
            groupingTask.InputTime = time;
            groupingTask.EarNum = task.EarNum;
            groupingTask.DeadLine = time.AddDays(1.0);
            AddTask(groupingTask);

            // 取回这条任务
            DairyTask groupingTaskCopy = GetUnfinishedTasks(UserBLL.Instance.CurrentUser.Pasture.ID).Find(p => p.ArrivalTime == time && p.TaskType == TaskType.GroupingTask);
            // 关联调群记录，任务单找到如何调群
            GroupingRecord groupingRecord = new GroupingRecord();
            groupingRecord.EarNum = groupingTaskCopy.EarNum;
            groupingRecord.TaskID = groupingTaskCopy.ID;
            CowBLL cowBLL = new CowBLL();
            Cow myCow = cowBLL.GetCowInfo(groupingTaskCopy.EarNum);
            groupingRecord.OriginalGroupID = myCow.GroupID;
            groupingRecord.OriginalHouseID = myCow.HouseID;
            groupingRecord.TargetGroupID = cowGroupId;
            groupingRecord.TargetHouseID = cowHouseId;
            GroupingRecordBLL gBLL = new GroupingRecordBLL();
            gBLL.InsertGroupingRecord(groupingRecord);
        }

        /// <summary>
        /// 在产犊界面调用本方法
        /// 产生产后三任务
        /// </summary>
        /// <param name="calving"></param>
        public void CreateAfterBornTasks(Calving calving)
        {
            // 分配兽医,饲养员
            CowGroupBLL g = new CowGroupBLL();
            CowBLL c = new CowBLL();
            Cow cc = c.GetCowInfo(calving.EarNum);
            CowGroup gg = g.GetCowGroupList(cc.FarmCode).Find(p => p.ID == cc.GroupID);

            // 产犊界面，输入产犊信息，调用本方法产生3个产后任务和犊牛饲喂任务
            DairyTask t1 = new DairyTask();
            t1.PastureID = gg.PastureID;
            t1.EarNum = calving.EarNum;
            t1.ArrivalTime = calving.Birthday.AddDays(3.0);
            t1.DeadLine = t1.ArrivalTime.AddDays(1.0);
            t1.OperatorID = gg.DoctorID;
            t1.TaskType = TaskType.Day3AfterBornTask;
            t1.InputTime = DateTime.Now;
            this.AddTask(t1);

            DairyTask t2 = new DairyTask();
            t2.PastureID = gg.PastureID;
            t2.EarNum = calving.EarNum;
            t2.ArrivalTime = calving.Birthday.AddDays(10.0);
            t2.DeadLine = t2.ArrivalTime.AddDays(1.0);
            t2.OperatorID = gg.DoctorID;
            t2.TaskType = TaskType.Day10AfterBornTask;
            t2.InputTime = DateTime.Now;
            this.AddTask(t2);

            DairyTask t3 = new DairyTask();
            t3.PastureID = gg.PastureID;
            t3.EarNum = calving.EarNum;
            t3.ArrivalTime = calving.Birthday.AddDays(15.0);
            t3.DeadLine = t3.ArrivalTime.AddDays(1.0);
            t3.OperatorID = gg.DoctorID;
            t3.TaskType = TaskType.Day15AfterBornTask;
            t3.InputTime = DateTime.Now;
            this.AddTask(t3);

            return;
        }

        /// <summary>
        /// 产后3天任务
        /// </summary>
        public void CompleteDay3AfterBorn(DairyTask task, int cowHouseId, int cowGroupId)
        {
            // 此任务单在，产犊界面/事件中产生，或者流产早产；流产等会取消之前的未完成产前任务单
            // 更新任务记录，标记完成
            task.Status = DairyTaskStatus.Completed;
            task.CompleteTime = DateTime.Now;
            UpdateTask(task);
        }

        /// <summary>
        /// 产后10天任务
        /// </summary>
        public void CompleteDay10AfterBorn(DairyTask task, int cowHouseId, int cowGroupId)
        {
            //此任务单在，产犊界面/事件中产生，或者流产早产；流产等会取消之前的未完成产前任务单
            //更新任务记录，标记完成
            task.Status = DairyTaskStatus.Completed;
            task.CompleteTime = DateTime.Now;
            UpdateTask(task);

            //牛调群至，初产牛群或高产牛群
            DairyTask groupingTask = new DairyTask();
            groupingTask.TaskType = TaskType.GroupingTask;
            DateTime time = DateTime.Now;
            groupingTask.ArrivalTime = time;
            groupingTask.EarNum = task.EarNum;
            groupingTask.DeadLine = time.AddDays(1.0);
            AddTask(groupingTask);
            //取回这条任务
            DairyTask groupingTaskCopy = GetUnfinishedTasks(UserBLL.Instance.CurrentUser.Pasture.ID).Find(p => p.ArrivalTime == time && p.TaskType == TaskType.GroupingTask);
            //关联调群记录，任务单找到如何调群
            GroupingRecord groupingRecord = new GroupingRecord();
            groupingRecord.EarNum = groupingTaskCopy.EarNum;
            groupingRecord.TaskID = groupingTaskCopy.ID;
            CowBLL cowBLL = new CowBLL();
            Cow myCow = cowBLL.GetCowInfo(groupingTaskCopy.EarNum);
            groupingRecord.OriginalGroupID = myCow.GroupID;
            groupingRecord.OriginalHouseID = myCow.HouseID;
            groupingRecord.TargetGroupID = cowGroupId;
            groupingRecord.TargetHouseID = cowHouseId;
            GroupingRecordBLL gBLL = new GroupingRecordBLL();
            gBLL.InsertGroupingRecord(groupingRecord);
        }

        /// <summary>
        /// 产后15天任务单
        /// </summary>
        public void CompleteDay15AfterBorn(DairyTask task)
        {
            //此任务单在，产犊界面/事件中产生，或者流产早产；流产等会取消之前的未完成产前任务单
            //更新任务记录，标记完成
            task.Status = DairyTaskStatus.Completed;
            task.CompleteTime = DateTime.Now;
            task.InputTime = DateTime.Now;
            UpdateTask(task);
        }

        /// <summary>
        /// 完成免疫任务
        /// </summary>
        public void CompleteImmune(DairyTask task)
        {
            //更新任务记录，标记完成
            //return dalMedical.CompleteImmune();
            task.Status = DairyTaskStatus.Completed;
            task.CompleteTime = DateTime.Now;
            task.InputTime = DateTime.Now;
            UpdateTask(task);

        }

        /// <summary>
        /// 增加免疫记录
        /// </summary>
        public int AddImmuneRecord(Immune immune)
        {
            //每头牛做增加操作时调用本方法
            return dalMedical.InsertImmuneRecord(immune.PastureID, immune.ImmuneDate, immune.Vaccine, immune.EarNum, immune.DoctorID);

        }

        /// <summary>
        /// 完成检疫任务
        /// </summary>
        public void CompleteQuarantine(DairyTask task)
        {
            //更新任务记录，标记完成
            task.Status = DairyTaskStatus.Completed;
            task.CompleteTime = DateTime.Now;
            task.InputTime = DateTime.Now;
            UpdateTask(task);

        }

        /// <summary>
        /// 增加检疫记录
        /// </summary>
        public int AddQuarantineRecord(Quarantine q)
        {
            //每头牛做增加操作时调用本方法
            QuarantineBLL qBLL = new QuarantineBLL();
            return qBLL.InsertQurantine(q);
        }

        /// <summary>
        /// 分群任务
        /// </summary>
        public void CompleteGrouping(DairyTask task)
        {
            //各种事件触发产生分群要求，产生任务单
            //饲养员按要求操作，回填完成时间
            task.Status = DairyTaskStatus.Completed;
            task.CompleteTime = DateTime.Now;
            task.InputTime = DateTime.Now;
            UpdateTask(task);


            //更新牛的牛群号，牛舍号
            CowBLL cowBLL = new CowBLL();
            Cow myCow = cowBLL.GetCowInfo(task.EarNum);
            GroupingRecordBLL gBLL = new GroupingRecordBLL();
            GroupingRecord record = gBLL.GetGroupingRecordByTaskID(task.ID);
            myCow.GroupID = record.TargetGroupID;
            myCow.HouseID = record.TargetHouseID;
            cowBLL.UpdateCowGroupAndHouse(myCow.EarNum, myCow.GroupID, myCow.HouseID);
        }

        /// <summary>
        /// 犊牛饲喂任务
        /// </summary>
        public void CompleteCalf(DairyTask task)
        {
            task.Status = DairyTaskStatus.Completed;
            task.CompleteTime = DateTime.Now;
            task.InputTime = DateTime.Now;
            UpdateTask(task);
        }
    }
}
