using SpacedRepetition.Models;
using SpacedRepetition.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacedRepetition.Services
{
    public class DeckService
    {
        private DeckRepository _repository = new DeckRepository();

        public List<Deck> AddDeckList(ApplicationUser user)
        {
            return _repository.AddDeckList(user);
        }
        public List<Deck> GetAllDecks(ApplicationUser user)
        {
            return _repository.GetAllDecks(user);
        }
        public Deck GetDeckById(ApplicationUser user, int id)
        {
            return _repository.GetDeckById(user, id);
        }
        public void AddDeck(ApplicationUser user, Deck deck)
        {
            _repository.AddDeck(user, deck);
        }
        public void EditDeck(Deck deck)
        {
            _repository.EditDeck(deck);
        }
        public void DeleteDeck(ApplicationUser user, Deck deck)
        {
            _repository.DeleteDeck(user, deck);
        }
    }
}