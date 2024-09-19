using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public ItemDropController dropController;

    public ScoreManager scoreManager;
    public LeaderboardManager leaderboardManager;
    PlayerData playerData;

    bool gameIsOver = false;

    private void Start()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        gameIsOver = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            var item = other.GetComponent<ItemManager>();
            if (item.isHit == true)
            {

                if(gameIsOver == false)
                {
                    StartCoroutine(leaderboardManager.SubmitScore(playerData.playerName, scoreManager.Score));
                    gameIsOver = true;
                }
                GameOverUI.SetActive(true);
                this.enabled = false;
                dropController.enabled = false;
                Time.timeScale = 0;
                leaderboardManager.FetchLeaderboardOnce(0);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
