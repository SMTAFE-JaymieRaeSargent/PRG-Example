using UnityEngine;
using System.IO;
using Unity.Collections;
using System;

public class DifficultySelect : MonoBehaviour
{
    // path
    string _filePath = $"{Application.dataPath}/Difficulty.json";
    private Difficulty _difficulty = new Difficulty();
    // getting the selected Difficulty
    void Difficulty(string difficultySetting)
    {
        _difficulty.difficulty = difficultySetting;
    }
    // save that selected difficulty to file
    void SaveDifficultySelectionToJson(Difficulty selectedDifficulty, string filePath)
    {
        string saveToJson = JsonUtility.ToJson(selectedDifficulty);
        File.WriteAllText(filePath, saveToJson);
    }
    // public behaviour to run those to functions
    public void SaveDifficulty(string difficulty)
    {
        Difficulty(difficulty);
        SaveDifficultySelectionToJson(_difficulty, _filePath);
    }
}
[Serializable]
public class Difficulty
{
    public string difficulty;
}