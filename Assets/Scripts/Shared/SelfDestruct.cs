using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public int time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf(time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroySelf (int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
