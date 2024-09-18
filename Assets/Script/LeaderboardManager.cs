using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LeaderboardManager : MonoBehaviour
{
    string submitScoreURL = "https://118.27.11.211/2024kadai/student_02007/submit_score.php";
    string fetchLeaderboardURL = "https://118.27.11.211/2024kadai/student_02007/fetch_leaderboard.php";

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

    public IEnumerator FetchLeaderboard()
    {
        UnityWebRequest www = UnityWebRequest.Get(fetchLeaderboardURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Parse the leaderboard data
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }
}
