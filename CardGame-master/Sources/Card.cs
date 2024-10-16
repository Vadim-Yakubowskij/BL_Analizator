using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Sources

{
    public class Card
    {
        public string Rank { get; }

        public Card(string rank)
        {
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Rank}";
        }

    }
}
