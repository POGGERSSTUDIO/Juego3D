using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text score;
    private int gameScore;
<<<<<<< Updated upstream
    
=======


>>>>>>> Stashed changes
    void Start()
    {

    }

    
    void Update()
    {

    }

    public void increaseScore(int objScore){

        gameScore += objScore;

        score.text = gameScore.ToString();

    }

}
