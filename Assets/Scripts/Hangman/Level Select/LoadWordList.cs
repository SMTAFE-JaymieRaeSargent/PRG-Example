using UnityEngine;
using System.IO;
public class LoadWordList : MonoBehaviour
{
    public WordLists words;
    string _selectDifficultyPath = $"{Application.dataPath}/Difficulty.json";
    Difficulty _difficulty;
    string _filePath;
    public Hangman.GameManager instance;
    Difficulty LoadDifficultyPath(string path)
    {
        string loadedDataFromFile = File.ReadAllText(path);
        return JsonUtility.FromJson<Difficulty>(loadedDataFromFile);
    }
    private void Awake()
    {
        _difficulty = LoadDifficultyPath(_selectDifficultyPath);
        _filePath = $"{Application.dataPath}/{_difficulty.difficulty}.json";
        words = LoadWordListFromPath(_filePath);
        instance.wordToGuess = ChooseRandomWordFromList();
    }
    WordLists LoadWordListFromPath(string path)
    {
        string loadedDataFromFile = File.ReadAllText(path);
        return JsonUtility.FromJson<WordLists>(loadedDataFromFile);
    }
    string ChooseRandomWordFromList()
    {
        int index = Random.Range(0,words.wordList.Length);
        return words.wordList[index];
    }
}
