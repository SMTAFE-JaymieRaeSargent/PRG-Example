using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class GameManager : MonoBehaviour
{
    // Increase Click Power
    // Increase amount of auto clicks per trigger
    // Increase speed per auto click trigger

    public int clickPoints = 0;
    public int clickPower = 1;   // Increase Click Power
    public int autoClickPower = 0; // Increase amount of auto clicks per trigger

    public Text clickPointsDisplay;
   // public TMP_Text clickPointsDisplay;

    public float autoClickTimer;
    public float autoClickTimerDuration; // Increase speed per auto click trigger

    public UpgradeData[] upgrades;

    private void Awake()
    {
        autoClickTimerDuration = 1;
    }
    private void Start()
    {
        for (int currentUpgrade = 0; currentUpgrade < upgrades.Length; currentUpgrade++)
        {
            upgrades[currentUpgrade].upgradeButtonText.text = $"{upgrades[currentUpgrade].upgradeName}";
            upgrades[currentUpgrade].upgradeButton.interactable = clickPoints >= upgrades[currentUpgrade].cost;
        }
    }
   
    void Update()
    {
        AutoClick();
        UpdateUI();// dont care about optimisation
    }
    public void ClickButton()
    {
        clickPoints += clickPower;
        //UpdateUI(); if you do then put it where its actually needed
    }
    void AutoClick()
    {
        autoClickTimer += Time.deltaTime;
        if (autoClickTimer >= autoClickTimerDuration)
        {
            clickPoints += autoClickPower;
            autoClickTimer = 0;
        }
    }
    void UpdateUI()
    {
        clickPointsDisplay.text = $"${clickPoints}";
        for (int currentUpgrade = 0; currentUpgrade < upgrades.Length; currentUpgrade++)
        {
           // upgrades[currentUpgrade].upgradeButtonText.text = $"{upgrades[currentUpgrade].upgradeName}";
            upgrades[currentUpgrade].upgradeButton.interactable = clickPoints >= upgrades[currentUpgrade].cost;
        }
    }
    public void Buy(int index)
    {
        if (clickPoints < upgrades[index].cost)
        {
            return;
        }
        clickPoints -= upgrades[index].cost;
        upgrades[index].level++;
        upgrades[index].cost = Mathf.CeilToInt(upgrades[index].cost * upgrades[index].costMultiplier);
        switch (upgrades[index].upgradeType)
        {
            case UpgradeType.ClickPower:
                clickPower += Mathf.RoundToInt(upgrades[index].amount);
                break;
            case UpgradeType.AutoClickerPower:
                autoClickPower += Mathf.RoundToInt(upgrades[index].amount);
                break;
            case UpgradeType.AutoClickerTime:
                break;
            default:
                break;
        }
    }
}
