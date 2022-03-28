using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Helpers
{
    public static class AuthorizedResultHelper
    {
        public static ActionResult AuthorizedResult(ViewResult result, bool isTournamentAdmin)
        {
            result.ViewData["IsTournamentAdmin"] = isTournamentAdmin;
            return result;
        }
    }
}
