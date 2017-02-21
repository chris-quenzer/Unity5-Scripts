using UnityEngine;
using System.Collections;

public class AttackCone : MonoBehaviour {

    public cannonAI cannonAI;
    public bool attack;

    void Awake()
    {
        cannonAI = gameObject.GetComponentInParent<cannonAI>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            attack = true;
            cannonAI.Shoot(true);
        }
        else
        {
            attack = false;
        }
    }
}
