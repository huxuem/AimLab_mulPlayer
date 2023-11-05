using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooter : MonoBehaviour
{
    public int currentPlayerId = -1;
    public int curSide;
    [SerializeField] Camera cam;

    private Renderer renderer;
    [SerializeField] Material RedMat;
    [SerializeField] Material BlueMat;



    private void Start()
    {
        //Debug.Log(transform.GetChild(0)+"########################################");
        //Debug.Log(renderer);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Click");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject);
                Coin target = hit.collider.gameObject.GetComponent<Coin>();
                if(target != null )
                {
                    Debug.Log("Hit!");
                    target.Hit();
                }
            }
        }
    }

    public void SetColor(int color)
    {
        //Debug.Log(" color:" + color);
        //Debug.Log(renderer);
        //Debug.Log("SetColor: " + renderer.material+" color:"+color);
        renderer = transform.GetChild(0).GetComponent<Renderer>();
        //renderer.material = (color == 0 ? RedMat : BlueMat);
        Debug.Log(renderer.material);
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
