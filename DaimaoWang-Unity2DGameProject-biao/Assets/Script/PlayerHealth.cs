using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    private Renderer myRender;
    public int blinks;
    public float time;
    private Animator anim;
    public float dieTime;

    private ScreenFlash sf;

    private PolygonCollider2D polygonCollider2D;

    public float hitBoxCdTime;

    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        sf = GetComponent<ScreenFlash>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }

    public void DamegePlayer(int damage)
    {
        health -= damage;
        SoundManager.PlayHitClip();
        if (health < 0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent = health;

        if (health <= 0)
        {
            GameController.isGameAlive = false;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);
            // Destroy(gameObject);
        }

        BlinkPlayer(blinks, time);
        sf.FlashScreen();

        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        polygonCollider2D.enabled = true;
    }

    void KillPlayer()
    {
        //Destroy(gameObject);
        SceneManager.LoadScene(1);
        GameController.isGameAlive = true;
    }
}
