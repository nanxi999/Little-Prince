using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayerDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince)
        {
            transform.parent.GetComponent<Shadow>().PlayerInRange();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.GetComponent<Shadow>().PlayerOutOfRange();
    }
}
