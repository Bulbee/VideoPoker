using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    [SerializeField]
    private int[] betValues = new int[] { 1, 2, 3, 4, 5 };
    private int[,] payTables = new int[,]{  {1,2,3,4,6,9,25,50,250},
                                            {2,4,6,8,12,18,50,100,500},
                                            {3,6,9,12,18,27,75,150,750},
                                            {4,8,12,16,24,36,100,200,1000},
                                            {5,10,15,20,30,45,125,250,4000}};

    private int curBetIndex = 0;
    public int CurrentBet
    {
        get { return curBetIndex; }
    }

    [Header("UI Objects")]
    public Text betAmountText;
    public Text currencyText;

    public CanvasGroup winningPopup;
    public Text winningText;

    public Image[] payTableImages;
    private UserData userData;
    // Start is called before the first frame update
    void Start()
    {
        SetBetUI();
        userData = new UserData();
        userData.currency = 100;
    }

    public void IncreaseBet()
    {
        curBetIndex++;
        if (curBetIndex >= betValues.Length)
        {
            curBetIndex = betValues.Length - 1;
        }
        SetBetUI();
    }

    public void DecreaseBet()
    {
        curBetIndex--;
        if (curBetIndex < 0)
        {
            curBetIndex = 0;
        }
        SetBetUI();
    }

    private void SetBetUI()
    {
        betAmountText.text = betValues[curBetIndex].ToString();
        foreach (var img in payTableImages)
        {
            img.color = Color.white;
        }
        payTableImages[curBetIndex].color = Color.yellow;
    }
    public void SetCurrencyUI()
    {
        currencyText.text = "x " + userData.currency.ToString();
    }

    public void TakeCurrency()
    {
        userData.SubstractCurrency(betValues[curBetIndex]);
        SetCurrencyUI();
    }

    public void AddWinnings(int a)
    {
        int amt = payTables[curBetIndex,a];
        winningPopup.alpha =1;
        winningPopup.interactable = true;
        winningPopup.blocksRaycasts = true;
        winningText.text = "You won " +amt.ToString() + " currency!";
        userData.AddCurrency(amt);
    }

    public void CloseWinningPopup()
    {
        winningPopup.alpha =0;
        winningPopup.interactable = false;
        winningPopup.blocksRaycasts = false;
    }
}
