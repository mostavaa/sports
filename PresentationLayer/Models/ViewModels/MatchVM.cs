using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models.ViewModels
{
    public class MatchVM
    {
        public string link { get; set; }
        public string firstTeam { get; set; }
        public string secondTeam { get; set; }
        public string firstTeamGoals { get; set; }
        public string secondTeamGoals { get; set; }
        public string date { get; set; }
        public bool isPlayed { get; set; }
    }
}