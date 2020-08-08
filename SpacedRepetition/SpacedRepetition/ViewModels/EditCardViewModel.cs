using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacedRepetition.ViewModels
{
    public class EditCardViewModel
    {
        public string CardFront { get; set; }
        public string CardBack { get; set; }
        public int DeckId { get; set; }
        public int CardId { get; set; }
    }
}