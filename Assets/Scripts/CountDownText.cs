using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownText : MonoBehaviour
{

    public TMPro.TMP_Text countDownText;
    
    void Start()
    {
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        countDownText.text = "5";
        yield return new WaitForSeconds(1f);
        countDownText.text = "4";
        yield return new WaitForSeconds(1f);
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
    }
}
