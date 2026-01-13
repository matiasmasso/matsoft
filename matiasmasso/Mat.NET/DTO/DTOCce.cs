namespace DTO
{
    public class DTOCce
    {
        public DTOExercici Exercici { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOAmt Amt { get; set; }

        public DTOCce(DTOExercici oExercici, DTOPgcCta oCta) : base()
        {
            Exercici = oExercici;
            Cta = oCta;
        }

        public DTOCce(DTOPgcCta oCta, DTOAmt oAmt) : base()
        {
            Cta = oCta;
            Amt = oAmt;
        }
    }
}
