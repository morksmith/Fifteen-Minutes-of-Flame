using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool Open;
    public RectTransform Rect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        
    }
}

