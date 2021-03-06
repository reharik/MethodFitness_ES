using CC.Core.Domain;

namespace MF.Core.Domain
{
    public class DomainEntity : Entity
    {
        public virtual int CompanyId { get; set; }
    }

    public interface ILookupType
    {
        int EntityId { get; set; }
        string Name { get; set; }
    }
}