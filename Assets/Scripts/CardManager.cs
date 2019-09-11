using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager CARDMANAGER;

    public List<Sprite> sprites = new List<Sprite>();
    public List<Card> cards = new List<Card>();

    void Start()
    {
        if (CARDMANAGER == null) CARDMANAGER = this;
        FillCardList();
    }

    void FillCardList()
    {
        //Ideally this gets moved to an editor script
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                Card newCard = new Card();
                newCard.suit = (Suits)i;
                newCard.value = j;
                newCard.sprite = sprites[(13 * (i - 1)) + j - 1];
                cards.Add(newCard);
            }
        }
        cards.TrimExcess();
    }

    //
    /*
    No need to shuffle here, so commenting it out for now 
    public void Shuffle(List<Card> l)
    {
        var count = l.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var temp = l[i];
            l[i] = l[r];
            l[r] = temp;
        }
    }
    */
}
