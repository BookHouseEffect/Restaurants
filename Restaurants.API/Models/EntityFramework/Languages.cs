using Restaurants.API.Models.Enums;
using System;
using System.Collections.Generic;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Languages : BaseEnumEntity
    {
        
        public string LanguageName { get; set; }

        protected Languages() { } //For EF

        private Languages(LanguageEnum @enum)
        {
            this.Id = (int)@enum;
            this.LanguageName = @enum.ToString();
        }

        public static implicit operator Languages(LanguageEnum @enum)
            => new Languages(@enum);

        public static implicit operator LanguageEnum(Languages language)
            => (LanguageEnum)language.Id;

        [NonSerialized]
        private ICollection<MenuLanguages> _MenuLanguage;

        public virtual ICollection<MenuLanguages> TheMenuLanguagesAssosiatedWithThisLanguages
        {
            get
            {
                return _MenuLanguage;
            }
            set
            {
                _MenuLanguage = value;
            }
        }

    }
}
