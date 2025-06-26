using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoTimer : MonoBehaviour
{
    public bool isExploding = false;
    public float explodeTime;

    public VolcanoPayout VolcanoPayout;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isExploding == true)
        {
            explodeTime -= Time.deltaTime;
            if (explodeTime <= 0)
            {
                VolcanoPayout.StopEverything();
                isExploding = false;
            }
        }
    }

    public void StartExploding()
    {
        isExploding = true;

        explodeTime = Random.Range(10, 31);
    }
}
