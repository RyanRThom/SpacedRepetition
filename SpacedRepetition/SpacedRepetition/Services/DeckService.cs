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
        public List<Card> GetAllCards(ApplicationUser user, Deck deck)
        {
            return _repository.GetAllCards(user, deck);
        }

        public List<Card> AddCardList(ApplicationUser user, Deck deck)
        {
            return _repository.AddCardList(user, deck);
        }
        public void AddCard(ApplicationUser user, Deck deck, Card card)
        {
            _repository.AddCard(user, deck, card);
        }

        public Card GetCardById(ApplicationUser user, Deck deck, int cardId)
        {
            return _repository.GetCardById(user, deck, cardId);
        }
        public void EditCard(Card card)
        {
            _repository.EditCard(card);
        }

        public void DeleteCard(ApplicationUser user, Deck deck, Card card)
        {
            _repository.DeleteCard(user, deck, card);
        }

        public List<Card> GetCardsToReview(ApplicationUser user)
        {
            return _repository.GetCardsToReview(user);
        }
    }
}