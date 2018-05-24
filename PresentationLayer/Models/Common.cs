using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public static class Common
    {
        public static int NumOfPopular = 5;
        public static int NumOfOldMonths = -1;
        public static int HomePageNumOfNews = 5;
        public static int DefaultTake = 5;

        public static int NumOfHomeSections = 3;
        public static int NumOfNewsInHomeSection = 5;
        public static int IconHeight = 60;
        public static int IconWidth = 60;

        public static string DateFormat = "dd-MMM-yyyy h:mm tt";

        #region Application Roles

        public static string Adminstration = "Adminstration";

        #endregion
    }
}