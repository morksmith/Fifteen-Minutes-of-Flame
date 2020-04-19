using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public enum ItemType { Grass, Leaves, Rocks, Sticks, Logs, Axes, Ropes, Torch}
    public ItemType Type;
    public Inventory Inventory;
    public Image SlotIcon;
    public Image SlotBackground;
    private Color startColour;
    public TextMeshProUGUI CountText;
    public float ItemCount;
    public GameObject ItemTransform;
    public bool Equipped;
    public int ItemSlot;
    public GameObject ItemPrefab;
    public bool Tool;
    public bool Torch;
    public PlayerControl Player;
    public GameManager Manager;
    public float Health;
    public TextMeshProUGUI HealthText;

    // Start is called before the first frame update
    void Start()
    {
        startColour = SlotIcon.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            ItemCount--;
            Health = 100;
        }
        if (Tool)
        {
            if (ItemCount > 0)
            {
                HealthText.text = Mathf.RoundToInt(Health) + "%";
            }
            else
            {
                HealthText.text = " ";
            }
        }
        
        CountText.text = ItemCount.ToString();
        if (Inventory.EquippedItem == ItemSlot)
        {
            Equipped = true;
        }
        else
        {
            Equipped = false;
        }

        if (ItemCount < 1)
        {
            SlotIcon.color = new Color(startColour.r, startColour.g, startColour.b, 0.5f);
        }
        else
        {
            SlotIcon.color = startColour;
        }

        if (Equipped)
        {
            if (Torch && ItemCount > 0 && !Manager.Paused)
            {
                Health -= 2 * Time.deltaTime;
            }
            SlotBackground.color = new Color(SlotBackground.color.r, SlotBackground.color.g, SlotBackground.color.b, 1);
            if(ItemCount > 0)
            {
                ItemTransform.SetActive(true);
            }
            else
            {
                ItemTransform.SetActive(false);
            }

        }
        else
        {
            SlotBackground.color = new Color(SlotBackground.color.r, SlotBackground.color.g, SlotBackground.color.b, 0.5f);
            ItemTransform.SetActive(false);
        }
        if (!Manager.Paused)
        {
            if (Input.GetMouseButtonDown(0) && Equipped && ItemCount > 0)
            {
                
                if (Tool)
                {
                    
                    if(Player.CursorImage.sprite == Player.HarvestCursor)
                    {
                        Player.AttackAnimation();
                        Health -= 2;
                    }
                }
                else
                {
                    if (ItemCount > 0 && Player.CursorImage.sprite == Player.NormalCursor)
                    {
                        var newItem = Instantiate(ItemPrefab, ItemTransform.transform.position, ItemTransform.transform.rotation);
                        newItem.GetComponent<Rigidbody>().AddForce(ItemTransform.transform.forward * 50 * Time.deltaTime, ForceMode.Impulse);
                        Player.AttackAnimation();
                        ItemCount--;
                    }
                    if(Player.CursorImage == Player.HarvestCursor)
                    {
                        Player.AttackAnimation();
                    }



                }
            }
        }
    }
}
