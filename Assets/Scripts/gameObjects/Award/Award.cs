using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Award : MonoBehaviour
{
    public Transform description;
    public float weight;
    public int firstSpawnLevel;
    public GameObject collectEffect;
    Animator animator;
    LevelController levelConroller;
    bool isCollected = false;
    List<Prince> princesCollided;
    GameObject effect;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        levelConroller = FindObjectOfType<LevelController>();
        princesCollided = new List<Prince>();
    }

    protected void Update()
    {
        CheckCollidedPrinces();
    }

    private void CheckCollidedPrinces()
    {
        foreach (Prince prince in princesCollided)
        {
            if (prince.saveKeyPressed && !prince.GetPrinceToSave() && !isCollected)
            {
                isCollected = true;
                GiveAwards(prince.gameObject);
                //levelConroller.PlayerAwarded(prince.GetID());
                effect = Instantiate(collectEffect, transform.Find("Renderer").position, transform.rotation);
                Destroy(effect, 1.5f);
            }
        }
        if (isCollected)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince)
        {
            princesCollided.Add(prince);
            AwardSetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince)
        {
            princesCollided.Remove(prince);
            if(princesCollided.Count == 0)
                AwardSetActive(false);
        }
    }

    private void AwardSetActive(bool stat)
    {
        animator.SetBool("Bounce", stat);
        description.gameObject.SetActive(stat);
    }

    public float GetWeight()
    {
        return weight;
    }

    public int GetFirstSpawnLevel()
    {
        return firstSpawnLevel;
    }

    protected virtual void GiveAwards(GameObject prince) { }
}
