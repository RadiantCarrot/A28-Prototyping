using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedScript : MonoBehaviour
{
    public int seed;
    public bool useSeed;
    public TMP_Text seedText;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSeed()
    {
        if (useSeed == true)
        {
            Random.InitState(seed);
        }
        else
        {
            int randomSeed = Random.Range(0, 1000000);
            Random.InitState(randomSeed);

            seedText = GameObject.Find("SeedText").GetComponent<TextMeshProUGUI>();
            seedText.text = "Seed: " + randomSeed.ToString();
            Debug.Log(randomSeed);
        }
    }
}
