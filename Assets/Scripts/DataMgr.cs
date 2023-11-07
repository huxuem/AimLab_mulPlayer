using System.Collections.Generic;
using UnityEngine;

public class DataMgr
{
    public int CurrentFrame;

    public int CurrentPlayerId;
    public string CurrentPlayerName = "";
    public int CurrentPlayerColor;

    public int YellowScore;
    public int GreenScore;

    public Dictionary<int, GameObject> AllPlayers = new Dictionary<int, GameObject>();

    public Dictionary<int, GameObject> AllBalls = new Dictionary<int, GameObject>();
}