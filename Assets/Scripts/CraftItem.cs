using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour

{
    public InventorySlot[] RequiredItems;
    public float[] CountRequirements;
    public float CompleteRequirements;
    public bool Selected;
    public bool Craftable;
    public Image BackgroundImage;
    public GameObject ItemPrefab;
    public Vector3 CraftPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Selected)
        {
            BackgroundImage.color = new Color(BackgroundImage.color.r, BackgroundImage.color.g, BackgroundImage.color.b, 1);
        }
        else
        {
            BackgroundImage.color = new Color(BackgroundImage.color.r, BackgroundImage.color.g, BackgroundImage.color.b, 0.1f);
        }
        
        if(CompleteRequirements == CountRequirements.Length)
        {
            Craftable = true;
        }
        else
        {
            Craftable = false;
        }

    }

    public void CreateItem()
    {
        for (var i = 0; i < RequiredItems.Length; i++)
        {
            RequiredItems[i].ItemCount -= CountRequirements[i];
        }
        Instantiate(ItemPrefab, CraftPosition, transform.rotation);
        CheckItemRequirements();
    }

    public void CheckItemRequirements()
    {
        CompleteRequirements = 0;
        for (var i = 0; i < RequiredItems.Length; i++)
        {
            if (RequiredItems[i].ItemCount >= CountRequirements[i])
            {
                CompleteRequirements++;
            }
        }
    }
}
