using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public PlayerControl Player;

    [Header("Game Settings")]
    public float GameLength = 900;
    public float TimeLeft = 900;
    public bool Paused = false;

    [Header("World Building")]
    public float WorldSize = 1000;
    public float ObjectLimit = 1000;
    public GameObject[] WorldObjects;



    [Header("Fire Management")]
    public float MaxFireHealth = 200;
    public float FireHealth = 200;
    public Slider FireSlider;
    public float MaxTemperature = 200;
    public float Temperature = 200;
    public Slider ColdSlider;
   

    private float timer;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        timer = Time.time + 1;
        TimeLeft = GameLength;
        FireHealth = MaxFireHealth;
        BuildWorld();
    }

    void Update()
    {
        ColdSlider.value = 1 - Temperature / MaxTemperature;
        FireSlider.value = FireHealth / MaxFireHealth;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!Paused)
            {
                PauseMenu();
            }
            else
            {
                ResumeGame();
            }
        }
        if(!Paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;

        }


        TimeLeft = GameLength - Time.time;
        if(Time.time > timer)
        {
            var playerDistance = Vector3.Distance(Player.transform.position, Vector3.zero);
            var tempDifference = -1 + (FireHealth / 100 - playerDistance / 10);
            if(FireHealth > 1)
            {
                FireHealth--;
                
            }
            else
            {
                FireHealth = 0;
                FireSlider.value = 0;
            }
            if (Temperature > 1)
            {
                if (!Player.TorchEquipped)
                {
                    Temperature += tempDifference;
                    Temperature = Mathf.Clamp(Temperature, 0, MaxTemperature);
                }
                else
                {
                    Temperature += tempDifference + 1;
                    Temperature = Mathf.Clamp(Temperature, 0, MaxTemperature);
                }
                

            }
            timer = Time.time + 1;
        }
    }

    public void GameOver()
    {

    }

    public void PauseMenu()
    {
        Paused = true;
    }
    public void ResumeGame()
    {
        Paused = false;
    }
    public void OpenCraftMenu()
    {
        Paused = true;
    }

    public void BuildWorld()
    {
        for(var i = 0; i <ObjectLimit; i++)
        {
            var objectPicker = Random.Range(0, WorldObjects.Length);
            var spawnPos = new Vector3(Random.Range(-WorldSize, WorldSize), 0, Random.Range(-WorldSize, WorldSize));
            if(Vector3.Distance(Vector3.zero, spawnPos) > 3)
            {
                var newObject = Instantiate(WorldObjects[objectPicker], spawnPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
            }
        }
    }
}
