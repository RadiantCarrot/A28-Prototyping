using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoTimer : MonoBehaviour
{
    public bool isExploding = false;
    public float explodeTime;
    private float maxExplodeTime;

    public VolcanoPayout VolcanoPayout;

    public GameObject Vignette;


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
            explodeTime = Mathf.Max(explodeTime, 0); // prevent negative time

            // Normalize explodeTime to 0–1
            float t = 1f - (explodeTime / maxExplodeTime);
            t = Mathf.Clamp01(t); // just in case

            SpriteRenderer sr = Vignette.GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = t;
            sr.color = c;

            if (explodeTime <= 0)
            {
                VolcanoPayout.StopEverything();
                FindObjectOfType<CameraShake>().TriggerShake(0.5f, 0.2f);
                isExploding = false;
            }
        }
    }

    public void StartExploding()
    {
        isExploding = true;

        explodeTime = Random.Range(10, 31);
        maxExplodeTime = explodeTime;
    }
}
