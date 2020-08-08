using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SpacedRepetition.Models;
using SpacedRepetition.Services;
using SpacedRepetition.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpacedRepetition.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private DeckService _service = new DeckService();
        // GET: Card
        public ActionResult Index(int? id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = _service.GetDeckById(user, (int)id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            List<Card> cards = _service.GetAllCards(user, deck);
            if (cards == null)
            {
                cards = _service.AddCardList(user, deck);
            }
            return View(cards);
        }

        public ActionResult Create(int? id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = _service.GetDeckById(user, (int)id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Card card)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            int deckId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            Deck deck = _service.GetDeckById(user, deckId);
            card.Level = 0;
            card.NextReview = DateTime.Now;
            card.Deck = deck;
            if (ModelState.IsValid)
            {
                _service.AddCard(user, deck, card);

                return RedirectToAction("Index", new { id = deckId });
            }
            return View(deck);
        }
        public ActionResult Edit(int? deckId, int? cardId)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (deckId == null || cardId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = _service.GetDeckById(user, (int)deckId);
            if (deck == null)
            {
                return HttpNotFound();
            }
            Card card = _service.GetCardById(user, deck, (int)cardId);
            if (card == null)
            {
                return HttpNotFound();
            }
            EditCardViewModel viewModel = new EditCardViewModel();
            viewModel.CardFront = card.CardFront;
            viewModel.CardBack = card.CardBack;
            viewModel.DeckId = deck.Id;
            viewModel.CardId = card.Id;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCardViewModel viewModel)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            int deckId = viewModel.DeckId;
            Deck deck = _service.GetDeckById(user, deckId);
            int cardId = viewModel.CardId;
            Card card = _service.GetCardById(user, deck, cardId);
            card.CardFront = viewModel.CardFront;
            card.CardBack = viewModel.CardBack;
            if (ModelState.IsValid)
            {
                _service.EditCard(card);
                return RedirectToAction("Index", new { id = deckId });
            }
            return View(card);
        }
        public ActionResult Delete(int? deckId, int? cardId)
        {
            if (deckId == null || cardId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Deck deck = _service.GetDeckById(user, (int)deckId);
            if (deck == null)
            {
                return HttpNotFound();
            }
            Card card = _service.GetCardById(user, deck, (int)cardId);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int deckId, int cardId)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Deck deck = _service.GetDeckById(user, deckId);
            Card card = _service.GetCardById(user, deck, cardId);
            _service.DeleteCard(user, deck, card);
            return RedirectToAction("Index", new { id = deckId });
        }
    }
}