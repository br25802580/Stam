using log4net;
using System;

namespace RealEstate
{
 public static   class HandleErrorUtils
    {
        public static void HandleError(this ILog log, Exception ex, string message = "Convertion Error")
        {
            log.Error("Error!", ex);
        }
    }
}
