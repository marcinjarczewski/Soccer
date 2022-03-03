using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class LanguageDbModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TranslationEntryDbModel> TranslationEntries { get; set; }
    }
}
