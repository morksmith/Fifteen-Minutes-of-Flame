using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public enum ItemType { Grass, Leaves, Rocks, Sticks, Logs, Axes, Picks, Torch}
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
    public PlayerControl Player;
    public GameManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        startColour = SlotIcon.color;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (Input.GetMouseButtonDown(0) && Equipped)
            {
                
                if (Tool)
                {
                    Player.AttackAnimation();
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
