using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOTask : DTOBaseGuid
    {
        public Cods Cod { get; set; }
        public string Nom { get; set; }
        public string Dsc { get; set; }
        public bool Enabled { get; set; }
        public DTOTaskLog LastLog { get; set; }
        public DateTimeOffset NotBefore { get; set; }
        public DateTimeOffset NotAfter { get; set; }
        public List<DTOTaskSchedule> Schedules { get; set; }
        public List<Exception> Exceptions { get; set; }

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
            Web2Sync,
            Tamariu
        }

        public enum ResultCods
        {
            Running,
            Success,
            Empty,
            DoneWithErrors,
            Failed
        }

        public static DTOTask Wellknown(Cods oCod)
        {
            DTOTask retval = null;
            switch (oCod)
            {
                case Cods.VivaceTransmisio:
                    {
                        retval = new DTOTask(new Guid("7CAC561F-37FB-4F42-B933-5B7F47B90F4F"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.EdiReadFromInbox:
                    {
                        retval = new DTOTask(new Guid("B56A4B69-B2A0-49C1-A415-6CE8594522C2"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.EdiWriteToOutbox:
                    {
                        retval = new DTOTask(new Guid("879AFDA9-6346-485B-A9A2-83C9C4211C72"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.EdiProcessaInbox:
                    {
                        retval = new DTOTask(new Guid("C5EACA6E-8A32-48C9-BC52-341A152EC524"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.NotifyVtos:
                    {
                        retval = new DTOTask(new Guid("7F9329FC-30D7-43A5-ACF3-F6A5EFABF6F2"));
                        retval.Cod = oCod;
                        break;
                    }

                case Cods.BankTransferReminder:
                    {
                        retval = new DTOTask(new Guid("3B328318-D2F3-493B-ACE2-5EFC526DE57B"));
                        break;
                    }
                case Cods.ElCorteInglesAlineacionDisponibilidad:
                    {
                        retval = new DTOTask(new Guid("3E585674-1801-458D-91DD-98B110719DE0"));
                        break;
                    }
                case Cods.EmailDescatalogats:
                    {
                        retval = new DTOTask(new Guid("D0B9D76A-86E1-4A69-A96D-DFCD1B9A675C"));
                        break;
                    }

                case Cods.Tamariu:
                    {
                        retval = new DTOTask(new Guid("AE956D1C-4CAC-44F9-B973-3D0F60F38E0F"));
                        break;
                    }
            }
            return retval;
        }

        public DTOTask() : base()
        {
            Schedules = new List<DTOTaskSchedule>();
            Exceptions = new List<Exception>();
        }

        public DTOTask(Guid oGuid) : base(oGuid)
        {
            Schedules = new List<DTOTaskSchedule>();
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
                    int iMinutesToRun = (int)TimeToNextRun().TotalMinutes;
                    retval = (iMinutesToRun <= 0);
                }
            }
            return retval;
        }

        public TimeSpan TimeToNextRun()
        {
            var oNextRun = NextRun();
            TimeSpan oRetVal = oNextRun - DateTimeOffset.Now;
            return oRetVal;
        }

        public TimeSpan TimeToNextRun(DTOTaskSchedule oSchedule)
        {
            DateTimeOffset dtNextRun = NextRun(oSchedule);
            DateTimeOffset dtNow = DateTimeOffset.Now;
            if (dtNow > dtNextRun)
                dtNextRun = dtNow;
            TimeSpan oRetVal = dtNextRun - dtNow;
            return oRetVal;
        }

        public DateTimeOffset NextRun()
        {
            DateTimeOffset retval;
            List<DTOTaskSchedule> oEnabledSchedules = Schedules.Where(x => x.Enabled).ToList();
            if (oEnabledSchedules.Count > 0)
            {
                retval = oEnabledSchedules.Min(x => NextRun(x));

                if (NotAfter != default(DateTimeOffset))
                {
                    if (retval > NotAfter)
                        retval = default(DateTimeOffset);
                }
            }
            return retval;
        }

        public DateTimeOffset NextRun(DTOTaskSchedule oSchedule)
        {
            DateTimeOffset retval;

            DateTimeOffset DtFch = NextAllowedFch(oSchedule);

            switch (oSchedule.Mode)
            {
                case DTOTaskSchedule.Modes.Interval:
                    {
                        if (LastLog != null)
                        {
                            bool blSwitch = (LastLog.Fch > DtFch);
                            if (blSwitch)
                                DtFch = LastLog.Fch;
                        }
                        if (DtFch == DateTimeOffset.MinValue)
                            retval = DateTimeOffset.Now;
                        else
                        {
                            retval = DtFch.AddMinutes(oSchedule.TimeInterval.TotalMinutes);
                            if (retval < DateTimeOffset.Now)
                                retval = DateTimeOffset.Now;
                        }

                        break;
                    }

                default:
                    {
                        var oTimeZoneBcn = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
                        DateTimeOffset targetFch = new DateTimeOffset(DtFch.Year, DtFch.Month, DtFch.Day, oSchedule.TimeInterval.Hours, oSchedule.TimeInterval.Minutes, 0, oTimeZoneBcn.GetUtcOffset(DtFch));
                        retval = targetFch; // New DateTimeOffset(targetFch, oTimeZoneBcn.GetUtcOffset(DtFch))
                        break;
                    }
            }

            return retval;
        }


        public DateTimeOffset NextAllowedFch(DTOTaskSchedule oSchedule)
        {
            DateTimeOffset DtLastRun;
            if (LastLog != null)
                DtLastRun = LastLog.Fch;

            DateTimeOffset retval = DTO.GlobalVariables.Today();
            if (NotBefore > retval)
                retval = NotBefore;

            for (int i = 0; i <= 6; i++)
            {
                if (oSchedule.WeekDays[(int)retval.DayOfWeek])
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
                    if (ex.Message.isNotEmpty())
                    {
                        Exceptions.Add(ex);
                        sb.AppendLine(ex.Message);
                    }
                }
                LastLog.ResultMsg += sb.ToString(); // evita maxacar-ho si l'hem assignat durant la tasca
            }

            if (LastLog.ResultCod == DTOTask.ResultCods.Running)
                LastLog.ResultCod = exs.Count == 0 ? ResultCods.Success : ResultCods.Failed;

            LastLog.Fch = DateTimeOffset.Now;
        }

        public void SetResult(DTOTaskResult oTaskResult)
        {
            {
                var withBlock = LastLog;
                withBlock.ResultMsg = oTaskResult.Msg;
                withBlock.ResultCod = oTaskResult.ResultCod;
                withBlock.Fch = DateTimeOffset.Now;
            }

            Exceptions.AddRange(oTaskResult.Exceptions);
        }



        public static string Report(List<DTOTask> oTasks)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oTasks.Count == 0)
                sb.AppendLine("No hi ha cap tasca per executar");
            else
            {
                var failedTasks = oTasks.Where(x => x.LastLog.ResultCod == DTOTask.ResultCods.Failed | x.LastLog.ResultCod == DTOTask.ResultCods.DoneWithErrors);
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
    }
}
