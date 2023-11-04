using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooter : MonoBehaviour
{
    public int currentPlayerId = -1;
    public int curSide;
    [SerializeField] Camera cam;
    [SerializeField] Animator animator;

    private Renderer renderer;
    [SerializeField] Material RedMat;
    [SerializeField] Material BlueMat;

    private float timer;

    private void Start()
    {
        timer=Time.time;
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {   
        
        if(Input.GetMouseButtonDown(0)&& Time.time>timer+0.1f)
        {
            //Debug.Log("Click");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                animator.SetTrigger("Shoot");
                Debug.Log(hit.collider.gameObject);
                Target target = hit.collider.gameObject.GetComponent<Target>();
                if(target != null )
                {
                    Debug.Log("Hit!");
                    target.Hit();
                }
            }
            timer = Time.time;
        }
    }

    public void SetColor(int color)
    {
        renderer.material = color == 0 ? RedMat : BlueMat;
    }
}
