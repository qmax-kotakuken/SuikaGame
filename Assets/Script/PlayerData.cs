using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public string playerName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}
