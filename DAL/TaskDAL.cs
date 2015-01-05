using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Data;
using System.Text;

namespace DairyCow.DAL
{
    public class TaskDAL : BaseDAL
    {
        /// <summary>
        /// 三天内，和之前所有未完成任务
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecentUnfinishedTaskList(int pastureID)
        {
            DataTable recentTaskList = null;

            string sql = string.Format(@"SELECT ID, 
                                        PastureID,
                                        [TaskType], 
                                        OperatorID, 
                                        ArrivalTime as StartTime, 
                                        DeadLine, 
                                        [Status] as TaskStatus, 
                                        FinishedTime,
                                        InputTime,
                                        RoleID,
                                        EarNum
                                        FROM Task
                                        where PastureID={0} and [Status]=0 and ArrivalTime<=DATEADD(DD,3,GETDATE())", pastureID);

            recentTaskList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return recentTaskList;
        }

        public DataTable GetAllTasks(int pastureID)
        {
            DataTable taskList = null;

            string sql = string.Format(@"SELECT ID, 
                                        PastureID
                                        [TaskType], 
                                        OperatorID, 
                                        ArrivalTime as StartTime, 
                                        DeadLine, 
                                        [Status] as TaskStatus, 
                                        FinishedTime,
                                        InputTime,
                                        RoleID,
                                        EarNum
                                        FROM Task
                                        WHERE PastureID={0}", pastureID);

            taskList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return taskList;
        }

        /// <summary>
        /// 获取该任务人，三天内要开始的任务
        /// </summary>
        /// <param name="operatorID">任务操作人ID</param>
        /// <returns>任务table</returns>
        public DataTable GetRecentUnfinishedTasksByOperator(int operatorID, int pastureID)
        {
            DataTable taskList = null;

            string sql = string.Format(@"SELECT ID, 
                                        PastureID,
                                        [TaskType], 
                                        OperatorID, 
                                        ArrivalTime as StartTime, 
                                        DeadLine, 
                                        [Status] as TaskStatus, 
                                        FinishedTime,
                                        InputTime,
                                        RoleID,
                                        EarNum
                                        FROM Task
                                        where OperatorID={0} and PastureID={1}
                                        and [Status]=0 and ArrivalTime<=DATEADD(DD,3,GETDATE())", operatorID, pastureID);

            taskList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return taskList;
        }

        public DataTable GetTaskByID(int id)
        {
            DataTable taskList = null;

            string sql = string.Format(@"SELECT ID, 
                                        PastureID,
                                        [TaskType], 
                                        OperatorID, 
                                        ArrivalTime as StartTime, 
                                        DeadLine, 
                                        [Status] as TaskStatus, 
                                        FinishedTime,
                                        InputTime,
                                        RoleID,
                                        EarNum
                                        FROM Task
                                        where [ID]={0}", id);

            taskList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return taskList;
        }

        /// <summary>
        /// Insert task
        /// </summary>
        /// <param name="myTask"></param>
        /// <returns></returns>
        public int InsertTask(DairyTask myTask)
        {
            StringBuilder sqlString = new StringBuilder();
            int typeValue;
            int taskStatus;
            switch (myTask.Status)
            {
                case DairyTaskStatus.Initial:
                    taskStatus = 0;
                    break;
                case DairyTaskStatus.Completed:
                    taskStatus = 1;
                    break;
                default:
                    taskStatus = 999;//unknow status
                    break;
            }
            switch (myTask.TaskType)
            {
                case TaskType.InseminationTask:
                    //建立即完成，配种
                    typeValue = 0;
                    break;
                case TaskType.InitialInspectionTask:
                    //配种后产生
                    typeValue = 1;
                    break;
                case TaskType.ReInspectionTask:
                    typeValue = 2;
                    break;
                case TaskType.Day21ToBornTask:
                    typeValue = 3;
                    break;
                case TaskType.Day7ToBornTask:
                    typeValue = 4;
                    break;
                case TaskType.Day3AfterBornTask:
                    typeValue = 5;
                    break;
                case TaskType.Day10AfterBornTask:
                    typeValue = 6;
                    break;
                case TaskType.Day15AfterBornTask:
                    typeValue = 7;
                    break;
                case TaskType.ImmuneTask:
                    myTask.EarNum = 0;//牛耳号无意义
                    typeValue = 8;
                    break;
                case TaskType.QuarantineTask:
                    myTask.EarNum = 0;//牛耳号无意义
                    typeValue = 9;
                    break;
                case TaskType.GroupingTask:
                    typeValue = 10;
                    break;
                case TaskType.CalfTask:
                    typeValue = 11;
                    break;
                default:
                    typeValue = 999; //unknow task
                    break;
            }
            if (myTask.TaskType == TaskType.InseminationTask)
            {
                sqlString.Append(@"insert into [Task] 
                          (TaskType,OperatorID,EarNum,ArrivalTime,DeadLine,FinishedTime,[Status],InputTime,RoleID,PastureID) 
                                                    values (
                                    '" + typeValue + "',"
                          + myTask.OperatorID + ",'"
                          + myTask.EarNum + "','"
                          + myTask.ArrivalTime + "','"
                          + myTask.DeadLine + "','"
                          + myTask.CompleteTime + "',"
                          + taskStatus + ",'"
                          + myTask.InputTime + "',"
                          + myTask.RoleID + ","
                          + myTask.PastureID + "); select @@identity;");
            }
            else
            {
                sqlString.Append(@"insert into [Task] 
                          (TaskType,OperatorID,EarNum,ArrivalTime,DeadLine,[Status],InputTime,RoleID,PastureID) 
                                                    values (
                                    '" + typeValue + "',"
                          + myTask.OperatorID + ",'"
                          + myTask.EarNum + "','"
                          + myTask.ArrivalTime + "','"
                          + myTask.DeadLine + "',"
                          + taskStatus + ",'"
                          + myTask.InputTime + "',"
                          + myTask.RoleID + ","
                          + myTask.PastureID + "); select @@identity;");
            }


            object taskId = dataProvider1mutong.ExecuteScalar(sqlString.ToString(), CommandType.Text);
            return Convert.ToInt32(taskId);
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="myTask"></param>
        /// <returns></returns>
        public int UpdateTask(DairyTask myTask)
        {

            StringBuilder sqlString = new StringBuilder();
            int taskStatus;
            switch (myTask.Status)
            {
                case DairyTaskStatus.Initial:
                    taskStatus = 0;
                    break;
                case DairyTaskStatus.Completed:
                    taskStatus = 1;
                    break;
                default:
                    taskStatus = 999;//unknow status
                    break;
            }
            sqlString.Append(@"Update [Task] SET");

            if (myTask.CompleteTime != null)
            {
                sqlString.Append(" [FinishedTime] = '" + myTask.CompleteTime + "',");
            }
            if (myTask.InputTime != null)
            {
                sqlString.Append("[InputTime] = '" + myTask.InputTime + "',");
            }
            sqlString.Append(" [Status] = " + taskStatus);
            sqlString.Append(" WHERE [ID] = " + myTask.ID);



            return dataProvider1mutong.ExecuteNonQuery(sqlString.ToString(), CommandType.Text);

        }

        /// <summary>
        /// 删除无效初检任务
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <returns></returns>
        public int DeletePreviousInitialInspectionTask(int earNum)
        {
            string sql = String.Format(@"Delete From [Task] Where TaskType=1 AND [Status]=0 AND EarNum={0}", earNum);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 删除无效复检任务
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <returns></returns>
        public int DeletePreviousReInspectionTask(int earNum)
        {
            string sql = String.Format(@"Delete From [Task] Where TaskType=2 AND [Status]=0 AND EarNum={0}", earNum);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 删除无效产前任务
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <returns></returns>
        public int DeleteBeforeBornTasks(int earNum)
        {
            string sql = String.Format(@"Delete From [Task] Where (TaskType=3 OR TaskType=4) AND [Status]=0 AND EarNum={0}", earNum);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public int GetInseminationID(int earNum)
        {
            string sql = string.Format(@"SELECT top 1 ID
                                        FROM [1mutong].[dbo].[Breed_Insemination]
                                        where EarNum={0}
                                        order by ID desc", earNum);
            return Convert.ToInt32(dataProvider1mutong.ExecuteScalar(sql, CommandType.Text));
        }
    }
}