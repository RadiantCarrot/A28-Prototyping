using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TilePayout : MonoBehaviour
{
    public float walletAmount;
    public TMP_Text walletText;

    public Slider BetSlider;
    public Button BetButton;
    private float betAmount;
    public TMP_Text betText;
    public bool betPlaced = false;

    public Button CashOutButton;

    public float payoutAmount;
    public TMP_Text payoutText;
    public float valueToAdd;

    public Button resetButton;
    public GameObject vignette;

    public TileAssigner tileAssigner;
    public bool blockDigging = false;

    public GameObject[] Treasures;
    public GameObject TreasureWaypoint;
    public bool moveTreasure = false;


    // Start is called before the first frame update
    void Start()
    {
        resetButton.interactable = false;
        CashOutButton.interactable = false;
        payoutAmount = 0;

        valueToAdd = betAmount / 10;
    }

    // Update is called once per frame
    void Update()
    {
        betAmount = (int)BetSlider.value;
        betText.text = "Bet Amount: P" + betAmount.ToString("F0");
        payoutText.text = "Payout: P" + payoutAmount.ToString("F0");

        if (moveTreasure == true)
        {
            foreach (GameObject treasure in Treasures)
            {
                treasure.transform.position = Vector3.MoveTowards(treasure.transform.position, TreasureWaypoint.transform.position, 0.5f);

                Vector3 currentScale = treasure.transform.localScale;
                float scaleDecrease = 2f * Time.deltaTime;
                Vector3 newScale = currentScale - new Vector3(scaleDecrease, scaleDecrease, 0);

                newScale.x = Mathf.Max(0.1f, newScale.x);
                newScale.y = Mathf.Max(0.1f, newScale.y);

                treasure.transform.localScale = newScale;
            }
        }
    }
    public void AddPayoutSmall()
    {
        payoutAmount += valueToAdd / 3;
    }
    public void AddPayoutBig()
    {
        payoutAmount += valueToAdd;
    }

    public void PlaceBet()
    {
        if (walletAmount >= betAmount)
        {
            betPlaced = true;
            BetSlider.interactable = false;
            BetButton.interactable = false;
            CashOutButton.interactable = true;
            resetButton.interactable = false;

            valueToAdd = betAmount * 0.33f;

            walletAmount -= betAmount;
            walletText.text = "Wallet: P" + walletAmount.ToString();
        }
    }

    public void Bust()
    {
        payoutAmount = 0;
        blockDigging = true;

        CashOutButton.interactable = false;
        resetButton.interactable = true;
        vignette.SetActive(true);
    }

    public void CashOut()
    {
        walletAmount += payoutAmount;
        walletText.text = "Wallet: P" + walletAmount.ToString();

        CashOutButton.interactable = false;
        resetButton.interactable = true;

        Treasures = GameObject.FindGameObjectsWithTag("Treasure");
        moveTreasure = true;
    }

    public void ResetGrid()
    {
        resetButton.interactable = false;
        BetButton.interactable = true;
        BetSlider.interactable = true;
        vignette.SetActive(false);
        blockDigging = false;

        payoutAmount = 0;
        betAmount = 0;
        betPlaced = false;
        moveTreasure = false;

        tileAssigner.ResetTiles();
    }
}
