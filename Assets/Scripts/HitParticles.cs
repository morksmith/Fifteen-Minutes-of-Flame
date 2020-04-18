using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticles : MonoBehaviour
{
    public float LifeTime = 2;
    public ParticleSystem ParticleSystem;
    public Color StartColour;


    private ParticleSystem.MainModule partMain;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        partMain = ParticleSystem.main;
        partMain.startColor = StartColour;
        timer = Time.time + LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            Destroy(gameObject);
        }
    }
}
