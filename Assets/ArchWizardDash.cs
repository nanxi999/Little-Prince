using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizardDash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        ArchWizard aw = GetComponentInParent<ArchWizard>();
        if (prince)
        {
            Vector3 dir = prince.transform.position - aw.transform.position;
            if (dir.magnitude == 0) { return; }
            else
            {
                prince.PushBack(dir / dir.magnitude * aw.dashPushBack, 0.7f);
            }
            prince.Hurt(aw.dashDamage);
            aw.StopDash();
        }
    }
}
