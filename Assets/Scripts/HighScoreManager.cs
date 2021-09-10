using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
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
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();

        if (savedName == "")
            HighScoreText.text += " none";
        else
            HighScoreText.text += $" {savedName} : {bestScore}";
    }


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
            savedName = playerName,
            bestScore = bestScore
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);

    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
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
