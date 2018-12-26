using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour, IGameSaver {

    // using string constants - to avoid bugs by typos
    private const string hasSavedDataKey = "HasSavedData";
    private const string Player1PointsKey = "Player1Points";
    private const string Player2PointsKey = "Player2Points";
    private const string Player2HumanKey = "Player2Human";
    private const string trueStr = "true";
    private const string falseStr = "false";

    public bool LoadGame(out SaveData data) {
        
        if (PlayerPrefs.HasKey(hasSavedDataKey)) {
            data.player1Points = PlayerPrefs.GetInt(Player1PointsKey);
            data.player2Points = PlayerPrefs.GetInt(Player2PointsKey);
            data.player2Human = PlayerPrefs.GetString(Player2HumanKey) == trueStr;
            return true;
        } else {
            data.player1Points = 0;
            data.player2Points = 0;
            data.player2Human = false;
            return false;
        }
    }

    public bool HasSavedGame() {
        return PlayerPrefs.HasKey(hasSavedDataKey);
    }

    public void SaveGame(SaveData data) {
        PlayerPrefs.SetInt(Player1PointsKey, data.player1Points);
        PlayerPrefs.SetInt(Player2PointsKey, data.player2Points);
        PlayerPrefs.SetString(Player2HumanKey, data.player2Human == true ? trueStr : falseStr);
        PlayerPrefs.SetString(hasSavedDataKey, trueStr);
    }


}
