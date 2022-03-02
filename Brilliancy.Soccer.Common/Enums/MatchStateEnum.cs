using Brilliancy.Soccer.Common.Translations;

namespace Brilliancy.Soccer.Common.Enums
{
    public enum MatchStateEnum
    {
        Creating = 1,
        Ongoing = 2,
        Finished = 3,
        Canceled = 4,
        Pending = 5
    }

    public static class MatchStateExtensions
    {
        public static string ToTranslatedString(this MatchStateEnum me)
        {
            switch (me)
            {
                case MatchStateEnum.Creating:
                    return CommonTranslations.MatchStateEnum_Creating;
                case MatchStateEnum.Ongoing:
                    return CommonTranslations.MatchStateEnum_Ongoing;
                case MatchStateEnum.Finished:
                    return CommonTranslations.MatchStateEnum_Finished;
                case MatchStateEnum.Canceled:
                    return CommonTranslations.MatchStateEnum_Canceled;
                case MatchStateEnum.Pending:
                    return CommonTranslations.MatchStateEnum_Pending;
                default:
                    return me.ToString();
            }
        }
    }
}
