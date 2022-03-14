using System;
using System.IO;
namespace Brilliancy.Soccer.Common.Helpers
{

    public static class GlobalUrlHelper
    {
        private static string _appUrl { get; set; }
        public static string AppUrl 
        { 
            get => _appUrl;
            set
            {
                if(string.IsNullOrEmpty(_appUrl))
                {
                    _appUrl = value;
                }
            }
        }
    }
}
