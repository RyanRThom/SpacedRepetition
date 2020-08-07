﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SpacedRepetition.Models;
using SpacedRepetition.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpacedRepetition.Controllers
{
    [Authorize]
    public class DeckController : Controller
    {
        private DeckService _service = new DeckService();
        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<Deck> decks = _service.GetAllDecks(user);
            if (decks == null)
            {
                decks = _service.AddDeckList(user);
            }
            return View(decks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deck deck)
        {
            deck.Cards = new List<Card>();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                _service.AddDeck(user, deck);

                return RedirectToAction("Index");
            }
            return View(deck);
        }

        public ActionResult Edit(int? id)
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
        public ActionResult Edit(Deck deck)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                _service.EditDeck(deck);
                return RedirectToAction("Index");
            }
            return View(deck);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Deck deck = _service.GetDeckById(user, (int)id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Deck deck = _service.GetDeckById(user, id);
            _service.DeleteDeck(user, deck);
            return RedirectToAction("Index");
        }
    }
}