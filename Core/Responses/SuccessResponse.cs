using Core.Responses.Bases;

namespace Core.Responses
{
    public record SuccessResponse : Response
    {
        public SuccessResponse(string message, int id) : base(true, message, id)
        {
        }

        public SuccessResponse(string message) : base(true, message, default)
        {
        }

        public SuccessResponse(int id) : base(true, string.Empty, id)
        {
        }

        public SuccessResponse() : base(true, string.Empty, default)
        {
        }
    }
}
