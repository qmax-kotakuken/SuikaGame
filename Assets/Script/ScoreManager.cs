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
        //�O�̃X�R�A�ƈ���Ă���
        if (Score != oldScore)
        {
            //�X�R�A����
            oldScore = Score;
            //�X�R�A��\��
            ScoreText.text = Score.ToString();


            
        }

    }
}
