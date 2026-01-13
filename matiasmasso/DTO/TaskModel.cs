using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class TaskModel : BaseGuid, IModel
    {
        public Cods Cod { get; set; }
        public string? Nom { get; set; }
        public string? Dsc { get; set; }
        public bool Enabled { get; set; }
        public Log? LastLog { get; set; }
        public DateTimeOffset? NotBefore { get; set; }
        public DateTimeOffset? NotAfter { get; set; }
        public List<Schedule> Schedules { get; set; } = new();
        public List<Exception> Exceptions { get; set; } = new();

        public enum Cods
        {
            NotSet,
            VivaceTransmisio,
            VtosUpdate,
            EmailStocks,
            WebAtlasUpdate,
            Avisame,
            CaducaCredits,
            PropersVencimentsClients,
            EdiReadFromInbox,
            EdiWriteToOutbox,
            EdiProcessaInbox,
            SorteoSetWinners,
            NotifyVtos,
            ImportWordPressCommentEmails,
            CurrencyExchangeRates,
            AmazonInvRpt,
            ArcPmcs,
            RequestForSupplierPurchaseOrder,
            StoreLocatorExcelMailing,
            SiiEmeses,
            SiiRebudes,
            EmailDescatalogats,
            BankTransferReminder,
            ElCorteInglesAlineacionDisponibilidad,
            MarketPlacesSync,
            Web2Sync
        }

        public enum ResultCods
        {
            Running,
            Success,
            Empty,
            DoneWithErrors,
            Failed
        }

        public static TaskModel Wellknown(Cods oCod)
        {
            TaskModel retval = null;
            switch (oCod)
            {
                case Cods.VivaceTransmisio:
                    {
                        retval = new TaskModel(new Guid("7CAC561F-37FB-4F42-B933-5B7F47B90F4F"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.EdiReadFromInbox:
                    {
                        retval = new TaskModel(new Guid("B56A4B69-B2A0-49C1-A415-6CE8594522C2"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.EdiWriteToOutbox:
                    {
                        retval = new TaskModel(new Guid("879AFDA9-6346-485B-A9A2-83C9C4211C72"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.EdiProcessaInbox:
                    {
                        retval = new TaskModel(new Guid("C5EACA6E-8A32-48C9-BC52-341A152EC524"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.NotifyVtos:
                    {
                        retval = new TaskModel(new Guid("7F9329FC-30D7-43A5-ACF3-F6A5EFABF6F2"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.BankTransferReminder:
                    {
                        retval = new TaskModel(new Guid("3B328318-D2F3-493B-ACE2-5EFC526DE57B"));
                        break;
                    }
                case Cods.ElCorteInglesAlineacionDisponibilidad:
                    {
                        retval = new TaskModel(new Guid("3E585674-1801-458D-91DD-98B110719DE0"));
                        break;
                    }
                case Cods.EmailDescatalogats:
                    {
                        retval = new TaskModel(new Guid("D0B9D76A-86E1-4A69-A96D-DFCD1B9A675C"));
                        break;
                    }
            }
            return retval;
        }

        public TaskModel() : base()
        {
            Schedules = new List<Schedule>();
            Exceptions = new List<Exception>();
        }

        public TaskModel(Guid oGuid) : base(oGuid)
        {
            Schedules = new List<Schedule>();
            Exceptions = new List<Exception>();
        }

        public bool IsDue()
        {
            // If _Cod = 9 Then Stop '===================================================================
            bool retval = false;
            if (Enabled)
            {
                if (LastLog == null || LastLog.ResultCod != ResultCods.Running)
                {
                    var timeToNextRun = TimeToNextRun();
                    if (timeToNextRun != null)
                        retval = ((TimeSpan)timeToNextRun).TotalMinutes <= 0;
                }
            }
            return retval;
        }

        public TimeSpan? TimeToNextRun()
        {
            TimeSpan? retval = null;
            var oNextRun = NextRun();
            if (oNextRun != null)
                retval = oNextRun - DateTime.Now;
            return retval;
        }

        public TimeSpan? TimeToNextRun(Schedule oSchedule)
        {
            DateTimeOffset? dtNextRun = NextRun(oSchedule);
            DateTime dtNow = DateTime.Now;
            if (dtNow > dtNextRun)
                dtNextRun = dtNow;
            TimeSpan? oRetVal = dtNextRun - dtNow;
            return oRetVal;
        }

        public DateTimeOffset? NextRun()
        {
            DateTimeOffset? retval = null;
            List<Schedule> oEnabledSchedules = Schedules.Where(x => x.Enabled).ToList();
            if (oEnabledSchedules.Count > 0)
            {
                retval = oEnabledSchedules.Min(x => NextRun(x));

                if (NotAfter != null)
                {
                    if (retval > NotAfter)
                        retval = null;
                }
            }
            return retval;
        }

        public DateTimeOffset? NextRun(Schedule oSchedule)
        {
            DateTimeOffset? retval = null;

            DateTimeOffset? DtFch = NextAllowedFch(oSchedule);

            switch (oSchedule.Mode)
            {
                case Schedule.Modes.Interval:
                    {
                        if (LastLog != null)
                        {
                            bool blSwitch = (LastLog.Fch > DtFch);
                            if (blSwitch)
                                DtFch = LastLog.Fch;
                        }
                        if (DtFch == null)
                            retval = DateTime.Now;
                        else
                        {
                            retval = ((DateTimeOffset)DtFch).AddHours((double?)oSchedule.Hours() ?? 0).AddMinutes((double?)oSchedule.Minutes() ?? 0);
                            if (retval < DateTime.Now)
                                retval = DateTime.Now;
                        }

                        break;
                    }

                default:
                    {
                        var oTimeZoneBcn = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
                        if (DtFch != null)
                        {
                            DateTime targetFch = new DateTime(((DateTimeOffset)DtFch).Year, ((DateTimeOffset)DtFch).Month, ((DateTimeOffset)DtFch).Day, oSchedule.Hours() ?? 0, oSchedule.Minutes() ?? 0, 0);
                            retval = new DateTimeOffset(targetFch, oTimeZoneBcn.GetUtcOffset((DateTimeOffset)DtFch));
                        }
                        break;
                    }
            }

            return retval;
        }


        public DateTimeOffset NextAllowedFch(Schedule oSchedule)
        {
            DateTimeOffset? DtLastRun;
            if (LastLog != null)
                DtLastRun = LastLog.Fch;

            DateTimeOffset retval = DateTime.Today;
            if (NotBefore != null && NotBefore > retval)
                retval = (DateTimeOffset)NotBefore;

            for (int i = 0; i <= 6; i++)
            {
                if (oSchedule?.WeekDays?[(int)retval.DayOfWeek] == '1')
                    break;
                else
                    retval = retval.AddDays(1);
            }

            return retval;
        }

        public void SetResult(List<Exception> exs)
        {
            if (exs.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var ex in exs)
                {
                    if (!string.IsNullOrEmpty(ex.Message))
                    {
                        Exceptions.Add(ex);
                        sb.AppendLine(ex.Message);
                    }
                }
                LastLog.ResultMsg += sb.ToString(); // evita maxacar-ho si l'hem assignat durant la tasca
            }

            if (LastLog.ResultCod == TaskModel.ResultCods.Running)
                LastLog.ResultCod = exs.Count == 0 ? ResultCods.Success : ResultCods.Failed;

            LastLog.Fch = DateTime.Now;
        }

        public void SetResult(Result oTaskResult)
        {
            {
                var withBlock = LastLog;
                withBlock.ResultMsg = oTaskResult.Msg;
                withBlock.ResultCod = oTaskResult.ResultCod;
                withBlock.Fch = DateTime.Now;
            }

            Exceptions.AddRange(oTaskResult.Exceptions);
        }



        public static string Report(List<TaskModel> oTasks)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oTasks.Count == 0)
                sb.AppendLine("No hi ha cap tasca per executar");
            else
            {
                var failedTasks = oTasks.Where(x => x.LastLog.ResultCod == TaskModel.ResultCods.Failed | x.LastLog.ResultCod == TaskModel.ResultCods.DoneWithErrors);
                if (failedTasks.Count() == 0)
                {
                    sb.AppendLine(string.Format("Executades {0} tasques correctament:", oTasks.Count));
                    foreach (var oTask in oTasks)
                        sb.AppendLine(oTask.Cod.ToString() + ": " + oTask.LastLog.ResultMsg);
                }
                else
                {
                    sb.AppendLine(string.Format("Error al executar {0} de {1} tasques:", failedTasks.Count(), oTasks.Count));
                    foreach (var oTask in oTasks)
                    {
                        sb.AppendLine(oTask.Cod.ToString() + ": " + oTask.LastLog.ResultCod.ToString() + " - " + oTask.LastLog.ResultMsg);
                        foreach (var ex in oTask.Exceptions)
                            sb.AppendLine("    " + ex.Message);
                    }
                }
            }

            return sb.ToString();
        }



        public override string ToString()
        {
            return Nom ?? "?";
        }

        public class Schedule : BaseGuid, IModel
        {
            public TaskModel Task { get; set; }
            public bool Enabled { get; set; }
            public Modes Mode { get; set; }
            public string? TimeInterval { get; set; } //ISO8601
            public string WeekDays { get; set; } = "0000000";

            // versió de TimeInterval per enviar en JSON per la Api
            //public String? ISO8601
            //{
            //    get
            //    {
            //        //return TimeHelper.ISO8601(TimeInterval);
            //        return TimeInterval == null ? null : System.Xml.XmlConvert.ToString((TimeSpan)TimeInterval);
            //    }
            //    set
            //    {
            //        //TimeInterval = TimeHelper.fromISO8601(value);
            //        TimeInterval = value == null ? null : System.Xml.XmlConvert.ToTimeSpan(value);
            //    }
            //}

            public enum Modes
            {
                GivenTime,
                Interval
            }


            public Schedule() : base()
            {
                //WeekDays = new[] { true, true, true, true, true, true, true };
            }

            public Schedule(Guid oGuid) : base(oGuid)
            {
                //WeekDays = new[] { true, true, true, true, true, true, true };
            }


            public int? Hours() => Mode == Modes.GivenTime ? Hours(TimeInterval) : null;
            public int? Minutes() => Mode == Modes.GivenTime ? Minutes(TimeInterval) : null;
            public int? IntervalTotalMinutes() => Mode == Modes.Interval ? IntervalTotalMinutes(TimeInterval) : null;

            public int? IntervalTotalMinutes(string? timeInterval)
            {
                var hours = Hours(timeInterval) ?? 0;
                var minutes = Minutes(timeInterval) ?? 0;
                return 60 * hours + minutes;
            }

            public static int? Hours(string? timeInterval)
            {
                int? retval = null;
                var hPos = timeInterval?.IndexOf("H");
                if ((timeInterval?.StartsWith("PT") ?? false) && hPos > 0)
                {
                    retval = int.Parse(timeInterval.Substring(2, (int)hPos! - 2));
                }
                return retval;
            }

            public static int? Minutes(string? timeInterval)
            {
                int? retval = null;
                var mPos = timeInterval?.IndexOf("M");
                if ((timeInterval?.StartsWith("PT") ?? false) && mPos > 0)
                {
                    var hPos = timeInterval?.IndexOf("H");
                    if (hPos > 0)
                        retval = int.Parse(timeInterval!.Substring((int)hPos + 1, (int)mPos! - (int)hPos - 1));
                    else
                        retval = int.Parse(timeInterval!.Substring(2, (int)mPos! - 2));
                }
                return retval;
            }

            public string FrequencyText(LangDTO? lang = null)
            {
                string retval = "";
                lang = lang ?? LangDTO.Default();
                switch (this.Mode)
                {
                    case Schedule.Modes.Interval:
                        {
                            retval = GetIntervalText(lang);
                            break;
                        }

                    default:
                        {
                            retval = $"{GetWeekDaysText(lang)} a les {(Hours() ?? 0):00}:{(Minutes() ?? 0):00}";
                            break;
                        }
                }
                return retval;
            }


            private string GetIntervalText(LangDTO lang )
            {
                string retval = "";
                var minutes = IntervalTotalMinutes();
                if (minutes == 1)
                    retval = lang.Tradueix("cada minuto", "cada minut", "each minute");
                else if (minutes == 30)
                {
                    retval = lang.Tradueix("cada media hora","cada mitja hora","every half an hour");
                }
                else if (minutes == 60)
                {
                    retval = lang.Tradueix("cada hora","cada hora","each hour");
                }
                else
                    retval = lang.Tradueix($"cada {(minutes ?? 0)} minutos", $"cada {(minutes ?? 0)} minuts", $"each {(minutes ?? 0)} minutes");
                return retval;
            }

            public bool IsWeekdayEnabled(int i) => WeekDays[i] != 0;

            private string GetWeekDaysText(LangDTO lang)
            {
                LangDTO oLang = lang ?? new LangDTO(LangDTO.Ids.CAT);
                List<int> DaysEnabled = new();
                List<int> DaysDisabled = new();

                //base 0 = sunday
                for (int i = 0; i < 7; i++)
                {
                    if (WeekDays?[i] == '1')
                        DaysEnabled.Add(i);
                    else
                        DaysDisabled.Add(i);
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (DaysEnabled.Count >= DaysDisabled.Count)
                {
                    if (DaysDisabled.Count == 0)
                        sb.Append("tots els dies de la setmana");
                    else
                    {
                        sb.Append("tots els dies menys ");
                        sb.Append(oLang.Weekday(DaysDisabled[0]));
                        if (DaysDisabled.Count > 1)
                        {
                            if (DaysDisabled.Count == 2)
                                sb.Append(" i ");
                            else
                                sb.Append(", ");
                            sb.Append(oLang.Weekday(DaysDisabled[1]));
                            if (DaysDisabled.Count > 2)
                            {
                                sb.Append(" i ");
                                _ = sb.Append(oLang.Weekday(DaysDisabled[2]));
                            }
                        }
                    }
                }
                else if (DaysEnabled.Count == 0)
                    sb.Append("(cap dia)");
                else
                {
                    sb.Append("cada ");
                    sb.Append(oLang.Weekday((int)DaysEnabled[0]));
                    if (DaysEnabled.Count > 1)
                    {
                        if (DaysEnabled.Count == 2)
                            sb.Append(" i ");
                        else
                            sb.Append(", ");
                        sb.Append(oLang.Weekday((int)DaysEnabled[1]));
                        if (DaysEnabled.Count > 2)
                        {
                            sb.Append(" i ");
                            sb.Append(oLang.Weekday((int)DaysEnabled[2]));
                        }
                    }
                }

                string retVal = sb.ToString();
                return retVal;
            }



            public static string SpanText(DateTime DtFch, LangDTO oLang)
            {
                string retval = "";
                if (DtFch != default(DateTime))
                {
                    TimeSpan oTimeSpan = DtFch - DateTime.Now;
                    retval = SpanText(oTimeSpan, oLang);
                }
                return retval;
            }


            public static string SpanText(TimeSpan oTimeSpan, LangDTO oLang)
            {
                string retval = "";
                if (oTimeSpan != default(TimeSpan))
                {
                    if (oTimeSpan.Days > 0)
                        retval = string.Format("d'aqui a {0} dies {1} hores {2} min {3}", oTimeSpan.Days, oTimeSpan.Hours, oTimeSpan.Minutes, oTimeSpan.Seconds);
                    else if (oTimeSpan.Hours > 2)
                        retval = string.Format("d'aqui a {0} hores {1} min {2}", oTimeSpan.Hours, oTimeSpan.Minutes, oTimeSpan.Seconds);
                    else if (oTimeSpan.Hours > 1)
                        retval = string.Format("d'aqui a 1 hora {0} min {1}", oTimeSpan.Minutes, oTimeSpan.Seconds);
                    else if (oTimeSpan.Minutes > 0)
                        retval = string.Format("d'aqui a {0} minuts {1}", oTimeSpan.Minutes, oTimeSpan.Seconds);
                    else if (oTimeSpan.Seconds > 0)
                        retval = string.Format("d'aqui a {0} segons", oTimeSpan.Seconds);
                    else
                        retval = "inminent";
                }
                return retval;
            }

            public string EncodedWeekdays()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //foreach (bool item in WeekDays)
                //    sb.Append(item ? "1" : "0");
                string retval = sb.ToString();
                return retval;
            }

            public static bool[] DecodedWeekdays(string src)
            {
                bool[] retval = new[] { (src.Substring(0, 1) == "1"), (src.Substring(1, 1) == "1"), (src.Substring(2, 1) == "1"), (src.Substring(3, 1) == "1"), (src.Substring(4, 1) == "1"), (src.Substring(5, 1) == "1"), (src.Substring(6, 1) == "1") };
                return retval;
            }
        }


        public class Log : BaseGuid
        {
            public DateTimeOffset? Fch { get; set; }
            public TaskModel.ResultCods? ResultCod { get; set; }
            public string? ResultMsg { get; set; }

            public Log() : base()
            {
            }

            public Log(Guid oGuid) : base(oGuid)
            {
            }

            public static Log Factory()
            {
                Log retval = new Log();
                {
                    retval.ResultCod = TaskModel.ResultCods.Running;
                    retval.Fch = DateTime.Now;
                }
                return retval;
            }

            public void Merge(Log aux)
            {
                SetResult((ResultCods)aux.ResultCod!, aux.ResultMsg ?? "");
            }

            public void SetResult(TaskModel.ResultCods cod, string msg, List<Exception> exs = null)
            {
                switch (cod)
                {
                    case TaskModel.ResultCods.Empty:
                        this.ResultCod = cod;
                        break;
                    case TaskModel.ResultCods.Running:
                        this.ResultCod = cod;
                        break;
                    case TaskModel.ResultCods.DoneWithErrors:
                        if (cod == TaskModel.ResultCods.Failed)
                            this.ResultCod = cod;
                        break;
                    case TaskModel.ResultCods.Failed:
                        break;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    if (!string.IsNullOrEmpty(ResultMsg))
                        ResultMsg += Environment.NewLine;
                    ResultMsg += msg;
                }

                if (exs != null)
                {
                    foreach (Exception ex in exs)
                    {
                        ResultMsg += Environment.NewLine;
                        ResultMsg += ex.Message;
                    }
                }
            }
        }

        public class Result
        {
            public TaskModel.ResultCods ResultCod { get; set; }
            public string Msg { get; set; }
            public List<Exception> Exceptions { get; set; }

            public Result() : base()
            {
                Exceptions = new List<Exception>();
            }

            public static Result Factory(TaskModel.ResultCods oResultCod, string msg = "", List<Exception> exs = null)
            {
                Result retval = new Result();
                retval.ResultCod = oResultCod;
                retval.Msg = msg;
                if (exs != null)
                    retval.AddExceptions(exs);
                return retval;
            }

            public bool Success()
            {
                bool retval = false;
                switch (ResultCod)
                {
                    case TaskModel.ResultCods.Success:
                    case TaskModel.ResultCods.Empty:
                        {
                            retval = true;
                            break;
                        }
                }
                return retval;
            }

            public static Result SuccessResult(string msg = "")
            {
                Result retval = new Result();
                retval.ResultCod = TaskModel.ResultCods.Success;
                retval.Msg = msg;
                return retval;
            }

            public static Result FailResult(List<System.Exception> exs, string msg = "")
            {
                Result retval = new Result();
                {
                    var withBlock = retval;
                    withBlock.ResultCod = TaskModel.ResultCods.Failed;
                    withBlock.AddExceptions(exs);
                    withBlock.Msg = msg;
                }
                return retval;
            }

            public void Fail()
            {
                ResultCod = TaskModel.ResultCods.Failed;
            }

            public void Fail(string stringFormatMsg, params string[] stringFormatValues)
            {
                Fail();
                if (stringFormatValues.Length > 0)
                    Msg = string.Format(stringFormatMsg, stringFormatValues);
                else
                    Msg = stringFormatMsg;
            }

            public void Fail(System.Exception ex, string stringFormatMsg, params string[] stringFormatValues)
            {
                Fail(stringFormatMsg, stringFormatValues);
                AddException(ex);
            }

            public void Fail(IEnumerable<System.Exception> exs, string stringFormatMsg, params string[] stringFormatValues)
            {
                Fail(stringFormatMsg, stringFormatValues);
                AddExceptions(exs);
            }

            public List<System.Exception> SystemExceptions()
            {
                List<System.Exception> retval = new List<System.Exception>();
                foreach (Exception ex in Exceptions)
                    retval.Add(new System.Exception(ex.Message));
                return retval;
            }




            public void Succeed()
            {
                ResultCod = TaskModel.ResultCods.Success;
            }

            public void Succeed(string stringFormatMsg, params string[] stringFormatValues)
            {
                Succeed();
                if (stringFormatValues.Length > 0)
                    Msg = string.Format(stringFormatMsg, stringFormatValues);
                else
                    Msg = stringFormatMsg;
            }

            public void Empty()
            {
                ResultCod = TaskModel.ResultCods.Empty;
            }

            public void Empty(string stringFormatMsg, params string[] stringFormatValues)
            {
                Empty();
                if (stringFormatValues.Length > 0)
                    Msg = string.Format(stringFormatMsg, stringFormatValues);
                else
                    Msg = stringFormatMsg;
            }

            public void AddException(string stringFormatMsg, params string[] stringFormatValues)
            {
                Exception ex = null;
                if (stringFormatValues.Length > 0)
                    ex = new Exception(string.Format(stringFormatMsg, stringFormatValues));
                else
                    ex = new Exception(stringFormatMsg);
                Exceptions.Add(ex);
                if (ResultCod == TaskModel.ResultCods.Success)
                    ResultCod = TaskModel.ResultCods.DoneWithErrors;
            }

            public void AddException(System.Exception ex)
            {
                AddException(ex.Message);
            }

            public void AddExceptions(IEnumerable<System.Exception> exs)
            {
                foreach (var ex in exs)
                    AddException(ex.Message);
            }

            public void DoneWithErrors()
            {
                ResultCod = TaskModel.ResultCods.DoneWithErrors;
            }

            public void DoneWithErrors(string stringFormatMsg, params string[] stringFormatValues)
            {
                DoneWithErrors();
                if (stringFormatValues.Length > 0)
                    Msg = string.Format(stringFormatMsg, stringFormatValues);
                else
                    Msg = stringFormatMsg;
            }

            public string ResultReport()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("Cod: " + ResultCod.ToString());
                sb.AppendLine("Msg: " + Msg);
                foreach (var ex in Exceptions)
                    sb.AppendLine(ex.Message);
                string retval = sb.ToString();
                return retval;
            }


        }


    }


}



