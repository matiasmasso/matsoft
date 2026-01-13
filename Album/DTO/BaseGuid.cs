
namespace DTO
{
    public class BaseGuid
    {
        public bool IsNew { get; set; } = true;

        public Guid Guid { get; set; } = System.Guid.NewGuid();

        public BaseGuid() { }
        public BaseGuid(Guid guid)
        {
            Guid = guid;
            IsNew = false;
        }
    }
}