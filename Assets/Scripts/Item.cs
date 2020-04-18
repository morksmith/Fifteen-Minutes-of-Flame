using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform Player;
    public string ItemName;
    public bool InInventory = false;
    public int Slot;
    public float Fuel;
    public Collider BoxCollider;

    private Rigidbody rb;
    private Vector3 startSize;


    void Start()
    {
        startSize = transform.localScale;
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-8, 8) * Time.deltaTime, 10 * Time.deltaTime, Random.Range(-8, 8) * Time.deltaTime), ForceMode.Impulse);

    }

    void Update()
    {
        
    }

   


}
