using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Health = 1;
    public float ItemCount = 1;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Harvest(float dmg)
    {
        if(Health > 1)
        {
            Health--;
        }
        else
        {
            Health = 0;
            Death();
        }
    }
    public void Death()
    {
        var spawnCount = Random.Range(1, ItemCount);
        for(var i = 0; i < spawnCount; i++)
        {
            var newItem = Instantiate(ItemPrefab, transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0.5F + Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), transform.rotation);
        }
        Destroy(gameObject);
    }
}
