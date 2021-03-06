using CC.Core.Domain;

namespace MF.Core.Domain
{
    // this is fucked should have a third Interface so that I can restrict repo from saving lookuptypes
    public class UserRole : Entity, ILookupType
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public override void UpdateSelf(Entity entity)
        {
            var userRole = (UserRole) entity;
            Name = userRole.Name;
            Description = userRole.Description;
        }
    }
}