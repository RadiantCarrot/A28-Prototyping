using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VaultPayout : MonoBehaviour
{
    public float walletAmount;
    public TMP_Text walletText;

    public Slider BetSlider;
    public Button BetButton;
    private float betAmount;
    public TMP_Text betText;
    public bool betPlaced = false;

    public Button CashOutButton;
    public bool lockPayout = true;

    public float payoutAmount;
    public TMP_Text payoutText;

    public int wireCount = 1;
    public float payoutMult = 0.25f;
    private readonly float[] payoutMultipliers = { 1.2f, 1.5f, 2.0f, 2.75f, 4.0f };
    public float payoutBase;

    public WireRandomiser wireRandomiser;
    public Button resetButton;
    public GameObject vignette;


    // Start is called before the first frame update
    void Start()
    {
        resetButton.interactable = false;
        CashOutButton.interactable = false;
        payoutAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        betAmount = (int)BetSlider.value;
        betText.text = "Bet Amount: P" + betAmount.ToString("F0");
        payoutText.text = "Payout: P" + payoutAmount.ToString("F0");
    }
    public void AddPayout()
    {
        if (wireCount <= payoutMultipliers.Length)
        {
            payoutAmount = betAmount * payoutMultipliers[wireCount - 1];

            if (wireCount == 5)
            {
                CashOut();
                betPlaced = false;
            }
            wireCount++;
        }
    }

    public void PlaceBet()
    {
        if (walletAmount >= betAmount)
        {
            BetSlider.interactable = false;
            BetButton.interactable = false;
            CashOutButton.interactable = true;
            resetButton.interactable = false;
            betPlaced = true;

            walletAmount -= betAmount;
            walletText.text = "Wallet: P" + walletAmount.ToString();

            payoutBase = betAmount;
        }
    }

    public void Bust()
    {
        payoutAmount = 0;
        wireCount = 1;
        payoutBase = 0;

        CashOutButton.interactable = false;
        resetButton.interactable = true;
        vignette.SetActive(true);
    }

    public void CashOut()
    {

        walletAmount += payoutAmount;
        walletText.text = "Wallet: P" + walletAmount.ToString();
        payoutAmount = 0;

        CashOutButton.interactable = false;
        resetButton.interactable = true;
    }

    public void ResetWires()
    {
        payoutAmount = 0;
        BetSlider.interactable = true;
        BetButton.interactable = true;
        CashOutButton.interactable = false;
        resetButton.interactable = false;
        betPlaced = false;
        vignette.SetActive(false);
        wireRandomiser.ResetWires();
    }
}
