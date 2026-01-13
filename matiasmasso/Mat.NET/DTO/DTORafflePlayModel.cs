namespace DTO
{
    public class DTORafflePlayModel
    {
        public Srcs Src { get; set; }
        public DTORaffleParticipant Participant { get; set; }
        public DTOStoreLocator3 StoreLocator { get; set; }

        public enum Srcs
        {
            website,
            app
        }
    }
}
