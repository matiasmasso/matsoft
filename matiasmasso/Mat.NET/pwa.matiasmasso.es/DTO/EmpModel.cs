using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace DTO
{
    public class EmpModel : BaseGuid, IModel
    {
        public EmpIds Id { get; set; }
        public int? Ord { get; set; }
        public string? Nom { get; set; }
        public string? Abr { get; set; }
        public Guid? Org { get; set; }

        public DateOnly? FchFrom { get; set; }
        public DateOnly? FchTo { get; set; }

        public CertModel? Cert { get; set; }

        public enum EmpIds : int
        {
            NotSet,
            [Display(Name = "M+O")] MatiasMasso,
            [Display(Name = "FRONT FRED")] FrontFred,
            [Display(Name = "BEBE DIGITAL")] BebeDigital,
            [Display(Name = "PICAPAQUET")] PicaPaquet,
            [Display(Name = "Espai Blau")] EspaiBlau,
            [Display(Name = "MM-VR")] MMC,
            [Display(Name = "TATITA")] Tatita,
            [Display(Name = "iWannaBit")] iWannaBit,
            [Display(Name = "E.S.Prim")] Prim,
            [Display(Name = "Rosa Mª")] Rosa,
            [Display(Name = "E.S.Horta")] Horta
        }

        public EmpModel() { }

        public EmpModel(Guid guid) : base(guid) { }


        public static string MailBody(EmpIds emp, String content)
        {
            string retval = "";
            switch (emp)
            {
                case EmpIds.MatiasMasso:
                    retval = string.Format(MMO_MailTemplate(), content);
                    break;
                default:
                    retval = content;
                    break;
            }
            return retval;
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = false;
            if (!string.IsNullOrEmpty(searchTerm))
                retval = (Nom?.Contains(searchTerm) ?? false) || (Abr?.Contains(searchTerm) ?? false);
            return retval;
        }

        public string SortOrder()
        {
            var retval = $"{(FchTo == null ? 0 : 1)}{Ord:000}{(int)Id:000}";
            return retval;
        }

        public List<int> YearsDescendingRange()
        {
            var yearFrom = (FchFrom ?? DateOnly.FromDateTime( new DateTime(1985, 5, 28))).Year;
            var yearTo = DateTime.Today.Year;
            var asc = Enumerable.Range(yearFrom, yearTo - yearFrom +1).ToList();
            var retval = asc.OrderByDescending(x => x).ToList();
            return retval;
        }

        public static string MMO_MailTemplate()
        {
            return @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" style=""height: 100%;"">
<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>
        Circular
    </title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
</head>
<body bgcolor=""#F5F5F5"" leftmargin=""0"" topmargin=""0"" marginwidth=""0"" marginheight=""0"" style=""margin:0;padding:0;"">

    <table cellpadding=""0"" cellspacing=""0"" width=""100%"" bgcolor=""#F5F5F5"" style=""font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 19px; line-height: 1.4em; height: 100%; "">
        <tr>
            <td style=""vertical-align: top;"">

                <table align=""center"" cellpadding=""0"" cellspacing=""0"" width=""600"" bgcolor=""#FFFFFF"" style=""font-family: arial, sans-serif; font-size: 12pt; height: 100%;"">

                    <!-- Section Header -->
                    <tr>
                        <td align=""right"" bgcolor=""#DDDDDD"" style=""color: #777777; padding: 15px 10px 5px 10px; font-size: 11pt"">
                                                    </td>
                    </tr>
                    <!-- End Section Header -->
                    <!-- Begin Section Content -->
                    <tr>
                        <td style=""padding:10px;"">
                        {0}
                        </td>
                    </tr>
                    <!-- End Section Content -->
                    <!-- Begin Section Footer -->
                    <tr>
                        <td bgcolor=""#DDDDDD"">
                            <table width=""100%"" style=""color: #777777; font-family: arial, sans-serif; font-size: 11pt; line-height: 1.2em; "">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td bgcolor=""#DDDDDD"" style=""width:38px;"">
                                        <a href=""https://www.matiasmasso.es"" title=""MATIAS MASSO, S.A."">
                                            <img src=""https://www.matiasmasso.es/Media/Img/Logos/logo.mmo.38x33.gif"" width=""38"" height=""33"" alt=""Logo de MATIAS MASSO, S.A."" />
                                        </a>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td width=""100%"" align=""left"" style=""padding: 10px 10px 10px 0;"">
                                        <a href=""https://www.matiasmasso.es"" style=""text-decoration: none; color: #777777; font-weight: bold;"">MATIAS MASSO, S.A.</a><br />
                                        Diagonal 403, Barcelona
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- End Section Footer -->
                </table>

            </td>
        </tr>
    </table>

</body>

</html>";
        }

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Abr ?? Nom ?? Id.ToString();
        }
    }
}
