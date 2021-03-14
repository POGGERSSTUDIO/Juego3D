using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{

    public Text score;
    private int gameScore;
    public Text h1;
    private int h1cd;
    public Text h2;
    private int h2cd;
    public Text h3;
    private int h3cd;
    public VideoPlayer vp;

    void Start()
    {
        score.text = "0";
        h1cd = 0;
        h1.text = h1cd.ToString();
        h2cd = 0;
        h2.text = h2cd.ToString();
        h3cd = 0;
        h3.text = h3cd.ToString();
        
    }

   

    void Update()
    {
        if(Time.time > h1cd){
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                increaseScore(-20);
                h1cd += (int) Time.time + 5;
            }
        }else{
            
            h1.text = ((h1cd - (int) Time.time) * Time.deltaTime).ToString();
            
        }

        if(Time.time > h2cd){
            if(Input.GetKeyDown(KeyCode.Alpha2)){

                increaseScore(-40);
                h2cd += (int) Time.time + 10;

            }
        }else{

            h2.text = (h2cd - (int) Time.time).ToString();
            
        }

        if(Time.time > h3cd){
            if(Input.GetKeyDown(KeyCode.Alpha3)){

                increaseScore(-10);
                h3cd += (int) Time.time + 3;

            }
        }else{

            h3.text = (h3cd - (int) Time.time).ToString();

        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("BOBO");
                SceneManager.LoadScene(1);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(Time.timeSinceLevelLoad >= vp.length)
            {
                SceneManager.LoadScene(0);
             
            }


        }
    }

    public void increaseScore(int objScore){

        gameScore += objScore;

        score.text = gameScore.ToString();

    }

   
   
}
