using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public InputField inputField;
    public Button submitButton;

    public NamingScene NamingScene;

    public PlayerData playerData;

    void Start()
    {
        // Add a listener to the button to handle click events
        submitButton.onClick.AddListener(OnSubmit);

        // Optionally, you can handle text submission when Enter is pressed
        inputField.onEndEdit.AddListener(OnSubmitOnEnter);
    }

    void OnSubmit()
    {
        // Get the text from the input field
        string PlayerName = inputField.text;

        if (!string.IsNullOrEmpty(PlayerName))
        {
            playerData.playerName = PlayerName;
        }

        // Handle the input (e.g., print it to the console)
        Debug.Log("User submitted: " + PlayerName);

        // Clear the input field if desired
        inputField.text = "";

        NamingScene.ToNextScene();
    }

    void OnSubmitOnEnter(string input)
    {
        // Check if the Enter key was pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
           OnSubmit();
        }
    }
}
