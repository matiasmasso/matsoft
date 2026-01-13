using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using iText.Forms.Xfdf;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class TaskService
    {

        public static TaskModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Tasks
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new TaskModel(x.Guid)
                    {
                        Nom = x.Nom
                    }).FirstOrDefault();
            }
        }

        public static bool Update(TaskModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Task? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Task();
                    db.Tasks.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Tasks.Find(value.Guid);

                if (entity == null) throw new Exception("Task not found");

                entity.Nom = value.Nom;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Tasks.Remove(db.Tasks.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


        public static bool UpdateSchedule(TaskModel.Schedule value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.TaskSchedule? entity;
                if (value.IsNew)
                {
                    entity = new Entities.TaskSchedule();
                    db.TaskSchedules.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.TaskSchedules.Find(value.Guid);

                if (entity == null) throw new Exception("Task schedule not found");

                entity.Task = value.Task.Guid;
                entity.Enabled = value.Enabled;
                entity.Mode = (int)value.Mode;
                entity.TimeInterval = value.TimeInterval;
                entity.Weekdays = value.WeekDays;

                db.SaveChanges();
                return true;
            }
        }

        public static bool DeleteSchedule(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.TaskSchedules.Remove(db.TaskSchedules.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class TasksService
    {
        public static List<TaskModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                var schedules = db.TaskSchedules
                    .Select(x => new TaskModel.Schedule(x.Guid)
                    {
                        Task = new TaskModel(x.Task),
                        Enabled = x.Enabled,
                        Mode = (TaskModel.Schedule.Modes)x.Mode,
                        TimeInterval = x.TimeInterval,
                        WeekDays = x.Weekdays
                    }).ToList();

                var retval = db.Tasks
                    .AsNoTracking()
                    .Include(x => x.TaskSchedules)
                    .Join(db.VwTaskLastLogs, x => x.Guid, log => log.Task, (x, log) => new TaskModel(x.Guid)
                    {
                        Cod = (TaskModel.Cods)x.Cod,
                        Nom = x.Nom,
                        Dsc = x.Dsc,
                        Enabled = x.Enabled,
                        NotBefore = x.NotBefore,
                        NotAfter = x.NotAfter,
                        LastLog = new TaskModel.Log(log.Guid)
                        {
                            Fch = log.Fch,
                            ResultCod = (TaskModel.ResultCods)log.ResultCod,
                            ResultMsg = log.ResultMsg
                        }
                    }).ToList();

                foreach (var task in retval)
                {
                    task.Schedules = schedules.Where(x => x.Task.Guid == task.Guid).ToList();
                }

                return retval;
            }
        }
    }
}
