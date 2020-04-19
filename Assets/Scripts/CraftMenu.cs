using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftMenu : MonoBehaviour
{
    public bool Open;
    public RectTransform Rect;
    public int SelectedItem;
    public CraftItem[] CraftableItems;
    public Button CraftButton;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CraftableItems[SelectedItem].Craftable)
        {
            CraftButton.interactable = true;
        }
        else
        {
            CraftButton.interactable = false;
        }
        if (Open)
        {
            Rect.anchoredPosition = Vector3.Lerp(Rect.anchoredPosition, Vector3.zero, 4 * Time.deltaTime);
        }
        else
        {
            Rect.anchoredPosition = Vector3.Lerp(Rect.anchoredPosition, new Vector3(0, -1500, 0), 4 * Time.deltaTime);
        }
    }

    public void CloseMenu()
    {
        Open = false;
    }

    public void OpenMenu()
    {
        Open = true;
        for (var i = 0; i < CraftableItems.Length; i++)
        {
            CraftableItems[i].CheckItemRequirements();
        }
    }

    public void CraftItem()
    {
        CraftableItems[SelectedItem].CreateItem();
    }
    public void SelectItem(int selectNumber)
    {
        SelectedItem = selectNumber;
        for (var i = 0; i < CraftableItems.Length; i++)
        {
            if(i == selectNumber)
            {
                CraftableItems[i].Selected = true;
                
            }
            else
            {
                CraftableItems[i].Selected = false;

            }
            
        }
    }
}
