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

        //������ݺ󣬼ǵø���ResetFixInput()
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


    // ���ͱ������״̬��Ҫ�ĳ�ֻ������ת
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


                //�����õ���target����coin����ʱ����

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
