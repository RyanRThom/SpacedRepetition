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
            string userId = user.Id;
            user.Decks.Add(deck);
            _context.Users.Find(userId).Decks.Add(deck);
            _context.SaveChanges();
        }
        public void EditDeck(Deck deck)
        {
            _context.Entry(deck).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteDeck(ApplicationUser user, Deck deck)
        {
            _context.Users.Find(user.Id).Decks.Remove(deck);
            _context.SaveChanges();
        }
    }
}