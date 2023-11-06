using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class TargetShooter : MonoBehaviour
{
    public int currentPlayerId = -1;
    public int curSide;
    [SerializeField] Camera cam;
    [SerializeField] Animator animator;
    private float timer;

    private Renderer renderer;
    [SerializeField] Material RedMat;
    [SerializeField] Material BlueMat;

    public Cameraholder Cameraholder;
    private GameObject ready;

    #region Variables

    [Header("Controller Input")] public bool inputEnabled = true;
    public string horizontalInput = "Horizontal";
    public string verticallInput = "Vertical";

    [Header("Camera Input")] public string rotateCameraXInput = "Mouse X";
    public string rotateCameraYInput = "Mouse Y";

    //[SerializeField] private Camera cameraMain;

    private int localFrame = 0;
    private bool isPauseUpdate;
    Vector3 cameraForward = new Vector3(0.0f, 1.0f, 0.0f);
    #endregion

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
        ResetFixInput();
        StartCoroutine(AutoSnapshot());
        //Debug.Log(transform.GetChild(0)+"########################################");
        //Debug.Log(renderer);
    }

    #region SnapShot
    IEnumerator AutoSnapshot()
    {
        while (true)
        {
            // SendLocalSnapshot();
            SendSnapshot();
            float waitTime = UnityEngine.Random.Range(0.2f, 0.4f);
            yield return new WaitForSeconds(waitTime);
        }
    }


    // 发送本机玩家状态，要改成只发送旋转
    void SendSnapshot()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Vector3 scl = transform.localScale;

        Globals.Instance.NetworkForCS.SnapshotRequest(localFrame, pos, rot, scl);
    }

#endregion

    void Update()
    {   
        
        if(Input.GetMouseButtonDown(0)&& Time.time>timer+0.1f)
        {
            //Debug.Log("Click");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                animator.SetTrigger("Shoot");
                //Debug.Log(hit.collider.gameObject);
                if (hit.collider.CompareTag("TargetGood"))
                {
                    Coin target = hit.collider.gameObject.GetComponent<Coin>();
                    if (target != null)
                    {
                        //Debug.Log("Hit!");
                        target.Hit();
                    }
                }
                else if (hit.collider.CompareTag("TargetBad"))
                {
                    Coin_Bad target = hit.collider.gameObject.GetComponent<Coin_Bad>();
                    if (target != null)
                    {
                        //Debug.Log("Hit!");
                        target.Hit();
                    }
                }


                //这里用的是target而非coin，暂时锁掉

                switch (hit.collider.gameObject.name)
                {
                    case "+":
                        Cameraholder.DPI += 0.1f;
                        break;
                    case "-":
                        Cameraholder.DPI -= 0.1f;
                        break;
                    case "++":
                        Cameraholder.DPI += 0.5f;
                        break;
                    case "--":
                        Cameraholder.DPI -= 0.5f;
                        break;
                    case "Ready":
                        //ready.SetActive(false);
                        break;
                    default: break;
                }
                //Debug.Log("DPI:" + Cameraholder.DPI);

            }
            timer = Time.time;
        }

        StoreInput();
    }

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




    private void StoreInput()
    {
        // 把本帧的输入保存下来，等待下一次FixUpdate时统一处理

        if (Input.GetKey(KeyCode.Space)) fixInput.Space = true;
        if (Input.GetKeyDown(KeyCode.Space)) fixInput.SpaceDown = true;
        if (Input.GetKeyUp(KeyCode.Space)) fixInput.SpaceUp = true;

        if (Input.GetKey(KeyCode.LeftShift)) fixInput.LeftShift = true;
        if (Input.GetKeyDown(KeyCode.LeftShift)) fixInput.LeftShiftDown = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) fixInput.LeftShiftUp = true;

        fixInput.Y += Input.GetAxis(rotateCameraYInput);
        fixInput.X += Input.GetAxis(rotateCameraXInput);
    }

    // 下面的这个FixUpdate才是真正处理联网逻辑的函数
    // - 为什么不用Update处理呢？
    // - 不同人的电脑配置不同，Update的帧数也不同，但数据的收发、处理都是和帧序号相关的，容易形成混乱
    // - 所以统一用每秒50帧的速率来处理
    private void FixedUpdate()
    {
        if (!isPauseUpdate)
            NaturalUpdate();
        else
            Debug.Log("暂停更新帧");

        // 处理完成，清空输入
        ResetFixInput();
    }

    void ResetFixInput()
    {
        fixInput.Space = false;
        fixInput.SpaceDown = false;
        fixInput.SpaceUp = false;

        fixInput.LeftShift = false;
        fixInput.LeftShiftDown = false;
        fixInput.LeftShiftUp = false;

        fixInput.Y = 0;
        fixInput.X = 0;
    }

    // 计算一帧，用于更新本地玩家
    // 这里的具体逻辑不属于网络编程的范畴，略读即可
    void NaturalUpdate()
    {
        // 更新本机帧号
        localFrame++;

        //InputHandle(); // 处理玩家输入
        NetworkHandle(); // 处理网络逻辑
        //cc.UpdateAnimator(); // 更新动画状态

    }

    // 每帧处理网络逻辑，在需要时向服务器上传数据
    void NetworkHandle()
    {
        if (!inputEnabled) return;

        // 处理相机输入，获取玩家当前的方向
        var right = Vector3.right;
        var forward = Vector3.forward;
        //var cameraMain = tpCamera.GetComponent<Camera>();
        if (cam)
        {
            right = cam.transform.right;
            right.y = 0.0f;
            right.Normalize();
            forward = cam.transform.forward;
            forward.y = 0.0f;
            forward.Normalize();
        }

        // 当前方向与上一帧的差值
        Vector3 fowardDiff = cameraForward.normalized - forward.normalized;


        //如果 按键情况改变 || 持续按键且人物朝向改变 则发送操作
        if (fowardDiff.sqrMagnitude > 0.0f)
        {

            // 更新朝向记录
            cameraForward = forward;

            // 发送操作
            SendDwarfAction(0, 0, 0, 0, cameraForward.x, cameraForward.z);
        }
    }

    // 发送一次操作
    void SendDwarfAction(int h, int v, int jump, int sprint, float fx, float fz)
    {
        Globals.Instance.NetworkForCS.ActionRequest(h, v, jump, sprint, fx, fz);
    }


}
