using Api.Entities;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http.Headers;
using static DTO.CatalogDTO;
using static DTO.ContactModel;
using static DTO.RaffleModel;

namespace Api.Services
{
    public class RaffleService
    {

        public static RaffleModel? Find(Guid? guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                RaffleModel? retval = null;
                if (guid != null)
                    retval = Find(db, (Guid)guid);
                return retval;
            }
        }

        public static RaffleModel.Player? Player(Guid raffle, UserModel user)
        {
            RaffleModel.Player? retval = null;
                using (var db = new Entities.MaxiContext())
                {
                var duplicated = CheckDuplicatedPlayer(db, raffle, user);
                if (duplicated == null)
                {
                    retval = Player(db, (Guid)raffle!, user!);
                } else
                {
                    retval = new RaffleModel.Player() { 
                        Duplicated = duplicated
                    };
                }
                }
            return retval;
        }

        public static RaffleModel.Player? CheckDuplicatedPlayer(Entities.MaxiContext db, Guid raffle, UserModel user)
        {
             return db.SorteoLeads
                        .AsNoTracking()
                .Where(x => x.Sorteo == raffle && x.Lead == user.Guid)
                .Select(x=> new RaffleModel.Player()
                {
                    Guid = x.Guid,
                    Fch = x.Fch,
                    Ticket = x.Num
                })
                .FirstOrDefault();
        }

        public static RaffleModel.Player Player(Entities.MaxiContext db, Guid raffle, UserModel user)
        {
            var retval = new RaffleModel.Player();
            retval.Lead = user;
            retval.Raffle = Find(raffle);

            if (retval.Raffle?.Product != null)
            {
                var cache = CacheService.CatalogRequest();
                var product = cache.ProductFromGuid(retval.Raffle.Product);
                if (product != null)
                {
                    retval.StoreLocator = StoreLocatorService.Offline(db, product, retval.Raffle.Lang, true);
                }
            }
            return retval;
        }



        public static RaffleModel? Find(Entities.MaxiContext db, Guid guid)
        {
            var retval = db.Sorteos
                        .AsNoTracking()
            .Where(x => x.Guid == guid)
            .Include(x => x.WinnerNavigation)
            .Include(x => x.WinnerNavigation.LeadNavigation)
            .Select(x => new RaffleModel(x.Guid)
            {
                Title = x.Title,
                Country = x.Country,
                Lang = new LangDTO(x.Lang),
                FchFrom = x.FchFrom,
                FchTo = x.FchTo,
                Product = x.Art,
                Question = x.Question,
                Answers = x.Answers,
                RightAnswer = x.RightAnswer,
                Bases = x.Bases,
                HasImgWinner = x.ImgWinner != null,

                Winner = x.Winner == null ? null : new RaffleModel.Participant((Guid)x.Winner)
                {
                    Lead = x.WinnerNavigation == null ? null : new UserModel(x.WinnerNavigation.Lead)
                    {
                        Nom = x.WinnerNavigation.LeadNavigation.Nom,
                        Cognoms = x.WinnerNavigation.LeadNavigation.Cognoms
                    },
                    Ticket = x.WinnerNavigation == null ? null : x.WinnerNavigation.Num,
                    Fch = x.WinnerNavigation == null ? null : x.WinnerNavigation.Fch,
                    Answer = x.WinnerNavigation == null ? null : x.WinnerNavigation.Answer,
                    Distributor = x.WinnerNavigation.Distributor == null ? null : new RaffleModel.Distributor((Guid)x.WinnerNavigation.Distributor)
                }
            }).FirstOrDefault();


            if (retval?.Winner?.Distributor != null)
            {
                retval.Winner.Distributor = db.CliGrals
                        .AsNoTracking()
                    .Join(db.VwAddresses, cli => cli.Guid, adr => adr.SrcGuid, (cli, adr) => new { cli, adr })
                    .Where(x => x.cli.Guid == retval.Winner!.Distributor.Guid)
                    .Select(x => new RaffleModel.Distributor
                    {
                        Nom = string.IsNullOrEmpty(x.cli.NomCom) ? x.cli.RaoSocial : x.cli.NomCom,
                        Address = x.adr.Adr,
                        Location = (AddressService.Address(x.adr) == null || AddressService.Address(x.adr)!.Zip == null) ? null : AddressService.Address(x.adr)!.Zip!.FullNom(retval.Lang)
                    }).FirstOrDefault();
            }
            return retval;
        }

        public static RaffleModel? CurrentOrNext(Entities.MaxiContext db, LangDTO lang)
        {
            var now = DateTime.Now;

            List<RaffleModel> currentAndNextRaffles = db.Sorteos
                        .AsNoTracking()
                .Where(x => x.FchTo > now)
                .OrderBy(x => x.FchFrom)
                .Select(x => new RaffleModel(x.Guid)
                {
                    Title = x.Title,
                    Country = x.Country,
                    FchFrom = x.FchFrom,
                    FchTo = x.FchTo,
                    Lang = new LangDTO(x.Lang)
                }).ToList();

            if (lang.Id == LangDTO.Ids.POR)
                currentAndNextRaffles = currentAndNextRaffles.Where(x => x.Lang?.Id == lang.Id).ToList();
            else
                currentAndNextRaffles = currentAndNextRaffles.Where(x => x.Lang?.Id != LangDTO.Ids.POR).ToList();

            var retval = currentAndNextRaffles.FirstOrDefault();
            return retval;
        }

        public static Byte[]? Thumbnail(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Sorteos.AsNoTracking().Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.ImgFbFeatured116;
            }
            return retval;
        }

        public static Byte[]? ImgBanner600(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Sorteos
                        .AsNoTracking()
                    .Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.ImgBanner600;
            }
            return retval;
        }

        public static Byte[]? RaffleWinnerImg(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Sorteos
                    .AsNoTracking()
                    .Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.ImgWinner;
            }
            return retval;
        }

        public static RaffleModel.Participant UpdateParticipant(RaffleModel.Participant value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.SorteoLead? entity;
                if (value.IsNew)
                {
                    value.Fch = DateTime.Now;
                    value.Ticket = value.Ticket == null ? (LastParticipant(db, value.Raffle!.Guid) + 1) : value.Ticket;
                    entity = new Entities.SorteoLead();
                    entity.Guid = value.Guid;
                    db.SorteoLeads.Add(entity);
                }
                else
                    entity = db.SorteoLeads.Find(value.Guid);

                if (entity == null) throw new System.Exception("Participant not found");

                entity.Lead = value.Lead!.Guid;
                entity.Sorteo = value.Raffle!.Guid;
                entity.Fch = value.Fch;
                entity.Answer = value.Answer;
                entity.Distributor = value.Distributor!.Guid;
                entity.Num = value.Ticket;
                // Save changes in database
                db.SaveChanges();
            }
            return value;
        }
        public static int LastParticipant(Entities.MaxiContext db, Guid raffle)
        {
            var last = db.SorteoLeads
                .Where(x => x.Sorteo == raffle)
                .Max(x => x.Num);
            var retval = last ?? 0;
            return retval;
        }
    }


    public class RafflesService
    {
        public static List<RaffleModel> All(LangDTO lang)
        {
            var retval = new List<RaffleModel>();
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Sorteos
                    .Include(x => x.WinnerNavigation)
                    .Include(x => x.WinnerNavigation.LeadNavigation)
                        .AsNoTracking()
                    .Where(x => x.Country == RaffleModel.LangCountry(lang).Guid)
                    .OrderByDescending(x => x.FchTo)
                    .Select(x => new RaffleModel(x.Guid)
                    {
                        Title = x.Title,
                        Country = x.Country,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        Product = x.Art,
                        Question = x.Question,
                        Answers = x.Answers,
                        RightAnswer = x.RightAnswer,
                        LeadsCount = x.SorteoLeads.Count,
                        Winner = x.Winner == null ? null : new RaffleModel.Participant((Guid)x.Winner)
                        {
                            Lead = x.WinnerNavigation == null ? null : new UserModel(x.WinnerNavigation.Lead)
                            {
                                Nom = x.WinnerNavigation.LeadNavigation.Nom,
                                Cognoms = x.WinnerNavigation.LeadNavigation.Cognoms
                            },
                            Ticket = x.WinnerNavigation == null ? null : x.WinnerNavigation.Num,
                            Distributor = x.WinnerNavigation.Distributor == null ? null : new RaffleModel.Distributor((Guid)x.WinnerNavigation.Distributor)
                        }
                    })
                    .ToList();
                return retval;
            }
        }
    }
}
