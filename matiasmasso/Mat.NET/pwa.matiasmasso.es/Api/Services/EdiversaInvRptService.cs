using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Math;
using DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Services
{
    public class EdiversaInvRptsService
    {

        

        public static List<EdiversaInvrptModel> GetLastValues(Guid ccx, DateTime? fch)
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetLastValues(db, ccx, fch);
            }
        }
        public static List<EdiversaInvrptModel> GetLastValues(Entities.MaxiContext db, Guid ccx, DateTime? fch)
        {
            var retval = new List<EdiversaInvrptModel>();
            var lastParent = new EdiversaInvrptModel();
            if (!fch.HasValue) fch = DateTime.Today.Date;

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("SELECT B.Holding, EdiInvrptHeader.Guid, EdiInvrptHeader.DocNum, EdiInvrptHeader.Fch, EdiInvrptHeader.FchCreated, NADMS, A.Guid AS NADMSGuid, NADGY, C.Guid AS NADGYGuid, D.Ref AS NADGYRef  ");
            sb.AppendLine(", EdiInvrptItem.Lin, EdiInvrptItem.Ean, EdiInvrptItem.RefSupplier, EdiInvrptItem.RefCustomer, EdiInvrptItem.Qty ");
            sb.AppendLine("FROM EdiInvrptHeader ");
            sb.AppendLine("INNER JOIN CliGral A ON EdiInvrptHeader.NADMS = A.GLN ");
            sb.AppendLine("INNER JOIN CliClient B ON A.Guid = B.Guid ");
            sb.AppendLine("LEFT OUTER JOIN CliGral C ON EdiInvrptHeader.NADGY = C.GLN ");
            sb.AppendLine("LEFT OUTER JOIN CliClient D ON C.Guid = D.Guid ");
            sb.AppendLine("LEFT OUTER JOIN EdiInvrptItem ON EdiInvrptHeader.Guid = EdiInvrptItem.Parent ");
            sb.AppendLine("INNER JOIN (");
            sb.AppendLine("     SELECT NadGy AS NAD, MAX(Fch) AS LASTFCH ");
            sb.AppendLine("     FROM EdiInvrptHeader ");
            sb.AppendLine("     WHERE DATEDIFF(dy, Fch, @fch) BETWEEN 0 AND 10 ");
            sb.AppendLine("     GROUP BY NadGy ");
            sb.AppendLine("     ) X ON EdiInvrptHeader.NADGY = X.NAD AND EdiInvrptHeader.Fch = X.LASTFCH ");
            sb.AppendLine("WHERE B.Holding= @holding ");
            sb.AppendLine("AND EdiInvrptItem.Ean IS NOT NULL"); //---------------------------------------------------- FAKE

            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sb.ToString();
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@holding", ccx.ToString()));
                command.Parameters.Add(new SqlParameter("@fch", fch));

                db.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader.GetGuid("Guid") != lastParent.Guid)
                        {
                                lastParent = new EdiversaInvrptModel(reader.GetGuid("Guid"))
                                {
                                    DocNum = reader.GetString("DocNum"),
                                    FchDoc = reader.GetDateTime("Fch"),
                                    FchReceived = reader.GetDateTime("FchCreated"),
                                    NadGy = reader.GetString("NadGy"),
                                    StockHolder = reader.IsDBNull("NadGyGuid") ? null : new GuidNom
                                    {
                                        Guid = reader.GetGuid("NadGyGuid"),
                                        Nom = reader.IsDBNull("NadGyRef") ? null : reader.GetString("NadGyRef")
                                    }

                                };
                            retval.Add(lastParent);
                        }

                        if(reader.GetValue("Lin") != null)
                        {
                            lastParent.Items.Add(new EdiversaInvrptModel.Item
                            {
                                Lin = reader.GetInt32("Lin"),
                                Qty = reader.GetInt32("Qty"),
                                Ean = reader.IsDBNull("Ean") ? null : reader.GetString("Ean"),
                                SupplierRef = reader.IsDBNull("RefSupplier") ? null : reader.GetString("RefSupplier"),
                                CustomerRef = reader.IsDBNull("RefCustomer") ? null : reader.GetString("RefCustomer")
                            });
                        }
                    }
                }
            }

            return retval;

            //StockHolder = x.NadgyNavigation == null ? null : new GuidNom
            //{
            //    Guid = (Guid)x.NadgyNavigation.Contact,
            //    Nom = x.NadgyNavigation.ContactNavigation.CliClient.Ref == null ? x.NadgyNavigation.ContactNavigation.FullNom : x.NadgyNavigation.ContactNavigation.CliClient.Ref
            //},
            //Ccx = x.NadmsNavigation == null ? null : new GuidNom
            //{
            //    Guid = (Guid)x.NadmsNavigation.Contact,
            //    Nom = x.NadmsNavigation.ContactNavigation.CliClient.Ref == null ? x.NadmsNavigation.ContactNavigation.FullNom : x.NadmsNavigation.ContactNavigation.CliClient.Ref
            //},
            //Items = x.EdiInvrptItems.Select(y => new EdiversaInvrptModel.Item
            //{
            //    Qty = y.Qty,
            //    Sku = y.Sku,
            //    Lin = y.Lin
            //}).ToList()
        }
    }
}
