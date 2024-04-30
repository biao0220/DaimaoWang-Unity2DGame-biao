using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{
    private bool isPlayerInDoor;
    public static bool eButton = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || eButton) && isPlayerInDoor)
        {


            PlayerPrefs.SetInt("coinNumber", CoinUI.CurrentCoinQuantity);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                eButton = false;
                SceneManager.LoadScene(2);


            }

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                eButton = false;
                SceneManager.LoadScene(1);

            }

        }
    }

    public void EButton()
    {
        eButton = true;
        Invoke("EButtonfalse", 0.5f);
        //eButton = false;
    }

    void EButtonfalse()
    {
        eButton = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player")
        && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            //Debug.Log("进来了");
            isPlayerInDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
        && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            //  Debug.Log("进入");
            isPlayerInDoor = false;

        }
    }



}
