using DocumentFormat.OpenXml.Office2019.Word.Cid;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static MatHelperStd.JScriptDate;

namespace DTO
{
    public class DTOEdiversaErr
    {
        public string Src { get; set; }
        public string Filename { get; set; }
        public DateTime FchCreated { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();


        public DTOEdiversaErr(string filePath)
        {
            System.IO.FileInfo oFile = new System.IO.FileInfo(filePath);
            Filename = System.IO.Path.GetFileName(oFile.FullName);
            FchCreated = oFile.CreationTime;
            Src = ReadFile(oFile);

            foreach (var line in Lines())
            {
                Items.Add(new Item(line));
            }
        }

        public bool HasTextFieldWrongValue() => Items.Any(x => x.Fields.Count> (int)Item.Ids.ErrId && x.Fields[(int)Item.Ids.ErrId] == ((int)Item.ErrIds.TextFieldWrongValue).ToString());

        protected static string ReadFile(FileInfo oFile) => System.IO.File.ReadAllText(oFile.FullName);
        private List<string> Lines() => Regex.Split(Src, "\r\n|\r|\n").ToList();

        public class Item
        {
            public int Id { get; set; }
            public string Src { get; set; }
            public List<string> Fields { get; set; }

            public enum Ids
            {
                free0,
                free1,
                free2,
                ErrId,
                ErrReason,
                Lin,
                free6,
                free7,
                free8,
                fixF,
                ErrValue,
                FixN
            }

            public enum ErrIds
            {
                TextFieldWrongValue = 417
            }


            public Item(string line)
            {
                Src = line;
                Fields = line.Split('|').ToList();
            }
        }
    }
}
