using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour  //
{
    public GameObject bloodEffect;
    public int health;
    public float flashTime;
    public int damage;
    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth playerHealth;
    public GameObject dropCoin;
    public GameObject floatPoint;

    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(dropCoin, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamegePlayer(damage);
            }
        }

    }

    public void TakeDamage(int damage)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        health -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);

        GameController.canShake.Shake();
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);  //延迟时间；
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
}
