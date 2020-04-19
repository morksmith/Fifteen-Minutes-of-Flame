using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Menu GameOverMenu;
    public Menu GameCompleteMenu;
    public PlayerControl Player;
    [Header("Game Settings")]
    public float GameLength = 900;
    public float TimeLeft = 900;
    public float ElapsedTime = 0;
    public bool Paused = false;
    public Light SunLight;
    public Image DayDial;
    public Image BlackScreen;

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
    private float index = 1;
    private Quaternion sunStartRotation;

    void Start()
    {
        Cursor.visible = false;
        
        timer = Time.time + 1;
        TimeLeft = GameLength;
        FireHealth = MaxFireHealth;
        BuildWorld();
        sunStartRotation = SunLight.transform.rotation;
        ElapsedTime = 0;
    }

    void Update()
    {
        index -= Time.deltaTime;
        if(BlackScreen.color.a > 0)
        {
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, index);
        }
        SunLight.intensity = Mathf.Lerp(0, 2, ElapsedTime / GameLength);
        SunLight.transform.rotation = Quaternion.Lerp(sunStartRotation, Quaternion.Euler(Vector3.zero), ElapsedTime / GameLength);
        RenderSettings.fogEndDistance = Mathf.Lerp(10, 100, ElapsedTime / GameLength);
        DayDial.transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -180), ElapsedTime / GameLength);
        
        if(!Paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ColdSlider.value = 1 - Temperature / MaxTemperature;
            FireSlider.value = FireHealth / MaxFireHealth;
            TimeLeft = GameLength - ElapsedTime;
            if (Time.time > timer)
            {
                ElapsedTime++;
                var playerDistance = Vector3.Distance(Player.transform.position, Vector3.zero);
                var tempDifference = -0.8f + (FireHealth / 150 - playerDistance / 10);
                if (FireHealth > 1)
                {
                    FireHealth--;

                }
                else
                {
                    FireHealth = 0;
                    FireSlider.value = 0;
                }
                if (Temperature > 0)
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

            if(Temperature <= 0)
            {
                GameOver();
            }
            if(TimeLeft <= 0)
            {
                GameComplete();
            }

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;

        }


        
    }

    public void GameOver()
    {
        Paused = true;
        GameOverMenu.OpenMenu();
    }
    public void GameComplete()
    {
        Paused = true;
        GameCompleteMenu.OpenMenu();
    }

    public void PauseMenu()
    {
        Paused = true;
    }
    public void ResumeGame()
    {
        Paused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
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
