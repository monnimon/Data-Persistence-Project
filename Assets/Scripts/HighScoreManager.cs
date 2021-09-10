using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    // enables access to HighScoreManager object from any other script
    public static HighScoreManager Instance;

    public Text HighScoreText;

    public string playerName;
    public string savedName;
    public int bestScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // stores "this" in the current instance of HighScoreManager (HighScoreManager.Instance can now 
        // be called from any other script
        Instance = this;
        // the HighScoreManager GameObject attached to this script will not be destroyed when scene changes
        DontDestroyOnLoad(gameObject);
        // load saved high score when app starts
        LoadHighScore();

        // if a name has been saved, display the name and their high score
        if (savedName == "")
            HighScoreText.text += " none";
        else
            HighScoreText.text += $" {savedName} : {bestScore}";
    }


    // small class to contain the specific data we want to save
    [System.Serializable]
    class SaveData 
    {
        public string savedName;
        public int bestScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData
        {
            savedName = playerName, // update playerName to the last saved name
            bestScore = bestScore
        };

        // transform into JSON format
        string json = JsonUtility.ToJson(data);
        // write it to a file  (first parameter is path to the file, second is the text (string) to write)
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);

    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "savefile.json";

        if (File.Exists(path))
        {
            // read file content
            string json = File.ReadAllText(path);
            // transform data from JSON file back into a SaveData instance
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            // set values to the values saved in that SaveData
            savedName = data.savedName;
            bestScore = data.bestScore;
        }
    }


    // Method to delete saved high score after session (for testing)
    public void DeleteHighScore()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path)) 
        {
            File.Delete(Application.persistentDataPath + "savefile.json");
            Debug.Log("Highscore deleted");
        }
    }

}
