using Api.Entities;
using ClosedXML;
using DTO;
namespace Api.Services
{
    public class MarketPlaceService
    {
        public static MarketPlaceModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.MarketPlaces
                    .Where(x => x.Guid == guid)
                    .Select(x => new MarketPlaceModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Web = x.Web,
                        Commission = x.Commission

                    }).FirstOrDefault();
            }
        }


        public static bool Update(MarketPlaceModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Update(db, value);
            }
        }

        public static bool Update(Entities.MaxiContext db, MarketPlaceModel value)
        {
            var guid = value.Guid;
            Entities.MarketPlace? entity;
            if (value.IsNew)
            {
                entity = new Entities.MarketPlace();
                db.MarketPlaces.Add(entity);
                entity.Guid = guid;
            }
            else
                entity = db.MarketPlaces.Find(guid);

            if (entity == null) throw new System.Exception("MarketPlace not found");

            entity.Nom = value.Nom;
            entity.Web = value.Web;
            entity.Commission = value.Commission;

            db.SaveChanges();
            return true;
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.MarketPlaces.Find(guid);
                if (entity != null)
                {
                    db.MarketPlaces.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }

        public static List<OfferModel> Offers(Guid marketplace)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Offers(db, marketplace);
            }
        }
        public static List<OfferModel> Offers(Entities.MaxiContext db, Guid marketplace)
        {
            return db.Offers
                .Where(x => x.Parent == marketplace)
                .Select(x => new OfferModel
                {
                    Parent = marketplace,
                    Sku = x.Sku,
                    Price = x.Price
                })
                .ToList();
        }

        public static List<OfferModel> UpdateOffer(OfferModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Offer? entity = db.Offers.Find(value.Parent, value.Sku);
                if (entity == null)
                {
                    entity = new Entities.Offer();
                    db.Offers.Add(entity);
                    entity.Parent = value.Parent;
                    entity.Sku = value.Sku;
                }

                entity.Price = value.Price ?? 0;

                db.SaveChanges();

                return Offers(db, value.Parent);
            }
        }

        public static List<OfferModel> DeleteOffer(OfferModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Offer? entity = db.Offers.Find(value.Parent, value.Sku);
                if (entity != null)
                {
                    db.Offers.Remove(entity);
                    db.SaveChanges();
                }
                return Offers(db, value.Parent);
            }
        }

    }
    public class MarketPlacesService
    {
        public static List<MarketPlaceModel> All(int empId)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.MarketPlaces
                    .Where(x => x.Gu.Emp == empId)
                    .OrderBy(x => x.Nom)
                    .Select(x => new MarketPlaceModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Web = x.Web,
                        Commission = x.Commission
                    })
                    .ToList();
                return retval;
            }
        }
    }
}
