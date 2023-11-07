using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cameraholder : MonoBehaviour
{
    public Transform cameraHolder;
    float verticalLookRotation;
    float horizontalLookRotation;
    public float DPI;
    private GameObject Ready;
     private GameObject Ready1;
    // Start is called before the first frame update
    void Start()
    {
        Ready = GameObject.Find("Ready");
        Ready1 = GameObject.Find("Ready1");

        DPI = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Ready.GetComponentInChildren<TextMeshPro>().text = "DPI:" + DPI.ToString()+"\r\nShoot this to start!";
        Ready1.GetComponentInChildren<TextMeshPro>().text = "DPI:" + DPI.ToString() + "\r\nShoot this to start!";
        if (DPI<0)
        {
            DPI = 0.1f;
        }
        //transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X"));
        horizontalLookRotation += Input.GetAxisRaw("Mouse X")*DPI;
        horizontalLookRotation = Mathf.Clamp(horizontalLookRotation, -80f, 80f);
        verticalLookRotation += -Input.GetAxisRaw("Mouse Y")*DPI;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation,-90f,45f);
        cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, horizontalLookRotation, 0);

        DPI = Mathf.Round(DPI * 100) / 100f;
    }
}
