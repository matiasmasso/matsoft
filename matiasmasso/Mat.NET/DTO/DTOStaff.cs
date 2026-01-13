using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOStaff : DTOContact
    {
        public string Abr { get; set; }
        [JsonIgnore]
        public Byte[] Avatar { get; set; }

        public DateTime Alta { get; set; }
        public DateTime Baixa { get; set; }
        public DateTime Birth { get; set; }
        public DTOEnums.Sexs Sex { get; set; }
        public string NumSs { get; set; }
        public DTOIban Iban { get; set; }
        public DTOStaffPos StaffPos { get; set; }

        public DTOStaffCategory Category { get; set; }
        public bool Teletrabajo { get; set; }

        public DTOStaff() : base()
        {
        }

        public DTOStaff(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOStaff FromContact(DTOContact oContact)
        {
            DTOStaff retval = null;
            if (oContact == null)
                retval = new DTOStaff();
            else
            {
                retval = new DTOStaff(oContact.Guid);
                {
                    var withBlock = retval;
                    withBlock.Emp = oContact.Emp;
                    withBlock.Nom = oContact.Nom;
                    withBlock.NomComercial = oContact.NomComercial;
                    withBlock.Nifs = oContact.Nifs;
                    withBlock.Address = oContact.Address;
                    withBlock.ContactClass = oContact.ContactClass;
                    withBlock.Lang = oContact.Lang;
                    withBlock.Rol = oContact.Rol;
                }
            }
            return retval;
        }


        public static string Posicio(DTOStaff oStaff, DTOLang oLang)
        {
            string retval = "";
            if (oStaff != null)
            {
                if (oStaff.StaffPos != null)
                    retval = DTOStaffPos.Nom(oStaff.StaffPos, oLang);
            }
            return retval;
        }

        public static string Categoria(DTOStaff oStaff)
        {
            string retval = "";
            if (oStaff != null)
            {
                if (oStaff.Category != null)
                    retval = oStaff.Category.Nom;
            }
            return retval;
        }

        public static string SegSocialGrup(DTOStaff oStaff)
        {
            string retval = "";
            if (oStaff != null)
            {
                if (oStaff.Category != null)
                {
                    if (oStaff.Category.SegSocialGrup != null)
                        retval = oStaff.Category.SegSocialGrup.nom;
                }
            }
            return retval;
        }

        public static string AliasOrNom(DTOStaff oStaff)
        {
            string retval = oStaff.Abr;
            if (retval == "")
                retval = oStaff.Nom;
            if (retval == "")
                retval = oStaff.FullNom;
            return retval;
        }

        public int Age(DateTime toFch = default(DateTime))
        {
            int retval = 0;

            if (this.Birth != default(DateTime))
            {
                if (toFch == default(DateTime))
                    toFch = DTO.GlobalVariables.Today();
                retval = toFch.Year - this.Birth.Year;
                // Go back to the year the person was born in case of a leap year
                if ((this.Birth.Date > toFch.AddYears(-retval)))
                    retval -= 1;
            }
            return retval;
        }


        public static MatHelper.Excel.Sheet excel(List<DTOStaff> oStaffs, DTOLang oLang, DTOExercici oExercici = null/* TODO Change to default(_) if this is not a reference type */)
        {
            string sSheetName = "staff";
            if (oExercici != null)
                sSheetName = oExercici.Year.ToString();
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sSheetName, "M+O Staff");
            {
                var withBlock = retval;
                withBlock.AddColumn("Nom", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("NIF", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Num SS", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Alta", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("Baixa", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                if (oExercici != null)
                    withBlock.AddColumn("Dies " + oExercici.Year, MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Home", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Dona", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Neixament", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("Edat", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Grup de Cotització", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Categoría Professional", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Posició", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Adreça", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Codi postal", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Població", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Telefon", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Iban", MatHelper.Excel.Cell.NumberFormats.PlainText);
            }

            foreach (var oStaff in oStaffs)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                {
                    var withBlock = oRow;
                    withBlock.AddCell(oStaff.Nom);
                    withBlock.AddCell(oStaff.PrimaryNifValue());
                    withBlock.AddCell(oStaff.NumSs);
                    withBlock.AddCell(oStaff.Alta);
                    withBlock.AddCell(oStaff.Baixa);
                    if (oExercici != null)
                        withBlock.AddFormula("IF(ISBLANK(RC[-1]),\"" + oExercici.Year + "-12-31\",MIN(\"" + oExercici.Year + "-12-31\",RC[-1]))-MAX(\"" + oExercici.Year + "-01-01\",RC[-2])");
                    withBlock.AddCell(oStaff.Sex == DTOEnums.Sexs.male ? 1 : 0);
                    withBlock.AddCell(oStaff.Sex == DTOEnums.Sexs.female ? 1 : 0);
                    withBlock.AddCell(oStaff.Birth);
                    if (oStaff.Birth == default(DateTime))
                        withBlock.AddCell();
                    else if (oExercici == null)
                        withBlock.AddCell(oStaff.Age());
                    else
                        withBlock.AddFormula(oStaff.Age(oExercici.FirstFch()).ToString());
                    withBlock.AddCell(DTOStaff.SegSocialGrup(oStaff));
                    withBlock.AddCell(DTOStaff.Categoria(oStaff));
                    withBlock.AddCell(DTOStaff.Posicio(oStaff, oLang));
                    oRow.AddCell(oStaff.Address.Text);
                    oRow.AddCell(oStaff.Address.Zip.ZipCod);
                    oRow.AddCell(DTOAddress.LocationFullNom(oStaff.Address, oLang));
                    oRow.AddCell(oStaff.Telefon);
                    oRow.AddCell(DTOIban.Formated(oStaff.Iban.Digits));
                }
            }
            return retval;
        }
    }
}
