using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    public Transform PlayerHead;
    public float LookSpeed;
    public float MoveSpeed;
    public float MoveMod;
    public bool InvertY = false;

    [Header("Interaction")]
    public float Range = 1;
    public float Damage = 1;
    public Image CursorImage;
    public Sprite NormalCursor;
    public Sprite GrabCursor;
    public Sprite HarvestCursor;
    public Inventory Inventory;
    


    private Camera cam;
    private float vertLook;
    private float sideLook;
    private Vector3 moveVector;
    private float yMod;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CursorImage.sprite = NormalCursor;
        SetInvert();
        cam = PlayerHead.GetComponent<Camera>();
    }

    void Update()
    {
        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        transform.Translate(moveVector * MoveSpeed * Time.deltaTime);

        vertLook += Input.GetAxis("Mouse Y") * LookSpeed * Time.deltaTime * yMod;
        vertLook = Mathf.Clamp(vertLook, -90, 90);
        sideLook += Input.GetAxis("Mouse X") * LookSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, sideLook, 0);
        PlayerHead.localEulerAngles = new Vector3(vertLook, 0, 0);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < Range)
            {
                if (hit.transform.tag == "Resource")
                {
                    CursorImage.sprite = HarvestCursor;
                    if (Input.GetMouseButtonDown(0))
                    {
                        var hitResource = hit.transform.GetComponent<Resource>();
                        hitResource.Harvest(Damage);
                    }
                }
                else if (hit.transform.tag == "Item")
                {
                    CursorImage.sprite = GrabCursor;
                    if (Input.GetMouseButtonDown(0))
                    {
                        var hitItem = hit.transform.GetComponent<Item>();
                        if (!hitItem.InInventory)
                        {
                            hitItem.InInventory = true;
                            Inventory.Items.Add(hit.transform.gameObject);
                        }
                        
                    }
                }
                else
                {
                    CursorImage.sprite = NormalCursor;
                }
            }
            else
            {
                CursorImage.sprite = NormalCursor;
            }
        }
    }

    public void SetInvert()
    {
        if (InvertY)
        {
            yMod = 1;
        }
        else
        {
            yMod = -1;
        }
    }

    
}
