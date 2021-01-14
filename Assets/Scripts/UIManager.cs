using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text pointText; 
    public Text timerText;
    public int nowPoint;
    public float timerLeftTimer;

    public int TotalTimerTick;
    public int nowTimerTick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator StartTimer()
    {
        while(true)
        {
            while(TotalTimerTick>nowTimerTick)
            {
                timerText.text = "LEFT TIMER = " + timerLeftTimer;
                yield return new WaitForSeconds(0.01f);
                timerLeftTimer-=0.01f;
                if (timerLeftTimer < 0)
                {
                   // SceneManager.LoadScene("SampleScene");
                }
            }
            
        }

    }
    public void AddPoint(int num)
    {
        nowPoint += num;
        pointText.text = "POINT = "+nowPoint;
    }
}
