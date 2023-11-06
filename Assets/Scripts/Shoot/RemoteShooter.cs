using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;


// 定义格式：玩家操作数据包
public class InputActionInfo
{
    public int id = 0; //玩家id
    public int frame = 0; //当前帧号
    public int inputH = 0; //水平（AD）输入
    public int inputV = 0; //垂直（WS）输入
    public int inputJ = 0; //跳跃输入
    public int inputS = 0; //冲刺输入
    public Vector3 forward = new Vector3(0.0f, 0.0f, 1.0f); //玩家朝向
    public Vector3 right = new Vector3(1.0f, 0.0f, 0.0f); //玩家朝向

    public InputActionInfo(int i, int f, int h, int v, int jump, int sprint, float fx, float fz)
    {
        id = i;
        frame = f;
        inputH = h;
        inputV = v;
        inputJ = jump;
        inputS = sprint;
        forward.x = fx;
        forward.y = 0.0f;
        forward.z = fz;
        forward.Normalize();
        right = Vector3.Cross(Vector3.up, forward);
        right.Normalize();
    }
}



public class RemoteShooter : MonoBehaviour
{
    public int currentPlayerId = -1;
    public int curSide;

    public int localFrame;
    public int serverFrame = 0;
    public int currentFrame = 0;
    List<InputActionInfo> inputActions = new List<InputActionInfo>(); //输入队列
    InputActionInfo MyInputAction = new InputActionInfo(0, 0, 0, 0, 0, 0, 0, 0);


    //[SerializeField] Camera cam;

    private Renderer renderer;
    [SerializeField] Material RedMat;
    [SerializeField] Material BlueMat;



    struct FixInput
    {
        public bool Space;
        public bool SpaceDown;
        public bool SpaceUp;

        public bool LeftShift;
        public bool LeftShiftDown;
        public bool LeftShiftUp;

        public float Y;
        public float X;

        //添加内容后，记得更新ResetFixInput()
    }

    FixInput fixInput;



    private void Start()
    {
        //ResetFixInput();
        localFrame = Globals.Instance.DataMgr.CurrentFrame;
        //StartCoroutine(AutoSnapshot());
        //Debug.Log(transform.GetChild(0)+"########################################");
        //Debug.Log(renderer);
    }

    public void HandleSnapshot(int frame, Vector3 pos, Quaternion rot, Vector3 scl)
    {
        Debug.Log("RemoteAction get");
        // 人物缩放比例
        transform.localScale = scl;
        transform.rotation = Quaternion.Slerp(Quaternion.identity, rot, 0.9f);
    }

    public void AddRemoteAction(int id, int frame, int h, int v, int jump, int sprint, float fx, float fz)
    {
        //Debug.Log("facex/z: " + fx + " " + fz);
    }

    //#region SnapShot
    //IEnumerator AutoSnapshot()
    //{
    //    while (true)
    //    {
    //        // SendLocalSnapshot();
    //        SendSnapshot();
    //        float waitTime = UnityEngine.Random.Range(0.2f, 0.4f);
    //        yield return new WaitForSeconds(waitTime);
    //    }
    //}

    //// 发送本机玩家状态，要改成只发送旋转
    //void SendSnapshot()
    //{
    //    Vector3 pos = transform.position;
    //    Quaternion rot = transform.rotation;
    //    Vector3 scl = transform.localScale;

    //    Globals.Instance.NetworkForCS.SnapshotRequest(localFrame, pos, rot, scl);
    //}

    //#endregion



    public void SetColor(int color)
    {
        //Debug.Log(" color:" + color);
        //Debug.Log(renderer);
        //Debug.Log("SetColor: " + renderer.material+" color:"+color);
        renderer = transform.GetChild(0).GetComponent<Renderer>();
        //renderer.material = (color == 0 ? RedMat : BlueMat);
        //Debug.Log(renderer.material);
        if(color == 0)
        {
            renderer.material = RedMat;
        }
        else
        {
            renderer.material = BlueMat;
        }
    }





}
