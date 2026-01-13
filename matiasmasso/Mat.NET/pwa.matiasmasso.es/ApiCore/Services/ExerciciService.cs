using Api.Entities;
using DTO;
using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Linq;

namespace Api.Services
{
    public class ExerciciService
    {
        private static LangDTO Lang = LangDTO.Cat();


        public static void Apertura(UserModel user, ExerciciModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var ctas = PgcCtasService.GetValues(db);
                var contacts = ContactsService.GetValues(db);

                var currentYear = DateTime.Today.Year;
                for (var year = value.Year; year <= currentYear; year++)
                {
                    Apertura(db, user, value.Emp, year, ctas, contacts);
                }
            }
        }

        private static void Apertura(Entities.MaxiContext db, UserModel user, EmpModel.EmpIds emp, int year, List<PgcCtaModel>ctas, List<ContactModel>contacts)
        {
            var items = Items(db, emp, year - 1);
            var saldos = Saldos(items, ctas, contacts);

            RemovePreviousApertura(db, emp, year);
            var firstCca = FirstCca(db, user, emp, year, saldos, ctas);
            CcaService.Update(db, firstCca);
            db.SaveChanges();

            var saldosPerCta = saldos
                .Where(x => x.Contact != null)
                .GroupBy(x => x.Cta)
                .OrderBy(x => x.Key!.Id)
                .ToList();

            foreach (var saldo in saldosPerCta)
            {
                var cta = saldo.Key;
                var contactSaldos = saldo.Select(x => x)
                    .OrderBy(x => x.Contact!.FullNom)
                    .ToList();

                var cca = ContactCca(user, emp, year, contactSaldos);
                CcaService.Update(db, cca);
                db.SaveChanges();
            }

            Renumerate(db, emp, year);
            db.SaveChanges();

        }

        private static CcaModel ContactCca(UserModel user, EmpModel.EmpIds emp, int year, List<Saldo> saldos)
        {
            var fch = DateOnly.FromDateTime(new DateTime(year, 1, 1));
            var retval = CcaModel.Factory(emp, user, CcaModel.CcdEnum.AperturaExercisi, fch);
            var cta = saldos.First().Cta!;

            retval.Concept = $"Apertura cte.{cta.Id} {cta.Nom?.Tradueix(Lang)}";
            retval.Cdn = int.Parse(cta.Id!);
            foreach (var saldo in saldos)
            {
                if (cta.IsDeutora())
                {
                    if (saldo.Eur > 0)
                        retval.AddDebit(saldo.Eur, cta, saldo.Contact!.Guid);
                    else
                        retval.AddCredit(Math.Abs(saldo.Eur), cta, saldo.Contact!.Guid);
                }
                else
                {
                    if (saldo.Eur > 0)
                        retval.AddCredit(saldo.Eur, cta, saldo.Contact!.Guid);
                    else
                        retval.AddDebit(Math.Abs(saldo.Eur), cta, saldo.Contact!.Guid);
                }
            }
            retval.AddSaldo(cta!);
            return retval;
        }

        private static CcaModel FirstCca(Entities.MaxiContext db, UserModel user, EmpModel.EmpIds emp, int year, List<Saldo> saldos, List<PgcCtaModel> ctas)
        {
            var fch = DateOnly.FromDateTime(new DateTime(year, 1, 1));
            var retval = CcaModel.Factory(emp, user, CcaModel.CcdEnum.AperturaExercisi, fch);
            retval.Concept = $"Apertura exercici {year}";
            var groupedSaldos = saldos.GroupBy(x => x.Cta).OrderBy(x => x.Key!.Id).ToList();
            foreach (var saldo in groupedSaldos)
            {
                if (saldo.Key!.Act == PgcCtaModel.Acts.Deutora)
                    retval.AddDebit(saldo.Sum(x => x.Eur), saldo.Key);
                else
                    retval.AddCredit(saldo.Sum(x => x.Eur), saldo.Key);
            }
            if (saldos.Sum(x => x.Eur) != 0)
            {
                var ctaResult = ctas.First(x => x.Cod == PgcCtaModel.Cods.ResultatsAnyAnterior);
                retval.AddSaldo(ctaResult);
            }
            return retval;
        }

        private static void RemovePreviousApertura(Entities.MaxiContext db, EmpModel.EmpIds emp, int year)
        {
            db.Ccas.RemoveRange(db.Ccas.Where(x =>
            x.Emp == (int)emp &&
            x.Yea == year &&
            x.Ccd == (int)CcaModel.CcdEnum.AperturaExercisi));
        }

        private static List<Item> Items(Entities.MaxiContext db, EmpModel.EmpIds emp, int year)
        {
            var retval = db.Ccbs.Where(x =>
            x.Cca.Emp == (int)emp &&
            x.Cca.Yea == year &&
            x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentComptes &&
            x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentExplotacio &&
            x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentBalanç)
                .Select(x => new Item
                {
                    Cta = x.CtaGuid,
                    Contact = x.ContactGuid,
                    Eur = x.Eur,
                    Dh = (CcaModel.Item.DhEnum)x.Dh
                })
                .ToList();

            return retval;
        }
        private static List<Saldo> Saldos(List<Item> items, List<PgcCtaModel> ctas, List<ContactModel> contacts)
        {
            var retval = items.GroupBy(x => new { x.Cta, x.Contact })
                .Select(x => new Saldo
                {
                    Cta = ctas.FirstOrDefault(y => y.Guid == x.Key.Cta),
                    Contact = contacts.FirstOrDefault(y => y.Guid == x.Key.Contact),
                    Eur = ctas.First(y => y.Guid == x.Key.Cta).IsDeutora() ?
                    x.Sum(y => y.Dh == CcaModel.Item.DhEnum.Deb ? y.Eur : -y.Eur) :
                    x.Sum(y => y.Dh == CcaModel.Item.DhEnum.Hab ? y.Eur : -y.Eur)
                })
                .Where(x => x.Eur != 0 && x.Cta!.IsBalance())
                .OrderBy(x => x.Cta!.Id)
                .ToList();

            return retval;
        }

        private static void Renumerate(Entities.MaxiContext db, EmpModel.EmpIds emp, int year)
        {
            var maxId = db.Ccas.Where(x =>
            x.Emp == (int)emp &&
            x.Yea == year)
                .Max(x => x.Cca1);

            foreach (var cca in db.Ccas.Where(x =>
            x.Emp == (int)emp &&
            x.Yea == year)
                .OrderBy(x => x.Fch).ThenBy(x => x.Ccd).ThenBy(x => x.Cdn))
            {
                cca.Cca1 += maxId;
            }

            db.SaveChanges();

            var id = 0;
            foreach (var cca in db.Ccas.Where(x =>
                x.Emp == (int)emp &&
                x.Yea == year)
                    .OrderBy(x => x.Fch).ThenBy(x => x.Ccd).ThenBy(x => x.Cdn))
            {
                id += 1;
                cca.Cca1 = id;
            }

        }

        private class Item
        {
            public Guid Cta { get; set; }
            public Guid? Contact { get; set; }
            public decimal Eur { get; set; }
            public CcaModel.Item.DhEnum Dh { get; set; }

            public override string ToString()
            {
                return $"Cte:{Cta.ToString()}-Contact:{Contact?.ToString()}, Eur:{Eur:N2}€ {Dh.ToString()}";
            }
        }
        private class Saldo
        {
            public PgcCtaModel? Cta { get; set; }
            public ContactModel? Contact { get; set; }
            public decimal Eur { get; set; }

            public override string ToString()
            {
                return $"Cte:{Cta?.ToString()} Contact:{Contact?.ToString()} Eur:{Eur:N2}€";
            }

        }
    }

    public class ExercicisService
    {
        public static List<ExerciciModel> GetValues(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Ccas
                    .GroupBy(x=>new {x.Emp, x.Yea})
                    .Select(x => new ExerciciModel
                    {
                        Emp = (EmpModel.EmpIds)x.Key.Emp,
                        Year = x.Key.Yea
                    })
                    .ToList();

                retval = retval
                    .Where(x => user.Emps.Any(y => y == x.Emp))
                    .OrderBy(x => x.Emp)
                    .ThenByDescending(x => x.Year)
                    .ToList();

                return retval;
            }
        }
    }
}
