namespace DTO.Models.Base
{
    public class LangText
    {
        public string Esp { get; set; }
        public string Cat { get; set; }
        public string Eng { get; set; }
        public string Por { get; set; }

        public LangText(string esp, string cat = "", string eng = "", string por = "")
        {
            Esp = esp;
            Cat = cat;
            Eng = eng;
            Por = por;
        }
    }
}
