using Newtonsoft.Json;


namespace DTO
{
    public class DTOProductFeatureImage
    {
        public System.Guid Guid { get; set; }
        public int Ord { get; set; }
        [JsonIgnore]
        public byte[] Image { get; set; }

        public bool IsLoaded { get; set; }
        public bool IsNew { get; set; }
    }
}
