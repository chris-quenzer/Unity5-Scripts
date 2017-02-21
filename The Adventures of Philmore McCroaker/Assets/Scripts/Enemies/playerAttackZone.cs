using UnityEngine;
using System.Collections;

public class playerAttackZone : MonoBehaviour {

    public bool attack;
    public float attackDelay = 0.5f;
    public float attackDuration = 10f;
    public Collider2D playerKill;

    void Start()
    {
        playerKill.enabled = false;
    }
    /*
    void Update()
    {

    }
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Frog")
        {
            attack = true;
            //StartCoroutine(Attack(attackDelay, attackDuration));
        }
        checkForAttack(attack);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Frog")
        {
            attack = true;
        }
        checkForAttack(attack);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Frog")
        {
            attack = false;
        }
        checkForAttack(attack);
    }

    public void checkForAttack(bool attackActive)
    {
        if (attackActive)
        {
            playerKill.enabled = true;
        }
        else if(!attackActive)
        {
            playerKill.enabled = false;
        }
    }

    /*IEnumerator Attack(float attackDelay, float attackDurationl)
    {
        float timer = 0;
        while(attackDelay > timer)
        {
            timer += Time.deltaTime;
        }
        attack = true;
        timer = 0;
        while (attackDuration > timer)
        {
            timer += Time.deltaTime;
        }
        attack = false;
        yield return 0;
    }*/
}
