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
    public GameObject VolcanoParticles;
    public bool threshold1 = true;
    public bool threshold2 = true;

    public GameObject ExplodeParticles;


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

            if (t >= 0.75f && threshold2 == true)
            {
                var ps = VolcanoParticles.GetComponent<ParticleSystem>();
                var emission = ps.emission;
                float currentRate = emission.rateOverTime.constant;
                emission.rateOverTime = currentRate * 2f;
                threshold2 = false;
            }
            else if (t >= 0.50f && threshold1 == true)
            {
                var ps = VolcanoParticles.GetComponent<ParticleSystem>();
                var emission = ps.emission;
                float currentRate = emission.rateOverTime.constant;
                emission.rateOverTime = currentRate * 2f;
                threshold1 = false;
            }

            if (explodeTime <= 0)
            {
                VolcanoPayout.StopEverything();
                FindObjectOfType<CameraShake>().TriggerShake(0.5f, 0.2f);
                isExploding = false;
                VolcanoParticles.GetComponent<ParticleSystem>().Stop();
                ExplodeParticles.GetComponent<ParticleSystem>().Play();
                threshold2 = true;
                threshold1 = true;
            }
        }
        else
        {
            SpriteRenderer sr = Vignette.GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a -= Time.deltaTime;

            if (c.a <= 0)
            {
                c.a = 0;
            }

            sr.color = c;
        }
    }

    public void StartExploding()
    {
        isExploding = true;

        explodeTime = Random.Range(10, 31);
        maxExplodeTime = explodeTime;
    }
}
