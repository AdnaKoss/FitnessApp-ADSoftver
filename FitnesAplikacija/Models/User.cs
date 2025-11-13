using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FitnesAplikacija.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, System.Diagnostics.CodeAnalysis.NotNull]
        public string Username { get; set; }

        [System.Diagnostics.CodeAnalysis.NotNull]
        public string DateOfBirth { get; set; }

        [Unique, System.Diagnostics.CodeAnalysis.NotNull]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.NotNull]
        public string Mobile { get; set; }

        [System.Diagnostics.CodeAnalysis.NotNull]
        public string Password { get; set; }
    }
}