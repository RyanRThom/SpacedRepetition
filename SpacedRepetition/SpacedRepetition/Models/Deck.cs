using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacedRepetition.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Card> Cards { get; set; }
    }
}