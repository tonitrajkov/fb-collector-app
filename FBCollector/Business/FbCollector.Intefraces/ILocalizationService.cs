using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface ILocalizationService
    {
        List<LanguageModel> GetSupportedLanguages();

        List<LocalizationModel> LoadLanguage(string langCode);
    }
}
