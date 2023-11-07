using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField]  GameObject target, faketarget;
    [SerializeField] Text Red, Blue;
    public float Count1, Count2;


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

    public void UpdateScore(int RedScore, int BlueScore)
    {
        Debug.Log("UpdateScore");
        Red.text = RedScore.ToString();
        Blue.text = BlueScore.ToString();
    }
}
