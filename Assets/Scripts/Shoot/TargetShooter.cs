using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Cameraholder Cameraholder;
    public GameObject ready,gameover,newHits;
    public AudioSource Shoot,ShootOpp,Shoot1;

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
                switch(hit.collider.gameObject.name)
                {
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
                    case "Ready£¿":
                        ready.SetActive(false);
                        Shoot1.Play();
                        break;
                    case "Gameover":
                        Application.Quit();
                        break;
                    case "Tryagain":
                        gameover.SetActive(false);
                        ready.SetActive(true);
                        Shoot1.Play();
                        break;
                    case "Opp":
                        ShootOpp.Play();
                        break;
                    default:
                        Target target = hit.collider.gameObject.GetComponent<Target>();
                        if (target != null)
                        {
                            Shoot.Play();
                            Debug.Log("Hit!");
                            target.Hit();
                            GameObject spawnedHit=Instantiate(newHits, hit.point, Quaternion.identity);
                            spawnedHit.transform.LookAt(Camera.main.transform);
                        }
                        break;
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
