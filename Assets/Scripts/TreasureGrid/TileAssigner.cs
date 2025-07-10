using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TileAssigner : MonoBehaviour
{
    public GameObject[] Tiles;
    public int tileHoleTotal;
    public int tileHoleCount;
    public GameObject Hole;
    public int tileTreasureTotal;
    public int tileTreasureCount;
    public GameObject TreasureBig;
    public GameObject TreasureSmall;

    public TMP_Text legendText;
    public int bigTreasureCount;
    public int smallTreasureCount;
    public int holeCount;


    // Start is called before the first frame update
    void Start()
    {
        Tiles = GameObject.FindGameObjectsWithTag("DirtSquare");
        //holeCount = tileHoleTotal;
        //bigTreasureCount = tileTreasureTotal;
        //smallTreasureCount = Tiles.Length - tileHoleTotal - tileTreasureTotal;

        ResetTiles();
    }

    // Update is called once per frame
    void Update()
    {
        legendText.text = "Holes["+holeCount+"]  |  Big Gems["+bigTreasureCount+ "]  |  Small Gems["+smallTreasureCount+"]";
    }

    public void ResetTiles()
    {
        foreach (GameObject tile in Tiles)
        {
            Destroy(tile.GetComponent<TileSelect>().objectDug);
            tile.GetComponent<TileSelect>().tileType = 0; // Reset tile type to empty
            tile.GetComponent<TileSelect>().digResult = null; // Reset dig result
            tile.SetActive(true); // Ensure the tile is active
        }

        tileHoleCount = tileHoleTotal;
        tileTreasureCount = tileTreasureTotal;

        for (int i = tileHoleCount; i > 0; i--)
        {
            AssignTileHoles();
        }
        for (int i = tileTreasureCount; i > 0; i--)
        {
            AssignTileTreasures();
        }
        AssignTileEmpties();

        holeCount = tileHoleTotal;
        bigTreasureCount = tileTreasureTotal;
        smallTreasureCount = Tiles.Length - tileHoleTotal - tileTreasureTotal;
    }

    public void AssignTileHoles()
    {
        int holeIndex = Random.Range(0, Tiles.Length);
        if (Tiles[holeIndex].GetComponent<TileSelect>().tileType == 0)
        {
            Tiles[holeIndex].GetComponent<TileSelect>().tileType = 2;
            Tiles[holeIndex].GetComponent<TileSelect>().digResult = Hole;
            tileHoleCount--;
            holeCount+=1;
        }
        else
        {
            AssignTileHoles();
        }
    }

    public void AssignTileTreasures()
    {
        int holeIndex = Random.Range(0, Tiles.Length);
        if (Tiles[holeIndex].GetComponent<TileSelect>().tileType == 0)
        {
            Tiles[holeIndex].GetComponent<TileSelect>().tileType = 1;
            Tiles[holeIndex].GetComponent<TileSelect>().digResult = TreasureBig;
            tileTreasureCount--;
            bigTreasureCount += 1;
        }
        else
        {
            AssignTileTreasures();
        }
    }

    public void AssignTileEmpties()
    {
        foreach (GameObject tile in Tiles)
        {
            if (tile.GetComponent<TileSelect>().tileType == 0)
            {
                tile.GetComponent<TileSelect>().digResult = TreasureSmall;
                smallTreasureCount += 1;
            }
        }
    }
}
