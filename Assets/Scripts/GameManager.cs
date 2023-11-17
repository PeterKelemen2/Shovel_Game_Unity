using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    private float timeScale = 4.0f;
    private int targetFPS = 60;
    private string saveFilePath;

    void Start()
    {
        Application.targetFrameRate = targetFPS;
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveGameData.json");

        SaveGame saveGame = new SaveGame();
        saveGame.score = 100;
        savePlayerData(saveGame);

        SaveGame loadedData = loadPlayerData();
        if (loadedData != null)
        {
            Debug.Log("Loaded score: " + saveGame.score);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Toggle between slowing and normal time by changing timeScale.
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = timeScale; // Slows down time.
            }
            else if(Time.timeScale == timeScale)
            {
                Time.timeScale = 1.0f; // Restores normal time.
            }
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void savePlayerData(SaveGame saveGame)
    {
        string jsonData = JsonUtility.ToJson(saveGame);
        File.WriteAllText(saveFilePath, jsonData);
        Debug.Log("Save Game Data saved");
    }

    public SaveGame loadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string JsonData = File.ReadAllText(saveFilePath);
            SaveGame loadedSaveData = JsonUtility.FromJson<SaveGame>(JsonData);
            Debug.Log("Save Game Data Loaded");
            
            return loadedSaveData;
        }
        else
        {
            Debug.LogWarning("No saved data found");
            return null;
        }
    }
}
