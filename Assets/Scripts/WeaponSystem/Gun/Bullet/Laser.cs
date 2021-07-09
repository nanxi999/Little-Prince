using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    public string[] layerNames;

    private LayerMask mask;
    Transform transform;
    [SerializeField] private Vector2 dir;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
        mask = LayerMask.GetMask(layerNames);
    }

    private void Update()
    {
        ShootLaser();
        LaserRotate();
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, dir, defDistanceRay, mask);
        if (hit) { 
            DrawRay(laserFirePoint.position, hit.point);
        } else
        {
            DrawRay(laserFirePoint.position, dir * defDistanceRay);
        }
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    public void SetDir(Vector2 newDir)
    {
        dir = newDir;
    }

    void LaserRotate()
    {
        Vector2 angle1 = Vector2.up;
        
        float angle = Vector2.SignedAngle(dir, angle1);
        angle = angle + 30 * Time.deltaTime;
        dir = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
        Debug.Log(dir + " " + angle);
    }
}
