using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameManager Manager;
    public float FireHealth;

    [Header("Light Settings")]
    public Light PointLight;
    public float FlickerTime;
    public float FlickerVariance;

    private float flickerTimer;
    private Vector3 originalPos;

    [Header("Particle Settings")]
    public ParticleSystem FireParticles;
    public ParticleSystem SmokeParticles;
    public ParticleSystem SparkParticles;
    private ParticleSystem.EmissionModule fireModule;
    private ParticleSystem.MainModule fireMain;
    private ParticleSystem.EmissionModule smokeModule;
    private ParticleSystem.MainModule smokeMain;
    private ParticleSystem.EmissionModule sparkModule;
    


    void Start()
    {
        flickerTimer = Time.time + FlickerTime;
        originalPos = PointLight.transform.localPosition;
        fireModule = FireParticles.emission;
        fireMain = FireParticles.main;
        smokeModule = SmokeParticles.emission;
        smokeMain = SmokeParticles.main;
        sparkModule = SparkParticles.emission;
    }

    void Update()
    {
        if(Time.time > flickerTimer)
        {
            var rand = Random.Range(-FlickerVariance, FlickerVariance);
            PointLight.range = (FireHealth + rand) / 10;
            PointLight.intensity = (FireHealth + rand) / 100;
            PointLight.transform.position = new Vector3(originalPos.x + rand / 10, originalPos.y + rand / 10, originalPos.z + rand / 10);
            flickerTimer = Time.time + FlickerTime;
        }
        FireHealth = Manager.FireHealth;
        fireModule.rateOverTime = FireHealth / 20;
        fireMain.startSize = FireHealth / 200;
        smokeModule.rateOverTime = FireHealth / 20;
        smokeMain.startSize = FireHealth / 100;
        sparkModule.rateOverTime = FireHealth / 20;
        

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Item")
        {
            FireHealth += other.transform.GetComponent<Item>().Fuel;
            FireHealth = Mathf.Clamp(FireHealth, 0, Manager.MaxFireHealth);
            Manager.FireHealth = FireHealth;
            Destroy(other.gameObject);
            
        }
    }
}
