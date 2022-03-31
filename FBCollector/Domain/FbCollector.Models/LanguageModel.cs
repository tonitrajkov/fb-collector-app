
namespace FbCollector.Models
{
    public class LanguageModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public bool IsDefault { get; set; }

        public bool IsCurrent { get; set; }
    }
}
