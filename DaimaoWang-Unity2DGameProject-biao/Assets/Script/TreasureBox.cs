using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject coin;
    public float delayTime;
    private bool canOpen;
    private bool isOpened;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || DoorToNextLevel.eButton)
        {
            if (canOpen && !isOpened)
            {
                anim.SetTrigger("Opening");
                isOpened = true;
                Invoke("GenCoin", delayTime);
                DoorToNextLevel.eButton = false;

            }

        }
    }

    void GenCoin()
    {
        Instantiate(coin, transform.position + new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
        && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
        && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;

        }
    }
}
