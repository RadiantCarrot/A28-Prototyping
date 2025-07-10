using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    public int tileType; // 0 = empty, 1 = treasure, 2 = hole
    public GameObject digParticles;
    public GameObject digResult;
    public GameObject objectDug;

    public TilePayout tilePayout;
    public TileAssigner tileAssigner;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (tilePayout.betPlaced == true && tilePayout.blockDigging == false)
        {
            Instantiate(digParticles, transform.position, Quaternion.identity);
            objectDug = Instantiate(digResult, transform.position, Quaternion.identity);
            UncoverTile();
            gameObject.SetActive(false);
        }
    }

    public void UncoverTile()
    {
        if (tileType == 0) // treasure small
        {
            tilePayout.AddPayoutSmall();
            tileAssigner.smallTreasureCount--;
        }
        if (tileType == 1) // treasure big
        {
            tilePayout.AddPayoutBig();
            tileAssigner.bigTreasureCount--;
        }
        if (tileType == 2) // hole
        {
            tilePayout.Bust();
        }
    }
}
