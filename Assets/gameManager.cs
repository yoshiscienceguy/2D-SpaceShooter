using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour
{
    public int killCount;
    public Text killcountUI;

    public float GameTime = 10;
    public Text gameTimeUI;
    private float currentGameTime;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        killcountUI.text = killCount.ToString();
        gameTimeUI.text = Mathf.Round(currentGameTime).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        else if (currentGameTime < GameTime)
        {
            currentGameTime += Time.deltaTime;
            float timeInSec = Mathf.Round(currentGameTime);
            string sec = ((int)timeInSec % 60).ToString();
            string min = ((int)timeInSec / 60).ToString();
            if (int.Parse(min) < 10) {
                min = "0" + min;
            }
            if (int.Parse(sec) < 10)
            {
                sec = "0" + sec;
            }
            gameTimeUI.text = min.ToString() + " : " + sec.ToString();
        }
        
        else {
            //win
        }

    }
    public void addKill() {
        killCount += 1;
        killcountUI.text =killCount.ToString();
    }
}
