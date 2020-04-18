using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Time")]
    public float GameLength = 900;
    public float TimeLeft = 900;


    [Header("Fire Management")]
    public float MaxFireHealth = 100;
    public float FireHealth = 100;


    private float timer;

    void Start()
    {
        timer = Time.time + 1;
        TimeLeft = GameLength;
        FireHealth = MaxFireHealth;
    }

    void Update()
    {
        TimeLeft = GameLength - Time.time;
        if(Time.time > timer)
        {
            if(FireHealth > 1)
            {
                FireHealth--;
            }
            else
            {
                FireHealth = 0;
            }
            timer = Time.time + 1;
        }
    }

    public void GameOver()
    {

    }
}
