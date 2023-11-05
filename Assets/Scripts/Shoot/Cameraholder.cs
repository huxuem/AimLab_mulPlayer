using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cameraholder : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    float verticalLookRotation;
    float horizontalLookRotation;
    public float DPI;
    public GameObject readytext;
    // Start is called before the first frame update
    void Start()
    {
        DPI = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        readytext.GetComponent<TextMeshPro>().text = "DPI:"+DPI.ToString()+"\r\nShoot this to start!";
        if (DPI<0)
        {
            DPI = 0.01f;
        }
        //transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X"));
        horizontalLookRotation += Input.GetAxisRaw("Mouse X")*DPI;
        horizontalLookRotation = Mathf.Clamp(horizontalLookRotation, -80f, 80f);
        verticalLookRotation += -Input.GetAxisRaw("Mouse Y")*DPI;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation,-90f,13f);
        cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, horizontalLookRotation, 0);
    }
}
