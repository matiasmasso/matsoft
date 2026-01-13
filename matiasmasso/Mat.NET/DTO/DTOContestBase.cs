using DTO;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System;

public class DTOContestBase : DTOBaseGuid
{
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
    public Image ImageFbFeatured { get; set; }
    [JsonIgnore]
    public Image ImageBanner600 { get; set; }
    [JsonIgnore]
    public Image ImageCallToAction500 { get; set; }
    public bool Visible { get; set; }

    public Codis Codi { get; set; }

    public enum Codis
    {
        NotSet,
        Raffle,
        Contest
    }

    public DTOContestBase() : base()
    {
    }

    public DTOContestBase(Guid oGuid) : base(oGuid)
    {
    }

    public string ImageFbFeaturedUrl(bool AbsoluteUrl = false)
    {
        var retval = MmoUrl.image(Defaults.ImgTypes.sorteofbfeatured200, base.Guid, AbsoluteUrl);
        return retval;
    }

    public static bool IsNotYetStarted(DTOContestBase oContestBase)
    {
        bool retval = true;
        if (IsOver(oContestBase))
            retval = false;
        else if (IsActive(oContestBase))
            retval = false;
        return retval;
    }

    public bool IsOver()
    {
        bool retval = FchTo < DateTime.Now;
        return retval;
    }

    public static bool IsOver(DTOContestBase oContestBase)
    {
        bool retval = false;
        if (oContestBase != null)
            retval = oContestBase.IsOver();
        return retval;
    }

    public static bool IsActive(DTOContestBase oContestBase)
    {
        bool retval = false;
        if (IsOver(oContestBase))
            retval = false;
        else
            switch (oContestBase.FchFrom.Year)
            {
                case object _ when oContestBase.FchFrom.Year > DateTime.Now.Year:
                    {
                        retval = false;
                        break;
                    }

                case object _ when oContestBase.FchFrom.Year < DateTime.Now.Year:
                    {
                        retval = true;
                        break;
                    }

                case object _ when oContestBase.FchFrom.Year == DateTime.Now.Year:
                    {
                        switch (oContestBase.FchFrom.Month)
                        {
                            case object _ when oContestBase.FchFrom.Month > DateTime.Now.Month:
                                {
                                    retval = false;
                                    break;
                                }

                            case object _ when oContestBase.FchFrom.Month < DateTime.Now.Month:
                                {
                                    retval = true;
                                    break;
                                }

                            case object _ when oContestBase.FchFrom.Month == DateTime.Now.Month:
                                {
                                    switch (oContestBase.FchFrom.Day)
                                    {
                                        case object _ when oContestBase.FchFrom.Day > DateTime.Now.Day:
                                            {
                                                retval = false;
                                                break;
                                            }

                                        case object _ when oContestBase.FchFrom.Day < DateTime.Now.Day:
                                            {
                                                retval = true;
                                                break;
                                            }

                                        case object _ when oContestBase.FchFrom.Day == DateTime.Now.Day:
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
}


public class DTOContestBaseParticipant : DTOBaseGuid
{


    public DTOContestBase Parent { get; set; }
    public DateTime Fch { get; set; }
    public DTOUser User { get; set; }

    public DTOContestBaseParticipant() : base()
    {
    }

    public DTOContestBaseParticipant(Guid oGuid) : base(oGuid)
    {
    }
}