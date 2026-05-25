using System.Reflection;
using UnityEngine;


namespace Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public CardType cardType;
        public CardNumber baseNumber;
        public int valueNumber;
        public Sprite cardSprite;

        public void InitializeNumberValue()
        {
            valueNumber = (int)baseNumber;
        }

        public enum CardType
        {
            Hearts,
            Diamonds,
            Spades,
            Clubs
        }

        public enum CardNumber
        {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
        }

    }
}