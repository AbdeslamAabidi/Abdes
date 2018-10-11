using Abdes.Enum;

namespace Abdes.Data
{
    public class Results
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
