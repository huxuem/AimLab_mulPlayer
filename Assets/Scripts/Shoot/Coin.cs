using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int isGood = 1;

    [NonSerialized] public int CoinId;

    [NonSerialized] public int OwnerPlayerId;


    public void Init(int coinId, int ownerPlayerId)
    {
        CoinId = coinId;
        OwnerPlayerId = ownerPlayerId;
    }

    public void changePos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Hit()
    {
        Globals.Instance.NetworkForCS.RemoveCoinReq(CoinId, isGood);
    }

}