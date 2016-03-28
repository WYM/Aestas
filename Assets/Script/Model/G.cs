using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class G
{
    public static string server = "127.0.0.1";


    public static string agcPath = Application.streamingAssetsPath + "/agc/";
    public static string tempPath = Application.streamingAssetsPath + "/temp/";
    public static string originConfigPath = agcPath + "/config/";
    public static string originScriptPath = agcPath + "/script/";
    public static string tempConfigPath = tempPath + "/config/";
    public static string tempScriptPath = tempPath + "/script/";

    public static string agcEncryptKey = "wxhpcdmt";
    public static string characterAgcFilename = "character";
    public static string suitAgcFilename = "suit";
    public static string emotionAgcFilename = "emotion";

    public static bool isStreamingResource = false;
    public static string titleCharacterPath = "UI/TitleEX/Characters/";
    public static string emotionPath = "Sprites/Characters/Emotions/";
    public static string suitPath = "Sprites/Characters/Suits/";
    public static string avatarPath = "Sprites/Characters/TalkAvatar/";
    public static string bgPath = "Sprites/Background/";
    public static string bgmPath = "Sound/BGM/";
    public static string sfxPath = "Sound/SFX/";
    public static string voicePath = "Sound/Voice/";

    public static Dictionary<string, string> characterPos
    {
        get
        {
            Dictionary<string, string> cPos = new Dictionary<string, string>();
            cPos.Add("c", "0,0");
            cPos.Add("1c", "0,0");
            cPos.Add("2c", "0,0");
            cPos.Add("3c", "0,0");
            cPos.Add("2l", "-450,0");
            cPos.Add("2r", "450,0");
            cPos.Add("3l", "-580,0");
            cPos.Add("3r", "580,0");
            return cPos;
        }
    }


}
