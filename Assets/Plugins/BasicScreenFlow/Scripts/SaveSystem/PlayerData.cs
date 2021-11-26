using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public bool firstTime = true;
    public string playerName = "";
    public string playerAge = "";
    public bool termsAccepted = false;
    public bool _BGMOn = true;
    public bool _SFXOn = true;
    public int[] keysLevelsCompleted = new int[0];
    public bool[] valuesLevelsCompleted = new bool[0];
    public int[] keysEggsCaughtInLevels = new int[0];
    public int[] valuesEggsCaughtInLevels = new int[0];
    public int lastLevelAvailable = 0;
}
