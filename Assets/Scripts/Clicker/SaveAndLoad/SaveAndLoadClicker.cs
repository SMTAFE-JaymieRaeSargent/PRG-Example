using UnityEngine;
using System.IO;
public class SaveAndLoadClicker : MonoBehaviour
{
    // The main data object that holds all save data.
    public SaveDataClicker dataToSaveAndLoad = new SaveDataClicker();
    // In game location of save data
    public GameManager locationToSendAndRecieveData;
    // File path where the JSON options file will be saved.
    string _filePath = $"{Application.dataPath}/ClickerData.json";
    private void Awake()
    {
        if (File.Exists(_filePath))
        {
            Load();
        }
    }
    void GetDataToSave()
    {
        dataToSaveAndLoad.clickPoints = locationToSendAndRecieveData.clickPoints;
        dataToSaveAndLoad.clickPower = locationToSendAndRecieveData.clickPower;
        dataToSaveAndLoad.autoClickPower = locationToSendAndRecieveData.autoClickPower;
        dataToSaveAndLoad.upgrades = locationToSendAndRecieveData.upgrades;
        dataToSaveAndLoad.autoClickTimer = locationToSendAndRecieveData.autoClickTimer;
        dataToSaveAndLoad.autoClickTimerDuration = locationToSendAndRecieveData.autoClickTimerDuration;
    }
    void SaveJSON(SaveDataClicker data, string path)
    {
        string lineToSave = JsonUtility.ToJson(data);       // Convert data to JSON string
        File.WriteAllText(path, lineToSave);                // Write string to file
    }
    public void Save()
    {
        // Gather current game settings
        GetDataToSave();
        // Save to file
        SaveJSON(dataToSaveAndLoad, _filePath);
    }


    SaveDataClicker LoadData()
    {
        // Read JSON string
        string loadedData = File.ReadAllText(_filePath);
        // Convert JSON back to data object
        return JsonUtility.FromJson<SaveDataClicker>(loadedData);
    }
    void SendDataFromLoad()
    {
        locationToSendAndRecieveData.clickPoints = dataToSaveAndLoad.clickPoints;
        locationToSendAndRecieveData.clickPower = dataToSaveAndLoad.clickPower;
        locationToSendAndRecieveData.autoClickPower = dataToSaveAndLoad.autoClickPower;
        locationToSendAndRecieveData.autoClickTimer = dataToSaveAndLoad.autoClickTimer;
        locationToSendAndRecieveData.autoClickTimerDuration = dataToSaveAndLoad.autoClickTimerDuration;
        for (int upgrade = 0; upgrade < locationToSendAndRecieveData.upgrades.Length; upgrade++)
        {
            locationToSendAndRecieveData.upgrades[upgrade].level = dataToSaveAndLoad.upgrades[upgrade].level;
            locationToSendAndRecieveData.upgrades[upgrade].amount = dataToSaveAndLoad.upgrades[upgrade].amount;
            locationToSendAndRecieveData.upgrades[upgrade].cost = dataToSaveAndLoad.upgrades[upgrade].cost;
            locationToSendAndRecieveData.upgrades[upgrade].costMultiplier = dataToSaveAndLoad.upgrades[upgrade].costMultiplier;
        }             

    }
    public void Load()
    {
        dataToSaveAndLoad = LoadData();      // Load from disk
        SendDataFromLoad();            // Apply to game
    }
}
