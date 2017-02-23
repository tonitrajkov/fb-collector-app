using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FbCollector.Domain;
using FbCollector.Domain.Mapper;
using FbCollector.Infrastructure;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Intefraces;
using FbCollector.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernateCfg;

namespace FbCollector.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IRepository<Localization> _localizationRepository;
        private readonly IRepository<Language> _languageRepository;

        public LocalizationService()
        {
            _localizationRepository = ServiceLocator.Current.GetInstance<IRepository<Localization>>();
            _languageRepository = ServiceLocator.Current.GetInstance<IRepository<Language>>();
        }

        public List<LanguageModel> GetSupportedLanguages()
        {
            var languages = _languageRepository.Query();

            return languages.Select(l => l.ToModel()).ToList();
        }

        public List<LocalizationModel> LoadLanguage(string langCode)
        {
            if (string.IsNullOrEmpty(langCode))
            {
                var defaultLang = _languageRepository.Query()
                                   .FirstOrDefault(l => l.IsDefault);

                if (defaultLang == null)
                    throw new FbException("LANG_DOESNT_EXIST");

                langCode = defaultLang.Code;
            }

            var languages = _localizationRepository.Query()
                .Where(l => l.LanguageCode.ToLower() == langCode.ToLower());

            return languages.Select(l => l.ToModel()).ToList();;
        }
    }
}
