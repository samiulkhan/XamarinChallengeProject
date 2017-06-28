using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAllianceApp.Models;

namespace XamarinChallenge.Models
{
    public class MovieList : List<Character>
    {
        public string MovieName { get; set; }
        public List<Character> Characters => this;
    }
}
