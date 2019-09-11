using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suits
{
    Clubs = 1,
    Diamonds = 2,
    Hearts = 3,
    Spades = 4
}
//Making the card fully public, not ideal but, gives me access easily and makes it easy to read in the inspector
[System.Serializable]
public class Card
{
    public Suits suit;
    public int value;

    public Sprite sprite;
}

public class UserData
{
    public int currency;

    public void SubstractCurrency(int a)
    {
        currency -= a;
    }

    public void AddCurrency(int a)
    {
        Debug.Log("Adding "+ a.ToString());
        currency += a;
    }
}

public class CardsDealt
{
    //There are 52 cards in the deck, so we want 10 unique cards everytime this is constructed.
    public List<int> cardIds = new List<int>();
    public CardsDealt()
    {
        for (int i = 0; i < 10; i++)
        {
            //TODO: Change this to be unique numbers!
            int rand = Random.Range(0, 52);
            while(isDuplicate(rand,cardIds))
            {
                rand = Random.Range(0, 52);
            }
            cardIds.Add(rand);
            cardIds.TrimExcess();
        }
    }
    private bool isDuplicate(int t,List<int> l)
    {
        foreach (var i in l)
        {
            if(t == i) return true;
        }
        return false;
    }
}
namespace CheckWinnings
{
    public class WinCheck
    {
        public static bool CheckForJacksOrBetter(List<Card> l)
        {
            foreach (var card in l)
            {
                if (card.value < 11)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckForTwoPair(List<Card> l)
        {
            for (int i = 0; i < l.Capacity - 1; i++)
            {
                for (int j = i + 1; j < l.Capacity; j++)
                {
                    if (l[i].value == l[j].value) return true;
                }
            }
            return false;
        }
        public static bool CheckForThreeKind(List<Card> l)
        {
            for (int i = 0; i < l.Capacity - 1; i++)
            {
                for (int j = i + 1; j < l.Capacity; j++)
                {
                    if (l[i].value == l[j].value)
                    {
                        for (int k = j + 1; k < l.Capacity; k++)
                        {
                            if (l[j].value == l[k].value) return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool CheckForStraight(List<Card> l)
        {
            List<int> ints = new List<int>();
            foreach (var card in l)
            {
                ints.Add(card.value);
            }
            ints.TrimExcess();
            //I dont think I saw anything against using built in methods, so just saving some time here
            ints.Sort();
            for (int i = 1; i < ints.Capacity; i++)
            {
                if (ints[i - 1] != ints[i]) return false;
            }
            return true;
        }
        public static bool CheckForFlush(List<Card> l)
        {
            for (int i = 1; i < l.Capacity; i++)
            {
                if (l[i].suit != l[i - 1].suit) return false;
            }
            return true;
        }
        public static bool CheckForFullHouse(List<Card> l)
        {
            List<int> ints = new List<int>();
            foreach (var card in l)
            {
                ints.Add(card.value);
            }
            ints.TrimExcess();
            ints.Sort();
            if (ints[0] != ints[1] || ints[3] != ints[4]) return false;
            if (ints[1] == ints[2] || ints[2] == ints[3]) return true;
            else
                return false;
        }
        public static bool CheckForFourOfAKind(List<Card> l)
        {
            for (int i = 0; i < l.Capacity - 1; i++)
            {
                for (int j = i + 1; j < l.Capacity; j++)
                {
                    if (l[i].value == l[j].value)
                    {
                        for (int k = j + 1; k < l.Capacity; k++)
                        {
                            if (l[j].value == l[k].value)
                            {
                                for (int o = k + 1; o < l.Capacity; o++)
                                {
                                    if (l[k].value == l[o].value)
                                        return true;
                                }

                            }
                        }
                    }
                }
            }
            return false;
        }
        public static bool CheckForStraightFlush(List<Card> l)
        {
            return (CheckForFlush(l) && CheckForStraight(l));
        }
        public static bool CheckForRoyalFlush(List<Card> l)
        {
            if (!CheckForFlush(l)) return false;
            List<int> ints = new List<int>();
            foreach (var card in l)
            {
                ints.Add(card.value);
            }
            ints.TrimExcess();
            ints.Sort();
            if(ints[0]== 1 && ints[1] == 10 && ints[2] == 11  && ints[3] == 12  && ints[4] == 13) return true;
            else return true;


        }

    }

}
