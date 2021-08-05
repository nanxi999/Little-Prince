using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    [SerializeField] private string[] layers;
    [SerializeField] AudioClip explosionSFX;
    
    private AudioSource audioSource;
    protected List<int> layerIndexes;
    private CircleCollider2D circle;
    private int dmg = 10;
    private bool ice;
    private GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.5f);
        circle = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        InitIndexList();
        Explode();
        if(audioSource && explosionSFX)
        {
            audioSource.PlayOneShot(explosionSFX);
        } else
        {
            Debug.Log("audiosource or audio not properly set for explosion");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!circle)
        {
            Start();
        }

        if (audioSource)
        {
            if(audioSource.time > 0.8)
            {
                audioSource.Stop();
            }
        }
    }

    public void IfIcyAttack(bool val)
    {
        ice = val;
    }

    public void InitIndexList()
    {
        layerIndexes = new List<int>();
        foreach (string layerName in layers)
        {
            int index = LayerMask.NameToLayer(layerName);
            layerIndexes.Add(index);
        }
    }

    public void SetDmg(int damage)
    {
        dmg = damage;
    }

    public void SetShooter(GameObject obj)
    {
        shooter = obj;
    }

    private void Explode()
    {
        List<Collider2D> colliderList = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask(layers));
        circle.OverlapCollider(filter, colliderList);
        foreach (Collider2D collider in colliderList)
        {
            if(collider.gameObject == shooter) { continue; }

            Hurtable enemy = collider.gameObject.GetComponent<Hurtable>();
            if (enemy)
            {
                enemy.Hurt(dmg);
            }

            Enemy enemyComp = collider.gameObject.GetComponent<Enemy>();
            if(enemyComp)
            {
                enemyComp.PushBack(Vector3.zero, 0.5f);
                if(ice)
                {
                    enemyComp.IceAttackHit(4, 0.6f);
                }
            }
        }
    }
}
