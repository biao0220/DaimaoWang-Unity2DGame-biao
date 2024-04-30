using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector2 startSpeed;
    public float destroyBombTime;
    public float hitBoxTime;
    public GameObject explosionRange;

    private Rigidbody2D rb2d;
    private Animator anim;

    public float delayExplodeTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2d.velocity = transform.right * startSpeed.x + transform.up * startSpeed.y;

        Invoke("Explode", delayExplodeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenExplosionRange()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }

    void Explode()
    {
        anim.SetTrigger("Explode");
        Invoke("DestroyThisBomb", destroyBombTime);
        Invoke("GenExplosionRange", hitBoxTime);
    }

    void DestroyThisBomb()
    {
        Destroy(gameObject);
    }
}
