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
    private float previousPayout;
    public TMP_Text cutText;

    public int wireCount = 1;
    public float payoutMult = 0.25f;
    private readonly float[] payoutMultipliers = { 1.2f, 1.5f, 2.0f, 2.75f, 4.0f };
    public float payoutBase;

    public WireRandomiser wireRandomiser;
    public Button resetButton;
    public GameObject vignette;

    public Animator SafeAnimator;
    public GameObject Locks;
    public TMP_Text safeText;
    public TMP_Text safePayoutText;
    public TMP_Text safe5wireText;
    public TMP_Text safeBonusText;
    public bool applyBonus = false;


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
        safeText.text = "P" + payoutAmount.ToString("F0") + "!";
        safeBonusText.text = "+P" + betAmount.ToString("F0") + "!";
    }
    public void AddPayout()
    {
        if (wireCount <= payoutMultipliers.Length)
        {
            //previousPayout = payoutAmount;
            payoutAmount = betAmount * payoutMultipliers[wireCount - 1];

            payoutAmount -= betAmount;

            float payoutDifference = payoutAmount - previousPayout;
            cutText.text = "+P" + payoutDifference.ToString("F0") + "!";

            if (wireCount == 5)
            {
                applyBonus = true;
                payoutAmount += betAmount;
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

            //payoutBase = betAmount;
            previousPayout = 0;
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
        //payoutAmount = 0;

        CashOutButton.interactable = false;
        resetButton.interactable = true;

        SafeAnimator.Play("SafeOpen");
        Locks = GameObject.Find("Locks");
        Locks.SetActive(false);
        StartCoroutine(SafeTextDelay());
    }

    public void ResetWires()
    {
        wireCount = 1;
        payoutAmount = 0;
        BetSlider.interactable = true;
        BetButton.interactable = true;
        CashOutButton.interactable = false;
        resetButton.interactable = false;
        betPlaced = false;
        vignette.SetActive(false);
        applyBonus = false;
        wireRandomiser.ResetWires();

        SafeAnimator.Play("SafeIdle");
        safeText.gameObject.SetActive(false);
        safePayoutText.gameObject.SetActive(false);
        safeBonusText.gameObject.SetActive(false);
        safe5wireText.gameObject.SetActive(false);
    }

    public IEnumerator SafeTextDelay()
    {
        yield return new WaitForSeconds(1.5f);
        safeText.gameObject.SetActive(true);
        safePayoutText.gameObject.SetActive(true);
        if (applyBonus == true)
        {
            safeBonusText.gameObject.SetActive(true);
            safe5wireText.gameObject.SetActive(true);
        }
    }
}
