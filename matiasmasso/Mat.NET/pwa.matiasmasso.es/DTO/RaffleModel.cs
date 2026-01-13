using DocumentFormat.OpenXml.Office2010.Excel;
using DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DTO
{
    public class RaffleModel : BaseGuid, IModel
    {
        public string? Title { get; set; }
        public Guid? Country { get; set; }
        public string? Description { get; set; }
        public DateTime? FchFrom { get; set; }
        public DateTime? FchTo { get; set; }
        public string? Question { get; set; }
        public string? Answers { get; set; }
        public int? RightAnswer { get; set; }

        public Participant? Winner { get; set; }

        public bool HasImgWinner { get; set; }
        public string? Bases { get; set; }
        public LangDTO? Lang { get; set; }

        public int LeadsCount { get; set; }
        public int NewLeadsCount { get; set; }
        public decimal? Prize { get; set; }

        public Guid? Product { get; set; }
        public decimal? Publicity { get; set; }
        public int? Shares { get; set; }
        public int? Status { get; set; }



        public const int BANNER600WIDTH = 600;
        public const int BANNER600HEIGHT = 200;
        public const int THUMBNAILWIDTH = 178;
        public const int THUMBNAILHEIGHT = 125;


        public RaffleModel() : base() { }
        public RaffleModel(Guid guid) : base(guid) { }


        public string ThumbnailUrl() => RaffleModel.ThumbnailUrl(Guid);
        public static string ThumbnailUrl(Guid guid) => Globals.RemoteApiUrl("raffle/thumbnail", string.Format("{0}.jpg", guid.ToString()));
        public string ImgBanner600Url() => Globals.RemoteApiUrl("raffle/ImgBanner600", string.Format("{0}.jpg", base.Guid.ToString()));
        public static LangDTO RaffleLang(LangDTO? lang) => !(lang?.IsPor() ?? false) ? lang! : LangDTO.Esp();

        public string PropertyPageUrl() => Globals.PageUrl("Raffle", Guid.ToString());
        public string PlayUrl() => Globals.PageUrl("Raffle/Play", Guid.ToString());


        public bool IsActive() => (FchFrom < DateTime.Now && FchTo > DateTime.Now);
        public bool IsOver() => FchTo < DateTime.Now;

        public string Answer(int? baseOneidx){
            string retval = "";
            if(!string.IsNullOrEmpty(Answers) && baseOneidx != null)
            {
                var answers = AnswerList();
                if (answers.Count >= baseOneidx)
                    retval = answers[(int)baseOneidx - 1];
            }
            return retval;
        }

        public List<string> AnswerList()
        {
            var retval = new List<string>();
            if (Answers != null){
                retval = Regex.Split(Answers, "\r\n|\r|\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
            }
            return retval;
        }

        public static CountryDTO UserCountry(UserModel user)
        {
            if (user.Country?.IsPortugal() ?? false) return user.Country;
            return CountryDTO.Spain();
        }
        public static CountryDTO LangCountry(LangDTO lang)
        {
            if (lang.IsPor()) return CountryDTO.Portugal();
            return CountryDTO.Spain();
        }

        public string WinnerImgUrl() => Globals.RemoteApiUrl("raffle/winner/image", string.Format("{0}.jpg", base.Guid.ToString()));
        public string WinnerFullNom() => Winner?.Lead?.NomiCognoms() ?? "";
        public string Caption() => string.Format("RaffleModel: {0:dd/MM/yy}-{1:dd/MM/yy} {2}", FchFrom, FchTo, Title);

        public Box HomeBox(LangDTO lang) => new Box
        {
            Caption = Title ?? "?",
            Url = UrlHelper.RelativeUrl(lang, PlayUrl()),
            ImageUrl = ImgBanner600Url()
        };
        public override string ToString() => string.Format("RaffleModel: {0:dd/MM/yy}-{1:dd/MM/yy} {2}", FchFrom, FchTo, Title);

        public class Participant : BaseGuid, IModel
        {
            public UserModel? Lead { get; set; }

            public RaffleModel? Raffle { get; set; }
            public int? Ticket { get; set; }
            public int? Answer { get; set; }
            public DateTime? Fch { get; set; }
            public Distributor? Distributor { get; set; }

            public Participant() : base() { }
            public Participant(Guid guid) : base(guid) { }

            public string NomAndCognoms() => string.Format("{0} {1}", Lead?.Nom ?? "", Lead?.Cognoms ?? "");
            public bool HasNomAndCognoms() => !string.IsNullOrEmpty(Lead?.Nom) && !string.IsNullOrEmpty(Lead?.Cognoms);
            public bool HasBirthYea() => Lead?.BirthYea != null && Lead?.BirthYea > 1900 && Lead?.BirthYea < DateTime.Today.Year;
            public bool HasAnswered() => Answer != null;
            public bool HasDistributor() => Distributor != null;


            public string? FormattedAnswer()
            {
                string? retval = null;
                if (HasAnswered())
                {
                    List<string> answers = Raffle!.AnswerList();
                    var answerText = answers[(int)Answer! - 1];
                    retval = string.Format("{0}.- {1}", Answer, answerText);
                }
                return retval;
            }

            public string? FormattedDistributor()
            {
                string? retval = null;
                if (HasDistributor())
                {
                    retval = string.Format("{0} ({1})", Distributor!.Nom, Distributor.Location);
                }
                return retval;
            }

            public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());
            public string Caption() => Lead?.EmailAddress?? string.Empty;

            public bool HasValidNomAndCognoms()
            {
                bool retval = true;
                if (!IsValidPersonName(Lead?.Nom)) retval = false;
                else if (!IsValidPersonName(Lead?.Cognoms)) retval = false;
                return retval;
            }

            public bool IsValidPersonName(string? src, int minlength = 2)
            {
                bool retval = true;
                if (string.IsNullOrEmpty(src)) retval = false;
                else if (src.Length < minlength) retval = false;
                else if (!Regex.Match(src, "^([a-zA-Záéíóúàèìòùüãç_ ªº_-_']+?)*?$").Success) retval = false;
                return retval;
            }
        }

        public class Player : Participant
        {
            public StoreLocatorDTO.OfflineClass? StoreLocator { get; set; }
            public Player Duplicated { get; set; }

            public Player() : base() {
                Fch = DateTime.Now;
            }

            public Player Factory(UserModel user, RaffleModel raffle)
            {
                var retval = new Player();
                retval.Lead = user;
                retval.Raffle = raffle;
                return retval;
            }

            public new string Caption()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ", NomAndCognoms());
                if(Lead?.Residence != null) sb.AppendFormat("({0})", (Lead.Residence.Nom));
                return sb.ToString().Trim();
            }

            public bool IsMissingUserDetails()
            {
                var retval = false;
                if (string.IsNullOrEmpty(Lead?.Nom)) retval = true;
                if (string.IsNullOrEmpty(Lead?.Cognoms)) retval = true;
                if (Lead?.BirthYea != null) retval = true;
                return retval;
            }

            public Player Trimmed()
            {
                var retval = new Player()
                {
                    Guid = Guid,
                    Lead = Lead,
                    Fch = Fch,
                    Raffle = new RaffleModel(Raffle!.Guid),
                    Answer = Answer,
                    Distributor = new Distributor(Distributor!.Guid)
                };
                return retval;
            }

            public string ConfirmationEmailBody(string localizedTemplate, LangDTO lang)
            {
                return string.Format(localizedTemplate
                    , Raffle!.Description
                    , NomAndCognoms()
                    , Fch
                    , Ticket
                    , FormattedAnswer()
                    , FormattedDistributor()
                    , lang.Weekday((DateTime)Raffle.FchTo!)
                    , (DateTime)Raffle.FchTo!
                    );
            }
        }

        public class Distributor : BaseGuid
        {

            public string? Nom { get; set; }
            public string? Address { get; set; }
            public string? Location { get; set; }

            public Distributor() : base() { }
            public Distributor(Guid guid) : base(guid) { }
        }
    }

}
