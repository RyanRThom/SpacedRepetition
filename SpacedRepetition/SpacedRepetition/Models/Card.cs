using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacedRepetition.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardFront { get; set; }
        public string CardBack { get; set; }
        public int Level { get; set; }
        public DateTime NextReview { get; set; }

        public Deck Deck { get; set; }
    }
}