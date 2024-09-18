using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public InputField inputField;
    public Button submitButton;

    void Start()
    {
        // Add a listener to the button to handle click events
        submitButton.onClick.AddListener(OnSubmit);

        // Optionally, you can handle text submission when Enter is pressed
        inputField.onEndEdit.AddListener(OnSubmitOnEnter);

        LoadPlayerName();
    }

    void OnSubmit()
    {
        // Get the text from the input field
        string userInput = inputField.text;

        if (!string.IsNullOrEmpty(userInput))
        {
            PlayerPrefs.SetString("userInput", userInput);
            PlayerPrefs.Save(); // Save the name persistently
        }

        // Handle the input (e.g., print it to the console)
        Debug.Log("User submitted: " + userInput);

        // Clear the input field if desired
        inputField.text = "";
    }

    void OnSubmitOnEnter(string input)
    {
        // Check if the Enter key was pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
           OnSubmit();
        }
    }

    // Call this to load the player's name at the start
    public void LoadPlayerName()
    {
        if (PlayerPrefs.HasKey("userInput"))
        {
            string savedName = PlayerPrefs.GetString("userInput");
        }
    }
}
