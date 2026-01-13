using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Models
{
    public class JornadesLaboralsModel
    {
        public List<Staff> Staffs { get; set; }
        public DTOJornadaLaboral.Status Status { get; set; }
        public JornadesLaboralsModel()
        {
            Staffs = new List<Staff>();
        }

        public class Staff
        {
            public Guid Guid { get; set; }
            public string Abr { get; set; }
            public string Nom { get; set; }
            public List<Item> Items { get; set; }

            public Staff(Guid? guid = null)
            {
                Guid = (guid == null) ? Guid.NewGuid() : (Guid)guid;
                Items = new List<Item>();
            }

            public DTOJornadaLaboral Value(Item item)
            {
                DTOJornadaLaboral retval = new DTOJornadaLaboral(item.Guid);
                retval.Staff = new DTOStaff(Guid);
                retval.FchFrom = item.FchFrom;
                retval.FchTo = item.FchTo;
                return retval;
            }
        }

        public class Item
        {
            public Guid Guid { get; set; }
            public DateTime FchFrom { get; set; }
            public DateTime FchTo { get; set; }

            public Double Horas()
            {
                Double retval = 0;
                if (FchFrom > DateTime.MinValue && FchTo > DateTime.MinValue)
                {
                    retval = Math.Round((FchTo - FchFrom).TotalHours, 2);
                }
                return retval;
            }

            public bool IsOpen()
            {
                return FchTo == DateTime.MinValue;
            }



        }

        public List<int> Years()
        {
            return this.Staffs.SelectMany(a => a.Items).GroupBy(b => b.FchFrom.Year).Select(c => c.First().FchFrom.Year).ToList();
        }


        public MatHelper.Excel.Book Excel()
        {

            MatHelper.Excel.Book retval = new MatHelper.Excel.Book("Registro de Jornada Laboral");
            foreach (Staff staff in Staffs)
            {
                MatHelper.Excel.Sheet oSheet = new MatHelper.Excel.Sheet(staff.Abr);
                oSheet.AddColumn("Año", MatHelper.Excel.Cell.NumberFormats.Integer);
                oSheet.AddColumn("Mes", MatHelper.Excel.Cell.NumberFormats.W50);
                oSheet.AddColumn("Dia", MatHelper.Excel.Cell.NumberFormats.Integer);
                oSheet.AddColumn("Hora", MatHelper.Excel.Cell.NumberFormats.Integer);
                oSheet.AddColumn("Minutos", MatHelper.Excel.Cell.NumberFormats.Integer);
                oSheet.AddColumn("Hora", MatHelper.Excel.Cell.NumberFormats.Integer);
                oSheet.AddColumn("Minutos", MatHelper.Excel.Cell.NumberFormats.Integer);
                oSheet.AddColumn("Horas", MatHelper.Excel.Cell.NumberFormats.Decimal2Digits);
                foreach (Item item in staff.Items)
                {
                    MatHelper.Excel.Row oRow = oSheet.AddRow();
                    oRow.AddCell(item.FchFrom.Year);
                    oRow.AddCell(DTOLang.ESP().MesAbr(item.FchFrom.Month));
                    oRow.AddCell(item.FchFrom.Day);
                    oRow.AddCell(item.FchFrom.Hour);
                    oRow.AddCell(item.FchFrom.Minute);
                    oRow.AddCell(item.FchTo.Hour);
                    oRow.AddCell(item.FchTo.Minute);
                    oRow.AddCell(item.Horas());
                }
                retval.Sheets.Add(oSheet);
            }
            return retval;
        }
    }
}
