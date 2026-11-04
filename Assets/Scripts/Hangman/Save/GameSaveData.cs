using System.IO;
using UnityEngine;

public class GameSaveData : MonoBehaviour
{
    [SerializeField] HangmanSaveData _saveData;
    public Hangman.GameManager locationToSendAndRecieveData;

    string _filePath = $"{Application.dataPath}/SaveGame.json";
    private void Awake()
    {
        if (File.Exists(_filePath))
        {
            Load();
        }
    }
    void GetDataToSave()
    {
        _saveData.currentWord = locationToSendAndRecieveData.wordToGuess;
        _saveData.lettersGuessed = locationToSendAndRecieveData.guessedLetters;
    }
    void SaveJSON(HangmanSaveData data, string path)
    {
        string lineToSave = JsonUtility.ToJson(data);       // Convert data to JSON string
        File.WriteAllText(path, lineToSave);                // Write string to file
    }
    public void Save()
    {
        // Gather current game settings
        GetDataToSave();
        // Save to file
        SaveJSON(_saveData, _filePath);
    }


    HangmanSaveData LoadData()
    {
        // Read JSON string
        string loadedData = File.ReadAllText(_filePath);
        // Convert JSON back to data object
        return JsonUtility.FromJson<HangmanSaveData>(loadedData);
    }
    void SendDataFromLoad()
    {
        if (_saveData.currentWord == "" || _saveData.currentWord == null)
        {
            return;
        }
        locationToSendAndRecieveData.wordToGuess = _saveData.currentWord;
        locationToSendAndRecieveData.guessedLetters = _saveData.lettersGuessed;
    }
    public void Load()
    {
        if (!File.Exists(_filePath))
        {
            return;
        }
        _saveData = LoadData();      // Load from disk
        SendDataFromLoad();            // Apply to game
    }
}
