using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireRandomiser : MonoBehaviour
{
    public GameObject WireboxPrefab;
    public GameObject WireboxPresent;
    public GameObject[] Wires;

    // Start is called before the first frame update
    void Start()
    {
        ResetWires();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetWires()
    {
        if (WireboxPresent != null)
        {
            Destroy(WireboxPresent);
        }
        WireboxPresent = Instantiate(WireboxPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        Wires = GameObject.FindGameObjectsWithTag("Wire");

        RandomiseCorrectWire();
    }

    private void RandomiseCorrectWire()
    {
        int wrongWire = Random.Range(0, Wires.Length);

        for (int i = 0; i < Wires.Length; i++)
        {
            if (i == wrongWire)
            {
                Wires[i].GetComponent<WireCut>().correctWire = false;
            }
        }
    }
}
