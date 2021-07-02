using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Prince prince;
    LevelController levelController;
    Vector2 newDir;
    bool freeze = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        prince = FindObjectOfType<Prince>();
        levelController = FindObjectOfType<LevelController>();
        levelController.EnemySpawned();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
