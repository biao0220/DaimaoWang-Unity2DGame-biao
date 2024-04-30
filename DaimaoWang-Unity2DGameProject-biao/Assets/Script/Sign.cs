using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogBoxText;
    public string signText;
    private bool isPlayerInSign;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || DoorToNextLevel.eButton) && isPlayerInSign)
        {
            dialogBoxText.text = signText;
            dialogBox.SetActive(true);
            DoorToNextLevel.eButton = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // Debug.Log("进入1");
        if (other.gameObject.CompareTag("Player")
        && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            //  Debug.Log("进入");
            isPlayerInSign = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
        && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            //  Debug.Log("进入");
            isPlayerInSign = false;
            dialogBox.SetActive(false);
        }
    }
}