using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizardDash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if(prince)
        {
            prince.Hurt(GetComponentInParent<ArchWizard>().dashDamage);
        }
    }
}
