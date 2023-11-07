using SLua;
using UnityEngine;

[CustomLuaClass]
public class NetworkForLua
{
    public void RecvConnectOK()
    {
        Debug.Log("连接服务器成功");
        LoginButton btn = GameObject.Find("Login").GetComponent<LoginButton>();
        btn.Login();
    }

    // 登录后，为本机玩家设置ID
    public void SetCurrentPlayerResponse(int id, string currentPlayerName, int color)
    {
        Globals.Instance.DataMgr.CurrentPlayerId = id;
        Globals.Instance.DataMgr.CurrentPlayerName = currentPlayerName;
        Globals.Instance.DataMgr.CurrentPlayerColor = color;
        //Debug.Log("Color:" + color);

        if (id != -1)
        {
            Debug.Log("已登录，本机用户 ID = " + Globals.Instance.DataMgr.CurrentPlayerId + " PlayerName =" + Globals.Instance.DataMgr.CurrentPlayerName);
        }
        else
        {
            Debug.Log("登录失败，原因可能是密码错误或已在线，请关闭客户端并重试");
        }
    }

    // 创建玩家
    public void CreatePlayerResponse(int id, string name, int color, float x, float y, float z)
    {
        GameObject tempGo;
        if (Globals.Instance.DataMgr.AllPlayers.TryGetValue(id, out tempGo))
        {
            if (null != tempGo)
            {
                return;
            }
            else
            {
                Globals.Instance.DataMgr.AllPlayers.Remove(id);
            }
        }

        Vector3 pos = new Vector3(x, y, z);
        Transform Ready = GameObject.Find("Ready").transform;

        // 创建本机玩家
        if (Globals.Instance.DataMgr.CurrentPlayerId == id)
        {
            // 创建物体
            var playerPrefab = Resources.Load("Player") as GameObject;
            //纠正旋转
            float curRot = color == 0 ? 90 : -90;
            var player = Object.Instantiate(playerPrefab, pos, Quaternion.Euler(new Vector3(0,curRot,0)));

            if (player == null)
            {
                Debug.Log("创建玩家失败");
                return;
            }

            player.transform.name = "Player_local";
            //player.transform.LookAt(Ready);

            // 在玩家列表中注册
            Globals.Instance.DataMgr.AllPlayers.Add(id, player);
            var pc = player.GetComponent<TargetShooter>();
            //Debug.Log(pc+" "+color);
            pc.currentPlayerId = id;
            pc.SetColor(color);
        }
        // 创建其他玩家
        else
        {
            var playerPrefab = Resources.Load("PlayerRemote") as GameObject;
            //纠正旋转
            float curRot = color == 0 ? 90 : -90;
            var player1 = Object.Instantiate(playerPrefab, pos, Quaternion.Euler(new Vector3(0, curRot, 0)));

            if (player1 == null) Debug.Log("创建玩家失败");

            player1.transform.name = "Player_remote" + id;
            //player1.transform.LookAt(Ready);

            // 在玩家列表中注册
            Globals.Instance.DataMgr.AllPlayers.Add(id, player1);

            var pc = player1.GetComponent<RemoteShooter>();
            pc.currentPlayerId = id;
            pc.localFrame = Globals.Instance.DataMgr.CurrentFrame;
            pc.serverFrame = Globals.Instance.DataMgr.CurrentFrame;
            pc.SetColor(color);
        }
    }

    // 移除玩家
    public void RemovePlayerResponse(int id)
    {
        Debug.LogFormat("RemovePlayer:{0}", id);

        if (Globals.Instance.DataMgr.AllPlayers.ContainsKey(id))
        {
            var player = Globals.Instance.DataMgr.AllPlayers[id];
            Globals.Instance.DataMgr.AllPlayers.Remove(id);
            Object.Destroy(player);
        }
    }

    // 处理其他客户端发来的操作
    //#############################现在远程没有多少动作，纯靠SnapShot可以解决，这个函数废弃
    public void ActionResponse(int id, int frame, int inputH, int inputV, int inputJump, int inputS, float fx, float fz)
    {


    }

    // 处理服务器定时发送的全局同步请求
    public void SyncInfoResponse(string info)
    {
        if (Globals.Instance.DataMgr.CurrentPlayerId == -1) return;
        if (!Globals.Instance.DataMgr.AllPlayers.ContainsKey(Globals.Instance.DataMgr.CurrentPlayerId)) return;

        //手动设置本机所有可上传的信息所在脚本的标志变量（isSyncAll）
        //但是现在不用那个，先去掉
        //Globals.Instance.DataMgr.AllPlayers[Globals.Instance.DataMgr.CurrentPlayerId].GetComponent<LocalDwarfController>().isSyncAll = true;
    }

    // 处理其他玩家发来的状态数据
    public void SnapshotResponse(int id, int frame, double r1, double r2, double r3, double r4)
    {
        if (id == Globals.Instance.DataMgr.CurrentPlayerId)
        {
            return;
        }

        // 打包数据
        //Vector3 pos = new Vector3((float)p1, (float)p2, (float)p3);
        Quaternion rot = new Quaternion((float)r1, (float)r2, (float)r3, (float)r4);
        //Vector3 scl = new Vector3((float)s1, (float)s2, (float)s3);

        //处理已在线玩家的Snapshot
        if (Globals.Instance.DataMgr.AllPlayers.ContainsKey(id))
        {
            var player = Globals.Instance.DataMgr.AllPlayers[id];
            player.GetComponent<RemoteShooter>().HandleSnapshot(frame, rot);
        }
    }

    public void AddCoinResponse(int id, float x, float y, float z, int pickSide, int isGood, bool isChanged)
    {
        //Debug.Log("addCoinStart");

        GameObject tempGo;
        if (Globals.Instance.DataMgr.AllBalls.TryGetValue(id, out tempGo))
        {
            //说明已经存在该value，是有人击中了已有的球。也可能是同步了已有的球，但那时pickside就==2了
            if (null != tempGo)
            {
                Debug.Log("TargetID: " + id);
                int YellowChange = 0;
                int GreenChange = 0;
                //把原有的球改位置，且知道是谁打的

                if (pickSide == 0)
                {
                    if(isGood == 1)
                    {
                        Coin tmpCoin = tempGo.GetComponent<Coin>();
                        tmpCoin.changePos(new Vector3(x, y, z));
                        YellowChange= 1;
                    }
                    else if(isGood == 0)
                    {
                        Coin_Bad tmpCoinBad = tempGo.GetComponent<Coin_Bad>();
                        tmpCoinBad.changePos(new Vector3(x, y, z));
                        YellowChange = -1;
                    }

                }
                else if(pickSide == 1)
                {
                    if (isGood == 1)
                    {
                        Coin tmpCoin = tempGo.GetComponent<Coin>();
                        tmpCoin.changePos(new Vector3(x, y, z));
                        GreenChange= 1;
                    }
                    else if (isGood == 0)
                    {
                        Coin_Bad tmpCoinBad = tempGo.GetComponent<Coin_Bad>();
                        tmpCoinBad.changePos(new Vector3(x, y, z));
                        GreenChange= -1;
                    }
                }

                if(Gamemanager.instance.State == 1)//只有游玩状态能改分数
                {
                    Globals.Instance.DataMgr.YellowScore += YellowChange;
                    Globals.Instance.DataMgr.GreenScore += GreenChange;
                    if (isChanged)
                    {
                        Globals.Instance.DataMgr.YellowSteal += YellowChange;
                        Globals.Instance.DataMgr.GreenSteal += GreenChange;
                        //Debug.Log("YelloSteal:" + Globals.Instance.DataMgr.YellowSteal + ", GreeSteal:" + Globals.Instance.DataMgr.GreenSteal);
                    }
                    //更新UI
                    Gamemanager.instance.UpdateScore(
                        Globals.Instance.DataMgr.GreenScore, Globals.Instance.DataMgr.YellowScore,
                         Globals.Instance.DataMgr.GreenSteal, Globals.Instance.DataMgr.YellowSteal);
                }
            }
        }
        else
        {
            Vector3 pos = new Vector3(x, y, z);
            //Debug.Log("BallPos: " + pos);

            //真target假target分开处理
            if(isGood == 1)
            {
                var TarPrefab = Resources.Load("NewTarget") as GameObject;
                var TarGo = Object.Instantiate(TarPrefab, pos, Quaternion.identity);

                TarGo.transform.name = "Target" + id;

                var coin = TarGo.GetComponent<Coin>();
                coin.Init(id, pickSide);
                Globals.Instance.DataMgr.AllBalls.Add(id, TarGo);
            }
            else
            {
                var TarPrefab = Resources.Load("NewFakeTarget") as GameObject;
                var TarGo = Object.Instantiate(TarPrefab, pos, Quaternion.identity);

                TarGo.transform.name = "FakeTarget" + id;

                var coin = TarGo.GetComponent<Coin_Bad>();
                coin.Init(id, pickSide);
                Globals.Instance.DataMgr.AllBalls.Add(id, TarGo);
            }

        }
    }

    public void PlayerGetHitResponse(int id)
    {
        Debug.Log("player get hit:" + id);
        if(id == Globals.Instance.DataMgr.CurrentPlayerId)
        {
            Globals.Instance.DataMgr.AllPlayers.TryGetValue(id, out var result);
            if (result != null)
            {
                TargetShooter player = result.GetComponent<TargetShooter>();
                if (player != null)
                {
                    player.GetHit();
                }
            }
        }
    }

    public void ChangeStateResponse(int state)
    {
        Debug.Log("curstate:"+state);
        Gamemanager.instance.ChangeState(state);
    }



    //#######################################################   已废弃
    public void RemoveCoinResponse(int id, int pickerPlayerId)
    {
        //GameObject tempGo;
        //if (Globals.Instance.DataMgr.AllCoins.TryGetValue(id, out tempGo))
        //{
        //    Globals.Instance.DataMgr.AllCoins.Remove(id);

        //    if (null != tempGo)
        //    {
        //        Object.Destroy(tempGo);
        //    }
        //}
    }

}