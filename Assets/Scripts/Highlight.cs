using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Renderer Render;
    public Color HighlightColor = Color.grey;

    
    // Start is called before the first frame update
    void Start()
    {
        Render.material.EnableKeyword("_EMISSION");
       
        
    }

    // Update is called once per frame
    void Update()
    {
        var index = Time.deltaTime;

        var emissionColour = Color.Lerp(Color.black, HighlightColor, Mathf.Sin(Time.time * 6));

        Render.material.SetColor("_EmissionColor", emissionColour);
    }

    
}
