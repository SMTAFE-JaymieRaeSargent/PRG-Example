using System;
using UnityEngine;

[Serializable]
public class SaveDataClicker
{
    public int clickPoints = 0;
    public int clickPower = 1; 
    public int autoClickPower = 0;
    public UpgradeData[] upgrades;
    public float autoClickTimer;
    public float autoClickTimerDuration;
}
