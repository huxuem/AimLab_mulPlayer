using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
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
        Globals.Instance.NetworkForCS.RemoveCoinReq(CoinId);
    }

    private void Update()
    {
        //if (OwnerPlayerId == Globals.Instance.DataMgr.CurrentPlayerId)
        //{
        //    return;
        //}

        //if (Time.frameCount % 5 != 0)
        //{
        //    return;
        //}

        //var player = Globals.Instance.DataMgr.AllPlayers[Globals.Instance.DataMgr.CurrentPlayerId];
        //if (null == player)
        //{
        //    return;
        //}

        //if (Vector2.Distance(
        //        new Vector2(player.transform.position.x, player.transform.position.z),
        //        new Vector2(transform.position.x, transform.position.z))
        //    < 1)
        //{
        //    Globals.Instance.NetworkForCS.RemoveCoinReq(CoinId);
        //}
    }
}