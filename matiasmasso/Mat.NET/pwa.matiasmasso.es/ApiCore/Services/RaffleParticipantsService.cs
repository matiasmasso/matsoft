using DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public class RaffleParticipantService
    {
        public static RaffleModel.Participant? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.SorteoLeads
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new RaffleModel.Participant
                    {
                        Guid = x.Guid,
                        Fch = x.Fch,
                        Answer = x.Answer,
                        Raffle = new RaffleModel
                        {
                            Guid = x.Sorteo,
                            Title = x.SorteoNavigation.Title,
                            FchFrom = x.SorteoNavigation.FchFrom,
                            FchTo = x.SorteoNavigation.FchTo,
                            RightAnswer = x.Answer,
                            Winner = x.SorteoNavigation.Winner == null ? null : new RaffleModel.Participant((Guid)x.SorteoNavigation!.Winner)
                        },
                        Distributor= x.Distributor==null ? null : new RaffleModel.Distributor
                        {
                            Guid = (Guid)x.Distributor,
                            Nom = x.DistributorNavigation == null ? null : string.IsNullOrEmpty(x.DistributorNavigation.NomCom) ? x.DistributorNavigation.RaoSocial : x.DistributorNavigation.NomCom
                        }
                    })
                    .FirstOrDefault();
            }
        }

        public static int Update(RaffleModel.Participant value)
        {
            using (var db = new Entities.MaxiContext())
            {
                if(value.Ticket == null)
                {
                    var lastTicket = db.SorteoLeads.Where(x => x.Sorteo == value.Raffle!.Guid).Max(x => x.Num) ?? 0;
                    value.Ticket = lastTicket + 1;
                }

                Entities.SorteoLead? entity;
                if (value.IsNew)
                {
                    entity = new Entities.SorteoLead();
                    db.SorteoLeads.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.SorteoLeads.Find(value.Guid);

                if (entity == null) throw new Exception("Raffle participant not found");

                entity.Sorteo = value.Raffle!.Guid;
                entity.Lead = value.Lead!.Guid;
                entity.Fch = value.Fch;
                entity.Answer = value.Answer;
                entity.Distributor = value.Distributor!.Guid;
                entity.Num = value.Ticket;

                db.SaveChanges();

                if(!string.IsNullOrEmpty(value?.Lead?.Nom) && !string.IsNullOrEmpty(value?.Lead?.Cognoms))
                    UserService.UpdateUserNames(db, value.Lead.Guid, value.Lead.Nom, value.Lead.Cognoms);

                return (int)value!.Ticket;
            }
        }

    }
    public class RaffleParticipantsService
    {
        public static List<RaffleModel.Participant> FromUser(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.SorteoLeads
                        .AsNoTracking()
                    .Where(x => x.Lead == guid)
                    .Select(x => new RaffleModel.Participant
                    {
                        Guid = x.Guid,
                        Fch = x.Fch,
                        Answer = x.Answer,
                        Raffle = new RaffleModel
                        {
                            Guid = x.Sorteo,
                            Title = x.SorteoNavigation.Title,
                            FchFrom = x.SorteoNavigation.FchFrom,
                            FchTo = x.SorteoNavigation.FchTo,
                            RightAnswer = x.Answer
                        }
                    }).ToList();
            }
        }
    }
}
