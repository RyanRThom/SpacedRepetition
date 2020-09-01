using SpacedRepetition.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SpacedRepetition.Repositories
{
    public class DeckRepository
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public List<Deck> AddDeckList(ApplicationUser user)
        {
            var deckList = new List<Deck>();
            _context.Users.Find(user.Id).Decks = deckList;
            _context.SaveChanges();
            return deckList;
        }
        public List<Deck> GetAllDecks(ApplicationUser user)
        {
            return _context.Users.Find(user.Id).Decks;
        }
        public Deck GetDeckById(ApplicationUser user, int id)
        {
            return _context.Users.Find(user.Id).Decks.SingleOrDefault(d => d.Id == id);
        }
        public void AddDeck(ApplicationUser user, Deck deck)
        {
            _context.Users.Find(user.Id).Decks.Add(deck);
            _context.SaveChanges();
        }
        public void EditDeck(Deck deck)
        {
            _context.Entry(deck).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public List<Card> GetAllCards(ApplicationUser user, Deck deck)
        {
            return _context.Users.Find(user.Id).Decks.SingleOrDefault(d => d.Id == deck.Id).Cards;
        }

        public void DeleteDeck(ApplicationUser user, Deck deck)
        {
            _context.Users.Find(user.Id).Decks.Remove(deck);
            _context.SaveChanges();
        }
        public List<Card> AddCardList(ApplicationUser user, Deck deck)
        {
            List<Card> cards = new List<Card>();
            _context.Users.Find(user.Id).Decks.SingleOrDefault(d => d.Id == deck.Id).Cards = cards;
            _context.SaveChanges();
            return cards;
        }

        public void DeleteCard(ApplicationUser user, Deck deck, Card card)
        {
            _context.Users.Find(user.Id).Decks.SingleOrDefault(d => d.Id == deck.Id).Cards.Remove(card);
            _context.SaveChanges();
        }

        public void EditCard(Card card)
        {
            _context.Entry(card).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Card GetCardById(ApplicationUser user, Deck deck, int cardId)
        {
            return _context.Users.Find(user.Id).Decks.SingleOrDefault(d => d.Id == deck.Id).Cards.SingleOrDefault(c => c.Id == cardId);
        }

        public void AddCard(ApplicationUser user, Deck deck, Card card)
        {
            _context.Users.Find(user.Id).Decks.SingleOrDefault(d => d.Id == deck.Id).Cards.Add(card);
            _context.SaveChanges();
        }

        public List<Card> GetCardsToReview(ApplicationUser user)
        {
            List<Card> toReview = new List<Card>();
            foreach (Deck deck in _context.Users.Find(user.Id).Decks)
            {
                foreach (Card card in deck.Cards)
                {
                    if (DateTime.Compare(card.NextReview, DateTime.Now) < 0)
                    {
                        toReview.Add(card);
                    }
                }
            }
            return toReview;
        }

        public List<Deck> GetDeckByName(ApplicationUser user, string deckName)
        {
            List<Deck> decks = _context.Users.Find(user.Id).Decks.Where(x => x.Name == deckName).ToList();
            return decks;
        }
    }
}