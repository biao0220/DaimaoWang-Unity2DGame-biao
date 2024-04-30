using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float startTime;
    public float time;
    private Animator anim;
    private PolygonCollider2D collider2D;
    private bool attackButton = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive)
        {
            Attack();
        }

    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack") || attackButton)
        {

            anim.SetTrigger("Attack");
            SoundManager.PlayAttackClip();
            StartCoroutine(StartAttack());

        }
        attackButton = false;
    }

    public void AttackButton()
    {
        attackButton = true;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
