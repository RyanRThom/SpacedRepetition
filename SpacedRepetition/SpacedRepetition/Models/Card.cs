using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpacedRepetition.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Display(Name = "Front")]
        public string CardFront { get; set; }

        [Display(Name = "Back")]
        public string CardBack { get; set; }
        public int Level { get; set; }
        public DateTime NextReview { get; set; }

        public Deck Deck { get; set; }
    }
}