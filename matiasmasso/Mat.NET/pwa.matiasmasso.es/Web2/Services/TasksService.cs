using DTO;

namespace Web.Services
{
    public class TasksService
    {
        private AppStateService appstate;

        public TasksService(AppStateService appstate)
        {
            this.appstate = appstate;
        }

        public async Task<List<TaskModel>?> FetchAsync()
        {
            return await appstate.GetAsync<List<TaskModel>>("tasks");
        }

        public async Task UpdateScheduleAsync(TaskModel.Schedule value)
        {
            await appstate.PostAsync<TaskModel.Schedule, bool>(value, "task/schedule");
        }
        public async Task DeleteScheduleAsync(TaskModel.Schedule value)
        {
            await appstate.GetAsync<bool>("task/schedule/delete", value.Guid.ToString());
        }

        public GlobalComponents.Icon.Ids Icon(TaskModel item)
        {
            GlobalComponents.Icon.Ids retval;
            switch (item.LastLog?.ResultCod)
            {
                case TaskModel.ResultCods.Running:
                    retval = GlobalComponents.Icon.Ids.AlarmClock;
                    break;
                case TaskModel.ResultCods.Success:
                    retval = GlobalComponents.Icon.Ids.CheckGreenCircle;
                    break;
                case TaskModel.ResultCods.Empty:
                    retval = GlobalComponents.Icon.Ids.DoNotEnter;
                    break;
                case TaskModel.ResultCods.DoneWithErrors:
                    retval = GlobalComponents.Icon.Ids.Warning;
                    break;
                case TaskModel.ResultCods.Failed:
                    retval = GlobalComponents.Icon.Ids.Xmark;
                    break;
                default:
                    retval = GlobalComponents.Icon.Ids.None;
                    break;
            }
            return retval;
        }
    }
}
