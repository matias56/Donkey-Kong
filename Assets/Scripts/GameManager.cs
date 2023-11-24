using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int level;
    public float lives;
    public float score;
    public int time;
    public float tF;
    public int initTime;
    public int curL;

    
    
    


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        
    }

    private void Update()
    {




        

       
        
        
           


        
        if(level != 0 && level != 5)
        {

            

            tF = Convert.ToInt32(time);

            tF -= 100f * Time.deltaTime;
            time = Mathf.RoundToInt(tF);



        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NewGame();
            }
        }



        if (time <= 0)
        {
            LevelFailed();
        }


        curL = level;
    }

    public void NewGame()
    {
        lives = 3;
        score = 0;

        LoadLevel(1);
    }

    private void LoadLevel(int index)
    {
        level = index;

        Camera camera = Camera.main;

        if(camera != null)
        {
            camera.cullingMask = 0;
        }

        Invoke(nameof(LoadScene), 1f);

        time = initTime;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(level);
        
    }

    public void LevelComplete()
    {
        score += time;

        int nextLevel = level + 1;
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextLevel);
        } else
        {
            LoadLevel(1);
        }
    }

   

    public void LevelFailed()
    {
        lives -= 1;

        if (lives <= 0)
        {
            NewGame();
        }
        else
        {
            LoadLevel(level);
        }
    }
}
