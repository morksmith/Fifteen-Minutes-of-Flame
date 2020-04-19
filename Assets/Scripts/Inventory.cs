using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int EquippedItem;
    public InventorySlot[] Slots;
    public PlayerControl Player;
    public float GrassCount;
    public float LeavesCount;
    public float StickCount;
    public float RockCount;
    public float LogCount;
    public float AxeCount;
    public float RopeCount;
    public float TorchCount;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(EquippedItem == 7)
            {
                EquippedItem = 0;
            }
            else
            {
                EquippedItem++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (EquippedItem == 0)
            {
                EquippedItem = 7;
            }
            else
            {
                EquippedItem--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquippedItem = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquippedItem = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquippedItem = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquippedItem = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquippedItem = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EquippedItem = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EquippedItem = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EquippedItem = 7;
        }

        if(EquippedItem == 6 && Slots[6].ItemCount > 0)
        {

            Player.AxeEquipped = true;
        }
        else
        {
            Player.AxeEquipped = false;
        }
        if (EquippedItem == 7 && Slots[7].ItemCount > 0)
        {
            Player.TorchEquipped = true;
        }
        else
        {
            Player.TorchEquipped = false;
        }
    }

    
    public void UpdateUI()
    {

    }
}
