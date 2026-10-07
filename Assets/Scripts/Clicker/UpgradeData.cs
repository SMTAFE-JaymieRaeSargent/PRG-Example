using System;
using UnityEngine.UI;
[Serializable]
public struct UpgradeData 
{
    public string upgradeName;
    public UpgradeType upgradeType;

    public int level;
    public int amount;
    public int cost;
    public float costMultiplier;
    public Button upgradeButton;
    public Text upgradeButtonText;
}
public enum UpgradeType
{
    ClickPower, 
    AutoClickerPower,
    AutoClickerTime
}
