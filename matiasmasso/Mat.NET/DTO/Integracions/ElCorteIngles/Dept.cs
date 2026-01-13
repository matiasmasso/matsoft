using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Integracions.ElCorteIngles
{
    public class Dept : DTOBaseGuid
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Tel { get; set; }

        public List<bool> PlantillaModSkuWeekDays { get; set; }
        public DTODocFile PlantillaModSkuDocFile { get; set; }

        public DTOUser Manager { get; set; }
        public DTOUser Assistant { get; set; }

        public enum Ids
        {
            none,
            _053,
            _068,
            _934
        }

        public Dept() : base()
        {
            //PlantillaModSkuWeekDays = new bool[] { false, false, false, false, false, false, false }.ToList(); 
        }
        public Dept(Guid guid, string nom = "") : base(guid)
        {
            PlantillaModSkuWeekDays = new bool[] { false, false, false, false, false, false, false }.ToList();
            Nom = nom;
        }

        public Ids Cod()
        {
            Ids retval = Ids.none;
            switch (Id)
            {
                case "053":
                    retval = Ids._053;
                    break;
                case "068":
                    retval = Ids._068;
                    break;
                case "934":
                    retval = Ids._934;
                    break;
            }
            return retval;
        }

        public bool PlantillaModSkuIsActive()
        {
            DayOfWeek wk = DTO.GlobalVariables.Today().DayOfWeek;
            List<bool> wks = this.PlantillaModSkuWeekDays;
            bool retval = wks[(int)wk];
            return retval;
        }

        public static Dept FromId(string id)
        {
            Dept retval = null;
            switch (id.Trim())
            {
                case "053":
                    retval = new Dept(new Guid("8B94EE26-19A5-4BF5-AD37-4897CA6B4340"));
                    retval.Id = id;
                    break;
                case "068":
                    retval = new Dept(new Guid("8D506781-0874-4243-ACEA-59241358EB81"));
                    retval.Id = id;
                    break;
                case "934":
                    retval = new Dept(new Guid("73938EF4-7076-40E4-9FF5-9FBC3F671284"));
                    retval.Id = id;
                    break;
            }
            return retval;
        }
    }
}
