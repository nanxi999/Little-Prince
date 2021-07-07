using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    public LayerMask mask;
    Transform transform;
    [SerializeField] private Vector2 dir;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
        mask = LayerMask.GetMask("Friendly");
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, dir, defDistanceRay, mask);
        if (hit) { 
            DrawRay(laserFirePoint.position, hit.point);
        } else
        {
            DrawRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    void SetDir(Vector2 newDir)
    {
        dir = newDir;
    }
}
