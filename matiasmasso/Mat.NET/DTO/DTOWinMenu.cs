using MatHelperStd;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOWinMenuItem : DTOBaseGuid
    {
        public DTOWinMenuItem parent { get; set; }
        public DTOLangText langText { get; set; }
        public int ord { get; set; }
        public string action { get; set; }
        public Cods cod { get; set; }
        public CustomTargets customTarget { get; set; }
        public DTOBaseGuid tag { get; set; }

        public MimeCods Mime { get; set; }
        [JsonIgnore]
        public Byte[] icon { get; set; }

        public List<DTOEmp> emps { get; set; }
        public List<DTORol> rols { get; set; }
        public List<DTOWinMenuItem> children { get; set; }

        public enum Cods
        {
            notSet,
            folder,
            item
        }

        public enum CustomTargets
        {
            none,
            bancs,
            staff,
            reps
        }

        public DTOWinMenuItem() : base()
        {
            children = new List<DTOWinMenuItem>();
            langText = new DTOLangText();
            rols = new List<DTORol>();
        }

        public DTOWinMenuItem(Guid oGuid) : base(oGuid)
        {
            children = new List<DTOWinMenuItem>();
            langText = new DTOLangText();
            rols = new List<DTORol>();
        }

        public static DTOWinMenuItem Factory(DTOWinMenuItem oParent)
        {
            DTOWinMenuItem retval = new DTOWinMenuItem();
            {
                var withBlock = retval;
                withBlock.parent = oParent;
                withBlock.emps = oParent.emps;
            }
            return retval;
        }

        public static void loadBancs(List<DTOBanc> items, ref DTOWinMenuItem oParent)
        {
            if (oParent.children.Count == 0)
            {
                foreach (var item in items)
                {
                    DTOWinMenuItem oMenuitem = new DTOWinMenuItem();
                    {
                        var withBlock = oMenuitem;
                        withBlock.icon = item.Logo;
                        withBlock.tag = item;
                        withBlock.langText = new DTOLangText(item.abrOrNom());
                        withBlock.cod = DTOWinMenuItem.Cods.item;
                        withBlock.customTarget = DTOWinMenuItem.CustomTargets.bancs;
                    }
                    oParent.children.Add(oMenuitem);
                }
            }
        }

        public static void loadReps(List<DTORep> oReps, ref DTOWinMenuItem oParent)
        {
            if (oParent.children.Count == 0)
            {
                foreach (DTORep item in oReps)
                {
                    DTOWinMenuItem oMenuitem = new DTOWinMenuItem();
                    {
                        var withBlock = oMenuitem;
                        withBlock.icon = item.Img48;
                        withBlock.tag = item;
                        withBlock.langText = new DTOLangText(item.NickName);
                        withBlock.cod = DTOWinMenuItem.Cods.item;
                        withBlock.customTarget = DTOWinMenuItem.CustomTargets.reps;
                    }
                    oParent.children.Add(oMenuitem);
                }
            }
        }

        public static void loadStaffs(List<DTOStaff> oStaffs, ref DTOWinMenuItem oParent)
        {
            if (oParent.children.Count == 0)
            {
                foreach (DTOStaff item in oStaffs)
                {
                    DTOWinMenuItem oMenuitem = new DTOWinMenuItem();
                    {
                        var withBlock = oMenuitem;
                        withBlock.icon = item.Logo;
                        withBlock.tag = item;
                        withBlock.langText = new DTOLangText(item.Abr);
                        withBlock.cod = DTOWinMenuItem.Cods.item;
                        withBlock.customTarget = DTOWinMenuItem.CustomTargets.staff;
                    }
                    oParent.children.Add(oMenuitem);
                }
            }
        }
    }
}
