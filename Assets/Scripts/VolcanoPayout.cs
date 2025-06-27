using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VolcanoPayout : MonoBehaviour
{
    public float walletAmount;
    public TMP_Text walletText;

    public Slider BetSlider;
    public Button BetButton;
    private float betAmount;
    public TMP_Text betText;

    public Button CashOutButton;
    public bool lockPayout = true;

    public float payoutIncrementInitial = 0.5f; // timer
    private float payoutIncrementCurrent;

    private float payoutCounter = 1;
    public float payoutMult = 0.25f;
    private float startingPMult;

    public float payoutAdditional;
    public float payoutAmount;
    public Slider payoutSlider;
    public TMP_Text payoutText;

    // Start is called before the first frame update
    void Start()
    {
        startingPMult = payoutMult;
        payoutIncrementCurrent = payoutIncrementInitial;
    }

    // Update is called once per frame
    void Update()
    {
        betAmount = (int)BetSlider.value;
        betText.text = "Bet Amount: P" + betAmount.ToString("F0");

        if (lockPayout == false)
        {
            payoutIncrementCurrent -= Time.deltaTime;
            {
                if (payoutIncrementCurrent <= 0)
                {
                    payoutIncrementCurrent = payoutIncrementInitial; // reset increment timer
                    payoutCounter ++;
                    payoutAdditional = payoutCounter * payoutMult;

                    payoutSlider.value = payoutAdditional * 2;
                    payoutAmount += payoutAdditional;
                    payoutText.text = "Payout: P" + payoutAmount.ToString("F2");
                }
            }
        }
    }

    public void PlaceBet()
    {
        if (walletAmount >= betAmount)
        {
            BetSlider.interactable = false;
            BetButton.interactable = false;
            CashOutButton.interactable = true;

            payoutAmount = betAmount;
            walletAmount -= betAmount;
            walletText.text = "Wallet: P" + walletAmount.ToString();
            lockPayout = false; // start payout counter
        }
    }

    public void CashOut()
    {
        lockPayout = true; // stop payout counter

        BetSlider.interactable = true;
        BetButton.interactable = true;
        CashOutButton.interactable = false;

        walletAmount += payoutAmount;
    }

    public void StopEverything()
    {
        lockPayout = true;

        BetSlider.interactable = true;
        BetButton.interactable = true;
        CashOutButton.interactable = false;

        payoutCounter = 1;
        payoutMult = startingPMult;
        payoutSlider.value = 0;
        payoutAmount = 0;
    }
}
