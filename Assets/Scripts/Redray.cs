using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redray : MonoBehaviour
{
    public float laserLength = 100f; // ���ⳤ��
    public LineRenderer lineRenderer; // LineRenderer���

    void Start()
    {
        // ����LineRenderer����ɫΪ��ɫ
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        ShootLaserFromTargetPosition(transform.position, -transform.up, laserLength);
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
