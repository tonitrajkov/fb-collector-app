using System.Collections.Generic;

namespace FbCollector.Models
{
    public class SystemLanguage
    {
        public string Code { get; set; }

        public List<LocalizationModel> Items { get; set; }
    }
}
