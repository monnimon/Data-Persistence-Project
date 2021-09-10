using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
// namespace will only be included when compiling within Unity Editor
// # means instructions for the compiler (won't be compiled and executed)
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Text NameText;

    public void StartGame()
    {
        SceneManager.LoadScene(1);  
    }

    public void SetName()
    {
        // Save text from input field in HighScoreManager
        HighScoreManager.Instance.playerName = NameText.text;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }

}
