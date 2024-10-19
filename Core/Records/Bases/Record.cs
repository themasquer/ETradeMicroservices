namespace Core.Records.Bases
{
    public abstract record Record : IRecord
    {
        public int Id { get; set; }

        protected Record(int id)
        {
            Id = id;
        }

        protected Record()
        {
        }
    }
}
