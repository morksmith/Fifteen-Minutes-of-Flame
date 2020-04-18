using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform Player;
    public string ItemName;
    public bool InInventory = false;
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
        if (InInventory)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 10);
            transform.position = Vector3.Lerp(transform.position, Player.position + new Vector3(0, 1, 0), Time.deltaTime * 20);
            BoxCollider.enabled = false;
            rb.useGravity = false;
        }
        else
        {
            BoxCollider.enabled = true;
            rb.useGravity = true;

        }
    }

    
}
