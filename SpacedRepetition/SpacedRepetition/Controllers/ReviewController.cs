using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SpacedRepetition.Models;
using SpacedRepetition.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpacedRepetition.Controllers
{
    [Authorize]
    public class ReviewController : BaseController
    {
        private DeckService _service = new DeckService();
        public ActionResult Index()
        {
            ApplicationUser user = GetCurrentUser();
            List<Card> model = _service.GetCardsToReview(user);
            return View(model);
        }

        public ActionResult Review(int? deckId, int? cardId)
        {
            ApplicationUser user = GetCurrentUser();
            Deck deck = _service.GetDeckById(user, (int)deckId);
            Card card = _service.GetCardById(user, deck, (int)cardId);
            card.Level += 1;
            card.NextReview = FindNextReview(card);
            _service.EditCard(card);
            return RedirectToAction("Index");
        }

        public DateTime FindNextReview(Card card)
        {
            int days = FindFib(card.Level);
            card.NextReview = DateTime.Now;
            return card.NextReview.AddDays(days);
        }

        public int FindFib(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            else
            {
                return FindFib(n - 1) + FindFib(n - 2);
            }
        }
    }
}