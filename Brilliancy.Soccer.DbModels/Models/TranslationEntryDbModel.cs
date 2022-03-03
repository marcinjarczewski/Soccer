using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class TranslationEntryDbModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public TranslationDbModel Translation { get; set; }

        public int TranslationId { get; set; }

        public LanguageDbModel Language { get; set; }

        public int LanguageId { get; set; }
    }
}
