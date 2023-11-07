using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Windows;

public class TargetShooter : MonoBehaviour
{
    public int currentPlayerId = -1;
    //public int curSide;
    [SerializeField] Camera cam;
    [SerializeField] Animator animator;
    private Cameraholder cameraholder;
    private float timer;
    [SerializeField] float ChangeSideTime = 1f;

    private Renderer renderer;
    [SerializeField] Material GreeMat;
    [SerializeField] Material YelloScore;

    public Cameraholder Cameraholder;
    public GameObject ready,gameover,newHits;
    public AudioSource Shoot,ShootOpp,Shoot1;

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

        //������ݺ󣬼ǵø���ResetFixInput()
    }

    FixInput fixInput;



    private void Start()
    {
        ResetFixInput();
        cameraholder = this.GetComponent<Cameraholder>();
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
            float waitTime = UnityEngine.Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(waitTime);
        }
    }


    // ���ͱ������״̬��Ҫ�ĳ�ֻ������ת
    void SendSnapshot()
    {   
        Quaternion rot = cameraholder.cameraHolder.transform.rotation;

        Globals.Instance.NetworkForCS.SnapshotRequest(localFrame, rot);
    }

#endregion

    void Update()
    {   
        
        if(Input.GetMouseButtonDown(0)&& Time.time>timer+0.1f)
        {
            //����жϵ��߼�
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
                        Shoot.Play();
                        Debug.Log("Hit!");
                        GameObject spawnedHit = Instantiate(newHits, hit.point, Quaternion.identity);
                        spawnedHit.transform.LookAt(Camera.main.transform);
                        target.Hit();
                    }
                }
                else if (hit.collider.CompareTag("TargetBad"))
                {
                    Coin_Bad target = hit.collider.gameObject.GetComponent<Coin_Bad>();
                    if (target != null)
                    {
                        Shoot.Play();
                        Debug.Log("Hit!");
                        GameObject spawnedHit = Instantiate(newHits, hit.point, Quaternion.identity);
                        spawnedHit.transform.LookAt(Camera.main.transform);
                        target.Hit();
                    }
                }
                else if (hit.collider.CompareTag("PlayerRemote"))
                {
                    //���߶��Ǵ�playershape�ϵ�
                    RemoteShooter RmPlayer = hit.collider.gameObject.GetComponentInParent<RemoteShooter>();
                    if (RmPlayer != null)
                    {
                        ShootOpp.Play();
                        RmPlayer.Hit();
                    }
                    else Debug.Log("���е�rmplayerû��Remoteshooter��"+hit.collider.gameObject);
                }


                switch (hit.collider.gameObject.name)
                {
                    case "Ready":
                        //Debug.Log("changestate:1");
                        Globals.Instance.NetworkForCS.ChangeStateReq(1);
                        hit.collider.gameObject.SetActive(false);
                        break;
                    case "Ready1":
                        //Debug.Log("changestate:1");
                        Globals.Instance.NetworkForCS.ChangeStateReq(1);
                        hit.collider.gameObject.SetActive(false);
                        break;
                    case "+":
                        Cameraholder.DPI += 0.1f;
                        Shoot1.Play();
                        break;
                    case "-":
                        Cameraholder.DPI -= 0.1f;
                        Shoot1.Play();
                        break;
                    case "++":
                        Cameraholder.DPI += 0.5f;
                        Shoot1.Play();
                        break;
                    case "--":
                        Cameraholder.DPI -= 0.5f;
                        Shoot1.Play();
                        break;
                    case "Exit":
                        Application.Quit();
                        break;
                    case "Exit1":
                        Application.Quit();
                        break;
                    case "Restart":
                        Shoot1.Play();
                        Globals.Instance.NetworkForCS.ChangeStateReq(0);
                        break;
                    case "Restart1":
                        Shoot1.Play();
                        Globals.Instance.NetworkForCS.ChangeStateReq(0);
                        break;
                    default:
                        break;
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
        //renderer.material = (color == 0 ? RedMat : YelloScore);
        //Debug.Log(renderer.material);
        if(color == 0)
        {
            renderer.material = YelloScore;
        }
        else
        {
            renderer.material = GreeMat;
        }
    }

    public void GetHit()
    {
        //Debug.Log("getHit");
        StartCoroutine(ChangeSideCoroutine());
    }

    IEnumerator ChangeSideCoroutine()
    {
        int curColor = Globals.Instance.DataMgr.CurrentPlayerColor;
        Globals.Instance.DataMgr.CurrentPlayerColor = (curColor == 0 ? 1 : 0);
        Debug.Log("ChangeColor:" + Globals.Instance.DataMgr.CurrentPlayerColor);

        Gamemanager.instance.PlayerGetHitUI(true);

        yield return new WaitForSeconds(ChangeSideTime);

        Globals.Instance.DataMgr.CurrentPlayerColor = curColor;
        Debug.Log("Color Back");

        Gamemanager.instance.PlayerGetHitUI(false);
    }




    private void StoreInput()
    {
        // �ѱ�֡�����뱣���������ȴ���һ��FixUpdateʱͳһ����

        if (Input.GetKey(KeyCode.Space)) fixInput.Space = true;
        if (Input.GetKeyDown(KeyCode.Space)) fixInput.SpaceDown = true;
        if (Input.GetKeyUp(KeyCode.Space)) fixInput.SpaceUp = true;

        if (Input.GetKey(KeyCode.LeftShift)) fixInput.LeftShift = true;
        if (Input.GetKeyDown(KeyCode.LeftShift)) fixInput.LeftShiftDown = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) fixInput.LeftShiftUp = true;

        fixInput.Y += Input.GetAxis(rotateCameraYInput);
        fixInput.X += Input.GetAxis(rotateCameraXInput);
    }

    // ��������FixUpdate�����������������߼��ĺ���
    // - Ϊʲô����Update�����أ�
    // - ��ͬ�˵ĵ������ò�ͬ��Update��֡��Ҳ��ͬ�������ݵ��շ��������Ǻ�֡�����صģ������γɻ���
    // - ����ͳһ��ÿ��50֡������������
    private void FixedUpdate()
    {
        if (!isPauseUpdate)
            NaturalUpdate();
        else
            Debug.Log("��ͣ����֡");

        // ������ɣ��������
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

    // ����һ֡�����ڸ��±������
    // ����ľ����߼������������̵ķ��룬�Զ�����
    void NaturalUpdate()
    {
        // ���±���֡��
        localFrame++;

        //InputHandle(); // �����������
        NetworkHandle(); // ���������߼�
        //cc.UpdateAnimator(); // ���¶���״̬

    }

    // ÿ֡���������߼�������Ҫʱ��������ϴ�����
    void NetworkHandle()
    {
        if (!inputEnabled) return;

        // ����������룬��ȡ��ҵ�ǰ�ķ���
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

        // ��ǰ��������һ֡�Ĳ�ֵ
        Vector3 fowardDiff = cameraForward.normalized - forward.normalized;


        //��� ��������ı� || �������������ﳯ��ı� ���Ͳ���
        if (fowardDiff.sqrMagnitude > 0.0f)
        {

            // ���³����¼
            cameraForward = forward;

            // ���Ͳ���
            SendDwarfAction(0, 0, 0, 0, cameraForward.x, cameraForward.z);
        }
    }

    // ����һ�β���
    void SendDwarfAction(int h, int v, int jump, int sprint, float fx, float fz)
    {
        Globals.Instance.NetworkForCS.ActionRequest(h, v, jump, sprint, fx, fz);
    }


}
