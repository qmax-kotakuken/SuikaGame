using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScoreEntry
{
    public string player_name;
    public int score;
}

public class LeaderboardManager : MonoBehaviour
{
    string submitScoreURL = "http://118.27.11.211/2024kadai/student_02007/submit_score.php";
    string fetchLeaderboardURL = "http://118.27.11.211/2024kadai/student_02007/fetch_leaderboard.php";

    public Text[] leaderboardTexts; // UI Text elements to display top 3 scores

    public Text leaderboardTitle;

    int currentRankType = 0;

    private void Start()
    {
        FetchLeaderboardOnce(0);
    }

    public IEnumerator SubmitScore(string playerName, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_name", playerName);
        form.AddField("score", score);

        UnityWebRequest www = UnityWebRequest.Post(submitScoreURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score submitted!");
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }

    public void FetchLeaderboardOnce(int _rankTypeNum)
    {
        switch(_rankTypeNum)
        {
            case 0:
                StartCoroutine(FetchLeaderboard("Daily"));
                leaderboardTitle.text = "Daily";
                break;
            case 1:
            StartCoroutine(FetchLeaderboard("Weekly"));
                leaderboardTitle.text = "Weekly";
                break;
            case 2:
                StartCoroutine(FetchLeaderboard("All-Time"));
                leaderboardTitle.text = "All Time";
                break;
            default:
                break;
        }
        
    }

    public IEnumerator FetchLeaderboard(string _rankingType)
    {
        string url = fetchLeaderboardURL + "?type=" + _rankingType; // Append ranking type to URL
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            ScoreEntry[] scores = JsonHelper.FromJson<ScoreEntry>(json);

             // Display the top 3 scores in the UI
            for (int i = 0; i < scores.Length && i < leaderboardTexts.Length; i++)
            {
                leaderboardTexts[i].text =  "" + scores[i].score;
            }
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }

    public void NextLeaderboard()
    {
        if(currentRankType < 2)
        {
            currentRankType++;
        }
        else 
        {
            currentRankType = 0;
        }
        FetchLeaderboardOnce(currentRankType);
        Debug.Log(currentRankType);
    }
    public void PrevLeaderboard()
    {
        if (currentRankType > 0)
        {
            currentRankType--;
        }
        else
        {
            currentRankType = 2;
        }
        FetchLeaderboardOnce(currentRankType);
        Debug.Log(currentRankType);
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}


