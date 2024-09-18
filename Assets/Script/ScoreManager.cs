using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score;
    int oldScore = 0;

    public Text ScoreText;

    PlayerData playerData;

    private void Start()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        Debug.Log(playerData.playerName);
    }

    // Update is called once per frame
    void Update()
    {
        //前のスコアと違ってたら
        if (Score != oldScore)
        {
            //スコアを代入
            oldScore = Score;
            //スコアを表示
            ScoreText.text = Score.ToString();


            
        }

    }
}
