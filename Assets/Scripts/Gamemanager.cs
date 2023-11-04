using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]  GameObject target, faketarget;
    public float Count1, Count2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 0; i < Count1; i++)
        {
            Instantiate(target, TargetBound.instance.GetRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < Count2; i++)
        {
            Instantiate(faketarget, TargetBound.instance.GetRandomPosition(), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
