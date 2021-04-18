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
    public VideoPlayer vp;


    void Start()
    {

    }

    
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0){
            if(Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Time.timeSinceLevelLoad >= vp.length)
            {
                SceneManager.LoadScene(2);

            }
        }
    }

    public void increaseScore(int objScore){
        gameScore += objScore;
        score.text = "Score: " + gameScore.ToString();
    }
}
