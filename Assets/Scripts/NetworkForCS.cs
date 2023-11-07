using SLua;
using UnityEngine;

public class NetworkForCS
{
    // 定义Lua函数
    private LuaSvr luaSvr;
    private LuaTable luaSelf;
    private LuaFunction luaConnect;
    private LuaFunction luaUpdate;
    private LuaFunction luaSendInit;
    private LuaFunction luaLogin;
    private LuaFunction luaLogout;
    private LuaFunction luaAction;
    private LuaFunction luaSnapshot;
    private LuaFunction luaAddCoinReq;
    private LuaFunction luaRemoveCoinReq;

    private LuaFunction luaRmPlayerHitReq;
    private LuaFunction luaChangeStateReq;


    public void Init()
    {
        // 启动本地Lua服务器
        luaSvr = new LuaSvr();

        // 在本地Lua服务器上注册函数
        luaSvr.init(null, OnComplete);
    }

    private void OnComplete()
    {
        luaSelf = (LuaTable)luaSvr.start("network");

        luaConnect = (LuaFunction)luaSelf["connect_to_server"];
        luaSendInit = (LuaFunction)luaSelf["send_init"];

        luaUpdate = (LuaFunction)luaSelf["update"];


        luaLogin = (LuaFunction)luaSelf["send_login"];
        luaLogout = (LuaFunction)luaSelf["send_logout"];
        luaAction = (LuaFunction)luaSelf["send_action"];
        luaSnapshot = (LuaFunction)luaSelf["send_snapshot"];

        luaAddCoinReq = (LuaFunction)luaSelf["send_add_coin_req"];
        luaRemoveCoinReq = (LuaFunction)luaSelf["send_remove_coin_req"];

        luaRmPlayerHitReq = (LuaFunction)luaSelf["send_rmplayer_hit_req"];
        luaChangeStateReq = (LuaFunction)luaSelf["send_change_state_req"];
    }
    
     public void ConnectToServer()
    {
        luaConnect.call(luaSelf);
    }

    public void Tick(float dt)
    {
        luaUpdate.call(luaSelf);
    }

    // 初始化网络，在游戏启动时调用
    public void InitRequest()
    {
        luaSendInit.call(luaSelf, 0);
    }

    // 发送本机玩家登陆请求
    public void LoginRequest(string user, string passwd, int color)
    {
        luaLogin.call(luaSelf, user, passwd, color);
    }

    // 发送本机玩家下线请求
    public void LogoutRequest(int id)
    {
        luaLogout.call(luaSelf, id);
    }

    // 发送本机玩家的一帧操作
    public void ActionRequest(int inputH, int inputV, int inputJ, int inputS, float fx, float fz)
    {
        Debug.Log("ActionRequest");
        luaAction.call(luaSelf, Globals.Instance.DataMgr.CurrentPlayerId, Globals.Instance.DataMgr.CurrentFrame, inputH, inputV, inputJ, inputS, fx, fz);
    }

    // 发送本机玩家的一帧状态
    public void SnapshotRequest(int frame, Quaternion rot)
    {
        luaSnapshot.call(luaSelf, Globals.Instance.DataMgr.CurrentPlayerId, frame,
            rot[0], rot[1], rot[2], rot[3]
        );
    }

    public void AddCoinReq(Vector3 pos)
    {
        // 放置在较高位置
        luaAddCoinReq.call(luaSelf, pos[0], pos[1] + 1, pos[2], Globals.Instance.DataMgr.CurrentPlayerId);
    }

    public void RemoveCoinReq(int id, int isGood)
    {
        Debug.Log("color: "+Globals.Instance.DataMgr.CurrentPlayerColor);
        luaRemoveCoinReq.call(luaSelf, id, Globals.Instance.DataMgr.CurrentPlayerColor, isGood);
    }

    public void RmPlayerHitReq(int id)
    {
        //Debug.Log("RemotePlayerGetHit! :" + id);
        luaRmPlayerHitReq.call(luaSelf, id);
    }

    public void ChangeStateReq(int state)
    {
        //state=0为ready，state=1为正常的游戏进程，state=2为游戏结束
        //对state返回的更改写在gamemanager？
        luaChangeStateReq.call(luaSelf, state);
    }
}
