using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class TemplateDbModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public TranslationDbModel TranslateContent { get; set; }

        public int? TranslateContentId { get; set; }

        public string Header { get; set; }

        public TranslationDbModel TranslateHeader { get; set; }

        public int? TranslateHeaderId { get; set; }
    }
}
