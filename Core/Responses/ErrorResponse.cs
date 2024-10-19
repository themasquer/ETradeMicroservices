using Core.Responses.Bases;

namespace Core.Responses
{
    public record ErrorResponse : Response
    {
        public ErrorResponse(string message) : base(false, message, default)
        {
        }

        public ErrorResponse() : base(false, string.Empty, default)
        {
        }
    }
}
