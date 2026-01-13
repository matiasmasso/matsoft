using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DTO
{
    public class DTOPaymentTerms
    {
        public CodsFormaDePago Cod { get; set; }
        public int Months { get; set; }
        public List<int> PaymentDays { get; set; }
        public PaymentDayCods PaymentDayCod { get; set; }
        public List<DTOVacacion> Vacaciones { get; set; }
        public DTOIban Iban { get; set; }
        public DTOBanc NBanc { get; set; }
        public List<Plazo> Plazos { get; set; }

        public enum CodsFormaDePago
        {
            notSet = 0,
            rebut = 1,
            reposicioFons = 2,
            comptat = 3,
            xerocopia = 4,
            domiciliacioBancaria = 5,
            transferencia = 6,
            rebutARep = 7,
            aNegociar = 9,
            efteAndorra = 10,
            transfPrevia = 11,
            diposit = 12
        }

        public enum PaymentDayCods
        {
            monthDay,
            weekDay
        }

        public DTOPaymentTerms() : base()
        {
            PaymentDays = new List<int>();
            Vacaciones = new List<DTOVacacion>();
            Plazos = new List<Plazo>();
        }

        public static DateTime Vto(DTOPaymentTerms oPaymentTerms, DateTime FromFch)
        {
            DateTime FchStep1 = VtoCheckMesos(oPaymentTerms, FromFch);
            DateTime FchStep2 = VtoCheckDiasDePago(oPaymentTerms, FchStep1);
            DateTime FchStep3 = VtoCheckVacances(oPaymentTerms, FchStep2);
            return FchStep3;
        }

        protected static DateTime VtoCheckMesos(DTOPaymentTerms oPaymentTerms, DateTime FromFch)
        {
            DateTime retval = FromFch.AddMonths(oPaymentTerms.Months);
            return retval;
        }

        protected static DateTime VtoCheckDiasDePago(DTOPaymentTerms oPaymentTerms, DateTime FromFch)
        {
            List<int> iPaymentDays = oPaymentTerms.PaymentDays;
            DateTime retval = FromFch;
            if (iPaymentDays != null)
            {
                if (iPaymentDays.Count > 0)
                {
                    int nextPaymentDay;
                    List<int> nextPaymentDays = iPaymentDays.Where(x => x >= FromFch.Day).ToList();
                    if (nextPaymentDays.Count == 0)
                    {
                        nextPaymentDay = iPaymentDays.Min();
                        retval = FromFch.AddMonths(1).AddDays(nextPaymentDay - FromFch.Day);
                    }
                    else
                    {
                        nextPaymentDay = nextPaymentDays.Min();
                        int lastDayOfMonth = FromFch.AddMonths(1).AddDays(-FromFch.Day).Day;
                        if (nextPaymentDay > lastDayOfMonth)
                            nextPaymentDay = lastDayOfMonth;
                        retval = FromFch.AddDays(nextPaymentDay - FromFch.Day);
                    }
                }
            }

            return retval;
        }

        protected static DateTime VtoCheckVacances(DTOPaymentTerms oPaymentTerms, DateTime SrcVto)
        {
            DateTime retval = DTOVacacion.Result(oPaymentTerms.Vacaciones, SrcVto);
            return retval;
        }



        public static string CfpText(DTOPaymentTerms.CodsFormaDePago oCod, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DTOValueNom oValueNom = Cods(oLang).Find(x => x.value == (int)oCod);
            if (oValueNom != null)
                sb.Append(Cods(oLang).Find(x => x.value == (int)oCod).nom);
            string retval = sb.ToString();
            return retval;
        }

        public static string Text(DTOPaymentTerms paymentTerms, DTOLang lang, string sVto = "")
        {
            string retval = "";
            if (paymentTerms != null)
            {
                if (string.IsNullOrEmpty(sVto))
                {
                    retval = TextHelper.StringListToMultiline(DTOPaymentTerms.ToList(paymentTerms, lang));
                }
                else
                {
                    retval = string.Format("{0} {1} {2}", DTOPaymentTerms.CfpText(paymentTerms.Cod, lang), lang.Tradueix("día", "dia", "due"), sVto);
                }
            }
            return retval;
        }

        public static List<string> ToList(DTOPaymentTerms paymentTerms, DTOLang lang)
        {
            List<string> retval = new List<string>();
            if (paymentTerms != null)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                if (paymentTerms.Cod > 0)
                    sb.Append(DTOPaymentTerms.CfpText(paymentTerms.Cod, lang) + " ");
                if (paymentTerms.Months > 0)
                    sb.Append(String.Format("{0} {1}", paymentTerms.Months * 30, lang.Tradueix("dias", "dies", "days")));
                else if (paymentTerms.Plazos.Count > 0)
                {
                    foreach (Plazo oPlazo in paymentTerms.Plazos)
                    {
                        if (oPlazo.Period != paymentTerms.Plazos.First().Period)
                            sb.Append(", ");
                        sb.Append(DTOPaymentTerms.PlazoText(oPlazo, lang));
                    }
                }

                if (paymentTerms.PaymentDays != null && paymentTerms.PaymentDays.Count > 0)
                    sb.AppendFormat(", {0}", DTOPaymentTerms.TextDias(paymentTerms, lang));


                string sFpg = sb.ToString();
                if (!string.IsNullOrEmpty(sFpg))
                    retval.Add(sFpg);

                if (paymentTerms.Vacaciones != null)
                {
                    foreach (DTOVacacion oItm in paymentTerms.Vacaciones)
                        retval.Add(DTOVacacion.Text(oItm, lang));
                }

            }
            return retval;
        }


        public static string VacacionsText(List<DTOVacacion> oVacacions, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DTOVacacion item in oVacacions)
                sb.AppendLine(VacacionText(item, oLang));
            string retval = sb.ToString();
            return retval;
        }

        public static string VacacionText(DTOVacacion item, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0} {1:00}/{2:00}", oLang.Tradueix("del", "del", "from"), item.MonthDayFrom.Day, item.MonthDayFrom.Month);
            sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.Tradueix("al", "al", "to"), item.MonthDayTo.Day, item.MonthDayTo.Month);
            if (item.MonthDayResult.Month == 0 & item.MonthDayResult.Day == 0)
                sb.Append(oLang.Tradueix(" aplaza 30 dias", " aplaça 30 dies", " add 30 days"));
            else
                sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.Tradueix("aplaza al", "aplaça al", "delay to"), item.MonthDayResult.Day, item.MonthDayResult.Month);
            string retval = sb.ToString();
            return retval;
        }


        public static string PlazoText(DTOPaymentTerms.Plazo oPlazo, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            string s = "";
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            switch (oPlazo.Period)
            {
                case DTOPaymentTerms.Plazo.Periods.d000:
                    {
                        s = oLang.Tradueix("a la vista", "a la vista", "at sight");
                        break;
                    }

                case DTOPaymentTerms.Plazo.Periods.d030:
                    {
                        s = oLang.Tradueix("30 dias", "30 dies", "30 days");
                        break;
                    }

                case DTOPaymentTerms.Plazo.Periods.d060:
                    {
                        s = oLang.Tradueix("60 dias", "60 dies", "60 days");
                        break;
                    }

                case DTOPaymentTerms.Plazo.Periods.d090:
                    {
                        s = oLang.Tradueix("90 dias", "90 dies", "90 days");
                        break;
                    }

                case DTOPaymentTerms.Plazo.Periods.d120:
                    {
                        s = oLang.Tradueix("120 dias", "120 dies", "120 days");
                        break;
                    }

                case DTOPaymentTerms.Plazo.Periods.d150:
                    {
                        s = oLang.Tradueix("150 dias", "150 dies", "150 days");
                        break;
                    }

                case DTOPaymentTerms.Plazo.Periods.d180:
                    {
                        s = oLang.Tradueix("180 dias", "180 dies", "180 days");
                        break;
                    }
            }
            return s;
        }
        public static List<DTOValueNom> Cods(DTOLang oLang)
        {
            var retval = new List<DTOValueNom>()
            {
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.notSet, oLang.Tradueix("(por asignar)", "(per asignar)", "(Not set)")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.rebut, oLang.Tradueix("recibo", "rebut", "receipt")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.reposicioFons, oLang.Tradueix("reposición fondos", "reposició de fons", "your check")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.comptat, oLang.Tradueix("contado", "comptat", "cash")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.xerocopia, oLang.Tradueix("efecto sin domiciliación", "efecte sense domiciliació", "bank draft")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.domiciliacioBancaria, oLang.Tradueix("efecto domiciliado Sepa Core", "efecte domiciliat Sepa Core", "Sepa Core bank draft")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.transferencia, oLang.Tradueix("transferencia", "transferència", "bank transfer")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.rebutARep, oLang.Tradueix("recibo a representante", "rebut a representant", "rep receipt")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.aNegociar, oLang.Tradueix("a convenir", "per convindre", "to be agreed")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.efteAndorra, oLang.Tradueix("efecto domiciliado en Andorra", "efecte domiciliat a Andorra", "Andorra bank draft")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.transfPrevia, oLang.Tradueix("transferencia previa", "transferència prèvia", "cash in advance")),
            new DTOValueNom((int)DTOPaymentTerms.CodsFormaDePago.diposit, oLang.Tradueix("a deducir de depósito", "a deduir de diposit", "to deduct from deposit"))
            };
            return retval;
        }

        public static string TextDias(DTOPaymentTerms oPaymentTerms, DTOLang oLang)
        {
            string s = "";
            if (oPaymentTerms.PaymentDays.Count > 0)
            {
                switch (oPaymentTerms.PaymentDayCod)
                {
                    case DTOPaymentTerms.PaymentDayCods.monthDay:
                        {
                            s = TextDiasDelMes(oPaymentTerms.PaymentDays, oLang);
                            break;
                        }

                    case DTOPaymentTerms.PaymentDayCods.weekDay:
                        {
                            s = TextDiasDeLaSemana(oPaymentTerms.PaymentDays, oLang);
                            break;
                        }
                }
            }
            return s;
        }

        public static string TextDiasDelMes(List<int> values, DTOLang oLang)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int day in values)
            {

                if (day == values.First())
                    sb.Append((values.Count == 1 ? oLang.Tradueix("dia", "dia", "day") : oLang.Tradueix("dias", "dies", "days")) + " ");
                else if (day == values.Last())
                    if (values.Count > 1)
                        sb.Append(oLang.Tradueix(" y ", " i ", " and "));
                    else
                        sb.Append(", ");

                sb.Append(day.ToString());
            }


            string retval = sb.ToString();
            return retval;
        }

        public static string TextDiasDeLaSemana(List<int> values, DTOLang oLang)
        {
            int i;
            string s = "";
            for (i = 0; i <= values.Count - 1; i++)
            {
                if (i == 0)
                {
                    s = oLang.Tradueix("dias", "dies", "days") + " ";
                }
                else if (i == values.Count - 1)
                {
                    s += oLang.Tradueix(" y ", " i ", " and ");
                }
                else
                {
                    s += ", ";
                }
                int iNumero = System.Convert.ToInt32(i / (double)7) + 1;
                switch (iNumero)
                {
                    case 0:
                        {
                            s += oLang.Tradueix("primer", "primer", "first");
                            break;
                        }

                    case 1:
                        {
                            s += oLang.Tradueix("segundo", "segon", "second");
                            break;
                        }

                    case 2:
                        {
                            s += oLang.Tradueix("tercer", "tercer", "third");
                            break;
                        }

                    case 3:
                        {
                            s += oLang.Tradueix("cuarto", "quart", "fourth");
                            break;
                        }

                    case 4:
                        {
                            s += oLang.Tradueix("último", "darrer", "last");
                            break;
                        }
                }

                int iWeekDay = i % 7;
                s = s + " " + oLang.WeekDay(iWeekDay);
                s = s + " " + oLang.Tradueix("del mes", "del mes", "each month");
            }
            return s;
        }

        public static string XMLEncoded(DTOPaymentTerms oPaymentTerms)
        {
            string retval = "";

            if (oPaymentTerms != null)
            {
                XmlElement oDoc; 
                XmlElement oNodePlazos;
                //XmlElement oNodeDias;
                XmlElement oNodeItm ;

                XmlDocument oXMLDoc = new XmlDocument();
                oDoc = oXMLDoc.CreateElement("FPG");
                oDoc.SetAttribute("MODO", ((int)oPaymentTerms.Cod).ToString());
                if (oPaymentTerms.Iban != null)
                    oDoc.SetAttribute("IBAN", oPaymentTerms.Iban.Digits);

                if (oPaymentTerms.NBanc != null)
                    oDoc.SetAttribute("NBANC", oPaymentTerms.NBanc.Guid.ToString());

                oXMLDoc.AppendChild(oDoc);

                if (oPaymentTerms.Plazos.Count > 0)
                {
                    oNodePlazos = oXMLDoc.CreateElement("PLAZO");
                    oDoc.AppendChild(oNodePlazos);


                    for (int i = 0; i < oPaymentTerms.Plazos.Count; i++)
                    {
                        DTOPaymentTerms.Plazo oPlazo = oPaymentTerms.Plazos[i];
                        oNodeItm = oXMLDoc.CreateElement("ITM");
                        oNodeItm.InnerText = oPlazo.Period.ToString();
                        oNodePlazos.AppendChild(oNodeItm);
                    }
                }
                retval = oXMLDoc.OuterXml;
            }
            return retval;
        }

        public static bool Match(DTOPaymentTerms oPaymentTerms1, DTOPaymentTerms oPaymentTerms2)
        {
            bool retval = (DTOPaymentTerms.XMLEncoded(oPaymentTerms1) == DTOPaymentTerms.XMLEncoded(oPaymentTerms2));
            return retval;
        }

        public class Plazo
        {
            public Periods Period { get; set; }
            public enum Periods
            {
                d000,
                d030,
                d060,
                d090,
                d120,
                d150,
                d180
            }

            public Plazo(Periods oPeriod = Periods.d000) : base()
            {
                Period = oPeriod;
            }
        }


    }
}
