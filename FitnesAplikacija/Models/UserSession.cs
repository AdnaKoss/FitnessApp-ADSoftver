using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnesAplikacija.Models;

namespace FitnesAplikacija.Models
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }
    }
}
