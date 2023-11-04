using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBound : MonoBehaviour
{
    public static TargetBound instance;
    [SerializeField] BoxCollider col;

    void Awake()
    {
        instance = this;
    }
    public Vector3 GetRandomPosition()
    {
        Vector3 center = col.center + this.transform.position;

        float minX = center.x - col.size.x / 2f;
        float maxX = center.x + col.size.x / 2f;
        float randomX = Random.Range(minX, maxX);
        float minY = center.y - col.size.y / 2f;
        float maxY = center.y + col.size.y / 2f;
        float randomY = Random.Range(minY, maxY);
        float minZ = center.z - col.size.z / 2f;
        float maxZ = center.z + col.size.z / 2f;
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randonpos = new Vector3(randomX, randomY, randomZ);
        return randonpos;

    }
}
