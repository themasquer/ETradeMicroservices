using Core.Records.Bases;

namespace Core.Entities.Bases
{
    public abstract class Entity : IRecord
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
