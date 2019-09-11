using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CheckWinnings;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public BetManager betManager;
    public CardManager cardManager;

    public static GameManager MANAGER;

    [Header("Cards")]
    public CardObject[] cards;

    private int curIndex = 0;
    private CardsDealt ids;

    [Header("UI Elements")]
    public CanvasGroup cardGroup;
    public CanvasGroup buttonGroup;
    public Button dealButton;

    void Start()
    {
        if (MANAGER == null)
        {
            MANAGER = this;
        }
    }

    public void StartGame()
    {
        buttonGroup.interactable = false;
        betManager.TakeCurrency();
        ids = new CardsDealt();
        curIndex = 0;
        foreach (var card in cards)
        {
            card.Hide();
        }
        StartCoroutine(FlipCardsWithDelay());
    }

    IEnumerator FlipCardsWithDelay()
    {
        foreach (var card in cards)
        {
            card.held = false;
            card.cardData = cardManager.cards[ids.cardIds[curIndex]];
            card.Flip();
            curIndex++;
            yield return new WaitForSeconds(0.1f);
        }
        cardGroup.blocksRaycasts = true;
        dealButton.interactable = true;
    }


    public void Deal()
    {
        cardGroup.blocksRaycasts = false;
        foreach (var card in cards)
        {
            if (card.held == false)
            {
                card.Hide();
            }
        }
        StartCoroutine(RedealCards());
    }

    IEnumerator RedealCards()
    {
        foreach (var card in cards)
        {
            if (card.held == false)
            {
                card.cardData = cardManager.cards[ids.cardIds[curIndex]];
                card.Flip();
                curIndex++;
                yield return new WaitForSeconds(0.1f);
            }
        }
        dealButton.interactable = false;
        buttonGroup.interactable = true;
        CheckForWinnings();
    }

    public void CheckForWinnings()
    {
        List<Card> a = new List<Card>();
        foreach(var card in cards)
        {
            a.Add(card.cardData);
        }
        a.TrimExcess();
        if(WinCheck.CheckForRoyalFlush(a)) betManager.AddWinnings(8);
        else if(WinCheck.CheckForStraightFlush(a)) betManager.AddWinnings(7);
        else if(WinCheck.CheckForFourOfAKind(a)) betManager.AddWinnings(6);
        else if(WinCheck.CheckForFullHouse(a)) betManager.AddWinnings(5);
        else if(WinCheck.CheckForFlush(a)) betManager.AddWinnings(4);
        else if(WinCheck.CheckForStraight(a)) betManager.AddWinnings(3);
        else if(WinCheck.CheckForThreeKind(a)) betManager.AddWinnings(2);
        else if(WinCheck.CheckForTwoPair(a)) betManager.AddWinnings(1);
        else if(WinCheck.CheckForJacksOrBetter(a)) betManager.AddWinnings(0);
        //If it gets here nothing happens, the game is already reset
        betManager.SetCurrencyUI();
    }
}
