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
    public float payoutMult;
    public TMP_Text multText;
    private float startingPMult;

    public float payoutAdditional;
    public float payoutAmount;
    public Slider payoutSlider;
    public TMP_Text payoutText;

    public GameObject VolcanoParticles;


    // Start is called before the first frame update
    void Start()
    {
        startingPMult = payoutMult;
        payoutIncrementCurrent = payoutIncrementInitial;

        payoutText.text = "Payout: P" + payoutAmount.ToString("F2");
        multText.text = "Multiplier: " + payoutMult.ToString("F1") + "x";
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
            VolcanoParticles.SetActive(true);
            VolcanoParticles.GetComponent<ParticleSystem>().Play();
            lockPayout = false; // start payout counter
        }
    }

    public void CashOut()
    {
        lockPayout = true; // stop payout counter

        BetSlider.interactable = true;
        BetButton.interactable = true;
        CashOutButton.interactable = false;
        VolcanoParticles.SetActive(false);

        var ps = VolcanoParticles.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        float currentRate = emission.rateOverTime.constant;
        emission.rateOverTime = currentRate / 4f;

        walletAmount += payoutAmount;
        walletText.text = "Wallet: P" + walletAmount.ToString();
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
        payoutText.text = "Payout: P" + payoutAmount.ToString("F2");

        var ps = VolcanoParticles.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        float currentRate = emission.rateOverTime.constant;
        emission.rateOverTime = currentRate / 4f;
    }
}
