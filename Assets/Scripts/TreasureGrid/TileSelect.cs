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

    // Start is called before the first frame update
    void Start()
    {
        tilePayout = GameObject.Find("TileController").GetComponent<TilePayout>();
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
        if (tileType == 1) // treasure
        {
            tilePayout.AddPayout();
        }
        if (tileType == 2) // hole
        {
            tilePayout.Bust();
        }
    }
}
