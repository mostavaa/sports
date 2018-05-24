using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models.ViewModels
{
    public class NewsVM
    {
        public string imageName { get; set; }
        public string link { get; set; }
        public string date { get; set; }
        public string desc { get; set; }
}
}