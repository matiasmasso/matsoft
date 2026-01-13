using DTO;
using MatHelperStd;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;

public class DTORaffle : DTOBaseGuid
{

    public class Compact
    {
        public Guid Guid { get; set; }
        public String Title { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public DTORaffleParticipant.Compact Winner { get; set; }

        public string BannerUrl { get; set; }
    }

    public DTOLang Lang { get; set; }
    public DTOCountry Country { get; set; }
    public string Title { get; set; } = "";
    public string Bases { get; set; }
    public DateTime FchFrom { get; set; } = DateTime.MinValue;
    public DateTime FchTo { get; set; } = DateTime.MinValue;
    public string UrlExterna { get; set; }
    public DTOAmt CostPrize { get; set; }
    public DTOAmt CostPubli { get; set; }

    [JsonIgnore]
    public Byte[] ImageFbFeatured { get; set; }
    [JsonIgnore]
    public Byte[] ImageBanner600 { get; set; }
    [JsonIgnore]
    public Byte[] ImageCallToAction500 { get; set; }
    public bool Visible { get; set; }



    public DTOProduct Product { get; set; }
    public DTOProductBrand Brand { get; set; }
    public DTORaffleParticipant Winner { get; set; }
    public string Question { get; set; } = null;
    public List<string> Answers { get; set; }
    public int RightAnswer { get; set; }
    public List<DTORaffleParticipant> Participants { get; set; }

    public int ParticipantsCount { get; set; }
    public int NewParticipantsCount { get; set; }

    [JsonIgnore]
    public Byte[] ImageWinner { get; set; }
    public DateTime FchWinnerReaction { get; set; }
    public DateTime FchDistributorReaction { get; set; }
    public DateTime FchDelivery { get; set; }
    public DateTime FchPicture { get; set; }
    public DTODelivery Delivery { get; set; }

    public int Shares { get; set; }

    public const int BANNER_WIDTH = 600;
    public const int BANNER_HEIGHT = 200;
    public const int THUMBNAILWIDTH = 325;
    public const int THUMBNAILHEIGHT = 205;

    public enum Statuses
    {
        NoReactionYet,
        WinnerReacted,
        DistributorReacted,
        Delivered,
        WinnerPictureSubmitted
    }


    public DTORaffle() : base()
    {
    }

    public DTORaffle(Guid oGuid) : base(oGuid)
    {
    }

    public static DTORaffle Factory(DTOLang oLang)
    {
        DTORaffle retval = new DTORaffle();
        {
            var withBlock = retval;
            withBlock.Answers = new List<string>();
            withBlock.FchFrom = DTO.GlobalVariables.Today();
            withBlock.FchTo = DTO.GlobalVariables.Today();
            withBlock.RightAnswer = 0;
            withBlock.Visible = true;
            withBlock.Lang = oLang;
            withBlock.Country = DTORaffle.CountryFromLang(withBlock.Lang);
            withBlock.Title = "(nou sorteig)";
            withBlock.Question = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo.";
            withBlock.Bases = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo.";
        }
        return retval;
    }


    public static DTOCountry CountryFromLang(DTOLang oLang)
    {
        DTOCountry retval = null/* TODO Change to default(_) if this is not a reference type */;
        switch (oLang.id)
        {
            case DTOLang.Ids.POR:
                {
                    retval = DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal);
                    break;
                }

            default:
                {
                    retval = DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain);
                    break;
                }
        }
        return retval;
    }

    public static string TimeToStart(DTORaffle oRaffle, DTOLang oLang)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(oLang.Tradueix("Próximo sorteo ", "Proper sorteig ", "Next raffle ", "Próximo sorteio "));
        TimeSpan oSpan = oRaffle.FchFrom - DTO.GlobalVariables.Now();
        int iMinutes = (int)oSpan.TotalMinutes;
        int iHours = (int)(iMinutes / (double)60);
        iMinutes = iMinutes - (iHours * 60);
        int iDays = (int)(iHours / (double)24);
        iHours = iHours - (iDays * 24);
        if (iDays > 0)
        {
            if (iDays == 1)
                sb.Append(oLang.Tradueix("mañana a medianoche.", "demà a mitja nit.", "tomorrow midnight.", "amanhã, meia-noite."));
            else
                sb.Append(oLang.Tradueix("en " + iDays + " dias.", "en " + iDays + " dies.", "on " + iDays + " days.", "em " + iDays + " dias."));
        }
        else if (iHours > 0)
        {
            int value = iHours + 1;
            sb.Append(oLang.Tradueix("en menos de " + value + " horas.", "en menys de " + value + " hores.", "in less than " + value + " hours.", "em menos de " + value + " horas."));
        }
        else
            sb.Append(oLang.Tradueix("en " + iMinutes + " minutos.", "en " + iMinutes + " minuts.", "in " + iMinutes + " minutes.", "em " + iMinutes + " minutos."));
        string retval = sb.ToString();
        return retval;
    }

    public static string RaffleTime(DTORaffle oRaffle, DTOLang oLang)
    {
        // ej: el ganador se publicará mañana viernes a media noche
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(oLang.Tradueix("El ganador se publicará ", "El guanyador es publicará ", "Winner to be published "));
        if ((oRaffle.FchTo - DTO.GlobalVariables.Now()).TotalHours < 48)
        {
            sb.AppendLine(oLang.Tradueix("mañana ", "demà ", "tomorrow "));
            sb.AppendLine(oLang.WeekDay(oRaffle.FchTo).ToLower());
            sb.AppendLine(oLang.Tradueix(" a media noche.", " a mitja nit.", " midnight."));
        }
        else
        {
            sb.AppendLine(oLang.Tradueix(" a media noche del ", " a mitja nit de ", " on "));
            sb.AppendLine(DTOLang.ESP().WeekDay(oRaffle.FchTo).ToLower());
            sb.AppendLine(oLang.Tradueix(".", ".", " midnight."));
        }
        string retval = sb.ToString();
        return retval;
    }

    public static DTOGrf GrfEnrollment(DTORaffle oRaffle)
    {
        DTOGrf retval = new DTOGrf();
        {
            var withBlock = retval;
            withBlock.FchFrom = new DateTime(oRaffle.FchFrom.Year, oRaffle.FchFrom.Month, oRaffle.FchFrom.Day, 0, 0, 0);
            withBlock.FchTo = new DateTime(oRaffle.FchTo.Year, oRaffle.FchTo.Month, oRaffle.FchTo.Day, 0, 0, 0).AddDays(1).AddMilliseconds(-1);
            withBlock.DateInterval = TimeHelper.DateIntervals.Hour;
            withBlock.Items = new List<DTOGrfItem>();
        }

        if (oRaffle.Participants != null)
        {
            foreach (var oParticipant in oRaffle.Participants)
            {
                DTOGrfItem oItem = new DTOGrfItem();
                oItem.Fch = oParticipant.Fch;
                retval.Items.Add(oItem);
            }
        }
        return retval;
    }

    public static DTORaffleParticipant GetRandomWinner(List<DTORaffleParticipant> oValidParticipants)
    {
        DTORaffleParticipant retval = null;
        if (oValidParticipants.Count > 0)
        {
            int iWinner = NumericHelper.RandomNumber(oValidParticipants.Count - 1);
            retval = oValidParticipants[iWinner];
        }
        return retval;
    }

    public static string WinnerFullNom(DTORaffle oRaffle)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (oRaffle.Winner != null)
        {
            var oUser = oRaffle.Winner.User;
            if (oUser != null)
                sb.AppendFormat("{0} {1}", oUser.Nom, oUser.Cognoms);
        }
        return sb.ToString();
    }

    public static string WinnerLocationFullNom(DTORaffle oRaffle)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (oRaffle.Winner != null)
        {
            var oUser = oRaffle.Winner.User;
            if (oUser != null)
            {
                if (!string.IsNullOrEmpty(oUser.LocationNom))
                {
                    sb.Append(oUser.LocationNom);
                    if ((!string.IsNullOrEmpty(oUser.ProvinciaNom)) & oUser.ProvinciaNom != oUser.LocationNom)
                        sb.Append(" (" + oUser.LocationNom + ")");
                }
            }
        }
        return sb.ToString();
    }

    public string BannerUrl()
    {
        return DTORaffle.BannerUrl(base.Guid);
    }

    public static string BannerUrl(Guid guid)
    {
        return MmoUrl.ApiUrl("Raffle/ImageBanner600", guid.ToString());
    }

    public string ImgCallAction500Url()
    {
        return MmoUrl.ApiUrl("Raffle/ImageCallToAction500", base.Guid.ToString());
    }

    public static string ImgCallAction500Url(Guid guid)
    {
        return MmoUrl.ApiUrl("Raffle/ImageCallToAction500", guid.ToString());
    }

    public Decimal RateNewLeads()
    {
        Decimal retval = 0;
        if (this.ParticipantsCount > 0)
        {
            retval = 100 * this.NewParticipantsCount / (this.ParticipantsCount + this.NewParticipantsCount);
        }
        return retval;
    }

    public DTOAmt Cpl()
    {
        DTOAmt retval = DTOAmt.Factory();
        if (this.NewParticipantsCount > 0)
        {
            Decimal totalCost = (this.CostPrize == null ? 0 : this.CostPrize.Eur) + (this.CostPubli == null ? 0 : this.CostPubli.Eur);
            retval = DTOAmt.Factory(totalCost / this.NewParticipantsCount);
        }
        return retval;
    }



    public Statuses Status()
    {
        Statuses retval = Statuses.NoReactionYet;

        if (this.FchPicture != DateTime.MinValue)
        {
            retval = Statuses.WinnerPictureSubmitted;
        }
        else if (this.FchDelivery != DateTime.MinValue)
        {
            retval = Statuses.Delivered;
        }
        else if (this.FchDistributorReaction != DateTime.MinValue)
        {
            retval = Statuses.DistributorReacted;
        }
        else if (this.FchWinnerReaction != DateTime.MinValue)
        {
            retval = Statuses.WinnerReacted;
        }
        return retval;
    }

    public string ZoomUrl(DTOWebDomain domain = null)
    {
        domain = (domain == null) ? DTOWebDomain.Default() : domain;
        string retval = domain.Url("sorteo/zoom", this.Guid.ToString());
        return retval;
    }

    public string PlayUrl(DTOWebDomain domain = null)
    {
        domain = (domain == null) ? DTOWebDomain.Default() : domain;
        string retval = domain.Url("sorteo/play", this.Guid.ToString());
        return retval;
    }

    public string ThumbnailUrl(DTOWebDomain domain = null)
    {
        domain = (domain == null) ? DTOWebDomain.Default() : domain;
        string retval = domain.ImageUrl(Defaults.ImgTypes.sorteofbfeatured200, this.Guid);
        return retval;
    }

    public string ImageFbFeaturedUrl(bool AbsoluteUrl = false)
    {
        var retval = MmoUrl.image(Defaults.ImgTypes.sorteofbfeatured200, base.Guid, AbsoluteUrl);
        return retval;
    }

    public static bool IsNotYetStarted(DTORaffle oRaffle)
    {
        bool retval = true;
        if (IsOver(oRaffle))
            retval = false;
        else if (IsActive(oRaffle))
            retval = false;
        return retval;
    }

    public bool IsOver()
    {
        bool retval = FchTo < DTO.GlobalVariables.Now();
        return retval;
    }

    public static bool IsOver(DTORaffle oRaffle)
    {
        bool retval = false;
        if (oRaffle != null)
            retval = oRaffle.IsOver();
        return retval;
    }

    public static bool IsActive(DTORaffle oRaffle)
    {
        bool retval = false;
        if (IsOver(oRaffle))
            retval = false;
        else
            switch (oRaffle.FchFrom.Year)
            {
                case object _ when oRaffle.FchFrom.Year > DTO.GlobalVariables.Now().Year:
                    {
                        retval = false;
                        break;
                    }

                case object _ when oRaffle.FchFrom.Year < DTO.GlobalVariables.Now().Year:
                    {
                        retval = true;
                        break;
                    }

                case object _ when oRaffle.FchFrom.Year == DTO.GlobalVariables.Now().Year:
                    {
                        switch (oRaffle.FchFrom.Month)
                        {
                            case object _ when oRaffle.FchFrom.Month > DTO.GlobalVariables.Now().Month:
                                {
                                    retval = false;
                                    break;
                                }

                            case object _ when oRaffle.FchFrom.Month < DTO.GlobalVariables.Now().Month:
                                {
                                    retval = true;
                                    break;
                                }

                            case object _ when oRaffle.FchFrom.Month == DTO.GlobalVariables.Now().Month:
                                {
                                    switch (oRaffle.FchFrom.Day)
                                    {
                                        case object _ when oRaffle.FchFrom.Day > DTO.GlobalVariables.Now().Day:
                                            {
                                                retval = false;
                                                break;
                                            }

                                        case object _ when oRaffle.FchFrom.Day < DTO.GlobalVariables.Now().Day:
                                            {
                                                retval = true;
                                                break;
                                            }

                                        case object _ when oRaffle.FchFrom.Day == DTO.GlobalVariables.Now().Day:
                                            {
                                                retval = true;
                                                break;
                                            }
                                    }

                                    break;
                                }
                        }

                        break;
                    }
            }

        return retval;
    }


    public class Collection : List<DTORaffle>
    {
        public static DTOUrl Url()
        {
            return DTOUrl.Factory("sorteos","sortejos","raffles","sorteios");
        }

        public static String LangUrl(DTOLang lang)
        {
            String segment = lang.Tradueix("sorteos", "sortejos", "raffles", "sorteios");
            String retval = lang.Domain(true).LangUrl(lang, segment);
            return retval;
        }
    }

    public class HeadersModel
    {
        public int TotalCount { get; set; }
        public int Take { get; set; }
        public int TakeFrom { get; set; }
        public DateTime NextFch { get; set; }
        public List<Item> Items { get; set; }

        public HeadersModel()
        {
            this.Items = new List<Item>();
        }

        public List<Item> DueItems()
        {
            DateTime now = DTO.GlobalVariables.Now();
            return this.Items.Where(x => x.FchTo < now).ToList();
        }

        public Item ActiveRaffle()
        {
            DateTime now = DTO.GlobalVariables.Now();
            Item retval = this.Items.FirstOrDefault(x => x.FchFrom <= now && x.FchTo > now);
            return retval;
        }

        public bool AnyActiveRaffle()
        {
            bool retval = (this.ActiveRaffle() != null);
            return retval;
        }

        public bool AnyNextRaffle()
        {
            DateTime now = DTO.GlobalVariables.Now();
            bool retval = this.NextFch > now;
            return retval;
        }

        public TimeSpan TimeToNext()
        {
            TimeSpan retval = NextFch - DTO.GlobalVariables.Now();
            return retval;
        }
        public string TimeToNextLessThan(DTOLang lang)
        {
            string retval = "";
            TimeSpan span = TimeToNext();
            if (span.TotalMinutes > 60)
                retval = string.Format("{0} {1}", (int)(span.TotalHours + 1), lang.Tradueix("horas", "hores", "hours"));
            else
                retval = string.Format("{0} {1}", (int)(span.TotalMinutes + 1), lang.Tradueix("minutos", "minuts", "minutes"));
            return retval;
        }


        public bool AnyMoreItems()
        {
            return (ItemsLeft() > 0);
        }

        public int ItemsLeft()
        {
            int retval = this.TotalCount - this.TakeFrom - this.Take;
            return retval;
        }

        public int MoreToTake()
        {

            int retval = this.Take;
            if (this.MoreToTakeFrom() + retval > this.TotalCount)
                retval = this.TotalCount - this.MoreToTakeFrom();

            return retval;
        }

        public int MoreToTakeFrom()
        {

            int retval = this.TakeFrom + this.Take;
            return retval;
        }


        public class Item
        {
            public Guid Guid { get; set; }
            public string Title { get; set; }
            public DateTime FchFrom { get; set; }
            public DateTime FchTo { get; set; }
            public string Winner { get; set; }
            public DTOFeedback.Model Feedback { get; set; }

            public string PlayUrl()
            {
                DTORaffle raffle = new DTORaffle(this.Guid);
                return raffle.PlayUrl();
            }

            public string ZoomUrl()
            {
                DTORaffle raffle = new DTORaffle(this.Guid);
                return raffle.ZoomUrl();
            }

            public string ThumbnailUrl()
            {
                DTORaffle raffle = new DTORaffle(this.Guid);
                return raffle.ThumbnailUrl();
            }
            public string BannerUrl()
            {
                DTORaffle raffle = new DTORaffle(this.Guid);
                return raffle.BannerUrl();
            }



        }

    }

}

public class DTORaffleParticipant : DTOBaseGuid
{
    public class Compact
    {
        public Guid Guid { get; set; }
        public DTOUser.Compact User { get; set; }
        public DateTime Fch { get; set; }
        public int Num { get; set; }


        public static Compact Factory(Guid participantGuid, Guid userGuid, String nom, string cognoms, string location, string provincia)
        {
            Compact retval = new Compact();
            retval.Guid = participantGuid;
            retval.User = new DTOUser.Compact();
            retval.User.Guid = userGuid;
            if (!String.IsNullOrEmpty(nom))
            {
                retval.User.Nom = nom;
            }
            if (!String.IsNullOrEmpty(cognoms))
            {
                retval.User.Nom += nom.Length == 0 ? cognoms : " " + cognoms;
            }
            if (!String.IsNullOrEmpty(location))
            {
                if (!String.IsNullOrEmpty(provincia))
                    retval.User.Nom += " (" + location + ", " + provincia + ")";
                else
                    retval.User.Nom += " (" + location + ")";
            }
            return retval;
        }
    }

    public DTORaffle Raffle { get; set; }
    public DateTime Fch { get; set; }
    public DTOUser User { get; set; }

    public DTOContact Distribuidor { get; set; }
    public int Answer { get; set; } = -1;
    public int Num { get; set; }

    public DTORaffleParticipant() : base()
    {
    }

    public DTORaffleParticipant(Guid oGuid) : base(oGuid)
    {
    }

    public static DTORaffleParticipant Factory(DTORaffle oRaffle, DTOUser oUser, int Answer = 0, DTOContact oDistributor = null/* TODO Change to default(_) if this is not a reference type */)
    {
        DTORaffleParticipant retval = new DTORaffleParticipant();
        {
            var withBlock = retval;
            withBlock.Raffle = oRaffle;
            withBlock.User = oUser;
            withBlock.Fch = DTO.GlobalVariables.Now();
            withBlock.Answer = Answer;
            withBlock.Distribuidor = oDistributor;
        }
        return retval;
    }

    public static string FullNom(DTORaffleParticipant oParticipant)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DTOUser oUser = oParticipant.User;
        sb.Append(oUser.Nom);
        if (!string.IsNullOrEmpty(oUser.Cognoms))
        {
            if (sb.Length > 0)
                sb.Append(" ");
            sb.Append(oUser.Cognoms);
        }

        if (sb.Length == 0)
        {
            if (!string.IsNullOrEmpty(oUser.NickName))
                sb.Append(oUser.NickName);
            else
                sb.Append(oUser.EmailAddress);
        }

        string retval = sb.ToString();
        return retval;
    }

    public static string FormattedNumAndNom(DTORaffleParticipant oParticipant)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(FormattedNum(oParticipant));
        sb.Append(" ");
        sb.Append(Nom(oParticipant));
        string retval = sb.ToString();
        return retval;
    }

    public static string FormattedNum(DTORaffleParticipant oParticipant)
    {
        string retval = "";
        if (oParticipant != null)
            retval = string.Format("{0:0000}", oParticipant.Num);
        return retval;
    }

    public static string Nom(DTORaffleParticipant oParticipant)
    {
        string retval = "";
        if (oParticipant != null)
        {
            DTOUser oUser = oParticipant.User;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oUser.Nom);
            if (sb.Length > 0)
                sb.Append(" ");
            sb.Append(oUser.Cognoms);
            retval = sb.ToString();
        }
        return retval;
    }

    public bool HasRightAnswer()
    {
        bool retval = (Answer == Raffle.RightAnswer - 1);
        return retval;
    }

    public static List<DTOZona> ExcludedZonas()
    {
        List<DTOZona> retval = new List<DTOZona>();
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasTenerife));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasGranCanaria));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasLaPalma));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasLaGomera));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasHierro));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasLanzarote));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasFuerteventura));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Ceuta));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Melilla));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Azores));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Madeira));
        retval.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Andorra));
        return retval;
    }

    public class Exception : System.Exception
    {
        public Reasons Reason { get; set; }
        public enum Reasons
        {
            NotSet,
            Duplicated,
            WrongCountry
        }

        public Exception(Reasons reason, string message = "") : base(message)
        {
            Reason = reason;
        }
    }
}
