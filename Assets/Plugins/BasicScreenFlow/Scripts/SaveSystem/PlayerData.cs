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
    public bool[] levels = new [] {false,false,false,false,false,false,false,false,false};
    public int lastLevelAvailable = 0;
}
