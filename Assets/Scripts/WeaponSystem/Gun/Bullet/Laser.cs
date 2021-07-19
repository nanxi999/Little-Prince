using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float defDistanceRay = 100;
    private Transform laserFirePoint;
    public LineRenderer lineRenderer;
    public string[] layerNames;
    public GameObject hitEffectPrefab;
    private GameObject hitEffect;
    RaycastHit2D hit;

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
        GenerateHitEffect();
    }

    public void ShootLaser()
    {
        hit = Physics2D.Raycast(laserFirePoint.position, dir, defDistanceRay, mask);
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

    public void SetWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    public float GetWidth()
    {
        return lineRenderer.startWidth;
    }

    void LaserRotate()
    {
        Vector2 angle1 = Vector2.up;
        
        float angle = Vector2.SignedAngle(dir, angle1);
        angle = angle + 30 * Time.deltaTime;
        dir = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
    }

    void GenerateHitEffect()
    {
        if(!hitEffect)
        {
            hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
        } else
        {
            hitEffect.transform.position = hit.point;
        }
    }

    public void DestroyHitEffect()
    {
        if(hitEffect)
        {
            Destroy(hitEffect);
        }
    }

    public void SetFirePoint(Transform point)
    {
        laserFirePoint = point;
    }
}
