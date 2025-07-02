using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WireCut : MonoBehaviour
{
    public GameObject wire1;
    public GameObject wire2;
    public GameObject lockRed;
    public GameObject lockGreen;
    public GameObject vaultKnob;
    public bool isUnlocking = false;
    public bool canCut = true;

    public VaultPayout vaultPayout;
    public bool correctWire = true;
    public GameObject cutText;

    // Start is called before the first frame update
    void Start()
    {
        lockRed.SetActive(true);
        lockGreen.SetActive(false);

        vaultKnob = GameObject.Find("VaultKnob");
        vaultPayout = GameObject.Find("VaultController").GetComponent<VaultPayout>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnlocking == true)
        {
            vaultKnob.transform.Rotate(0, 0, 1.5f);
        }
    }

    private void OnMouseDown()
    {
        if (vaultPayout.betPlaced == true && canCut == true)
        {
            wire1.transform.localScale = new Vector3(wire1.transform.localScale.x, wire1.transform.localScale.y / 2.5f, wire1.transform.localScale.z);
            wire1.transform.localPosition = new Vector3(wire1.transform.localPosition.x, wire1.transform.localPosition.y + 0.5f, wire1.transform.localPosition.z);
            wire2.transform.localScale = new Vector3(wire2.transform.localScale.x, wire2.transform.localScale.y / 2.5f, wire2.transform.localScale.z);
            wire2.transform.localPosition = new Vector3(wire2.transform.localPosition.x, wire2.transform.localPosition.y -0.5f, wire2.transform.localPosition.z);

            StartCoroutine(Unlocking());
            isUnlocking = true;
            canCut = false;
        }
    }

    private IEnumerator Unlocking()
    {
        isUnlocking = true;
        yield return new WaitForSeconds(1f);

        isUnlocking = false;
        ConfirmAnswer();
    }

    private void ConfirmAnswer()
    {
        if (correctWire == true)
        {
            lockRed.SetActive(false);
            lockGreen.SetActive(true);
            vaultPayout.cutText = cutText.GetComponent<TMP_Text>();
            vaultPayout.AddPayout();
            StartCoroutine(MoveCutText());
        }
        else
        {
            vaultPayout.Bust();
        }
    }

    public IEnumerator MoveCutText()
    {
        cutText.SetActive(true);
        cutText.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(1f);
        cutText.SetActive(false);
    }
}
