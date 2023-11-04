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
        renderer = GetComponent<Renderer>();
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
                Target target = hit.collider.gameObject.GetComponent<Target>();
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
        renderer.material = color == 0 ? RedMat : BlueMat;
    }
}
