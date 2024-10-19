using Core.Records.Bases;

namespace Core.Responses.Bases
{
    public record Response : Record
    {
        public bool IsSuccessful { get; }
        public string Message { get; }

        public Response(bool isSuccessful, string message, int id) : base(id)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public Response()
        {
            Message = string.Empty;
        }
    }
}
