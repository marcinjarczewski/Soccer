using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class TranslationDbModel
    {
        public int Id { get; set; }

        public ICollection<TranslationEntryDbModel> TranslationEntries { get; set; }
    }
}
