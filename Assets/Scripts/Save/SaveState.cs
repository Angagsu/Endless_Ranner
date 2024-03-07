using System;

[Serializable]
public class SaveState 
{
    [NonSerialized] private const int HAT_COUNT = 16;

    public int Highscore;
    public int Fish;
    public DateTime LastSaveTime;
    public int CurrentHatIndex;

    public byte[] UnlockedHatFlag;

    public SaveState()
    {
        Highscore = 0;
        Fish = 10;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        UnlockedHatFlag = new byte[HAT_COUNT];
        UnlockedHatFlag[0] = 1;
    }
}
