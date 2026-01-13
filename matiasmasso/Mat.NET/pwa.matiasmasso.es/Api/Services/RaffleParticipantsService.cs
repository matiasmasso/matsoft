using DTO;
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
    }
    public class RaffleParticipantsService
    {
        public static List<RaffleModel.Participant> FromUser(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.SorteoLeads
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
