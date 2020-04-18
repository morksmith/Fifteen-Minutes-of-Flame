using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int EquippedItem;
    public InventorySlot[] Slots;
    public float GrassCount;
    public float LeavesCount;
    public float StickCount;
    public float RockCount;
    public float LogCount;
    public float AxeCount;
    public float PickCount;
    public float TorchCount;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            EquippedItem = 4;
        }
    }

    
    public void UpdateUI()
    {

    }
}
