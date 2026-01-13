namespace DTO
{
    public class DTOSepaText : DTOBaseId
    {
        public DTOLangText LangText { get; set; }


        public DTOSepaText(int iId) : base(iId)
        {
            LangText = new DTOLangText();
        }
        public DTOSepaText() : base(-1)
        {
            LangText = new DTOLangText();
        }
    }
}
