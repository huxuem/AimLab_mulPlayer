using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField]  GameObject target, faketarget;
    [SerializeField] Text Green, Yellow, Greensteal, Yellowsteal, Timer;
    [SerializeField] GameObject HitUI;
    [SerializeField] GameObject Ready_1, Ready_2, Exit_1, Restart_1, Exit_2, Restart_2;
    //public float Count1, Count2;
    public int State = 0;

    private int timeRemain;
    [SerializeField] private int timeMax = 10;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateScore(int GreenScore, int YellowScore, int GreenSteal, int YellowSteal)
    {
        Debug.Log("UpdateScore");
        Green.text = GreenScore.ToString();
        Yellow.text = YellowScore.ToString();
        Greensteal.text = GreenSteal.ToString();
        Yellowsteal.text = YellowSteal.ToString();
    }

    public void PlayerGetHitUI(bool isHit)
    {
        HitUI.SetActive(isHit);
    }

    IEnumerator TimerCoroutine()
    {
        timeRemain = timeMax;
        Timer.text = timeRemain.ToString();
        while (timeRemain > 0)
        {
            yield return new WaitForSeconds(1);
            Timer.text = (--timeRemain).ToString();
        }
        //��ʱ�������˾�ת�׶�
        Globals.Instance.NetworkForCS.ChangeStateReq(2);
    }

    private void ChangeState_UI(bool ReadyState, bool GameoverState)
    {
        Ready_1.SetActive(ReadyState);
        Ready_2.SetActive(ReadyState);

        Exit_1.SetActive(GameoverState);
        Restart_1.SetActive(GameoverState);
        Exit_2.SetActive(GameoverState);
        Restart_2.SetActive(GameoverState);
    }

    public void ChangeState(int state)
    {
        State = state;

        switch (state)
        {
            case 0://תΪReady״̬:���÷�����ʱ��Text������Ready
                ChangeState_UI(true,false);
                Globals.Instance.DataMgr.YellowScore = 0;
                Globals.Instance.DataMgr.GreenScore = 0;
                Timer.text = "����ʱ��";
                break; 

            case 1:
                ChangeState_UI(false, false);
                StartCoroutine(TimerCoroutine());
                break;
            case 2:
                ChangeState_UI(false,true);
                Timer.text = "��Ϸ����";
                break;
            default:
                break;
        }
    }
}
