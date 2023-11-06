using System;
using UnityEngine;

public class Coin_Bad : MonoBehaviour
{
    private int isGood = 0;

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
        Debug.Log("HitFake");
        Globals.Instance.NetworkForCS.RemoveCoinReq(CoinId, isGood);
    }

}