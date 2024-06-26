using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Color col = sr.color;
        col.a = 0.75f;
        sr.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
