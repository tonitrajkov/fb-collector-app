using System.Collections.Generic;
using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface ILocalizationService
    {
        List<LanguageModel> GetSupportedLanguages();

        SystemLanguage LoadLanguage(string langCode);
    }
}
