using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Transform backDoor;

    private bool isDoor;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || DoorToNextLevel.eButton) && isDoor)
        {
            DoorToNextLevel.eButton = false;
            playerTransform.position = backDoor.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() ==
        "UnityEngine.CapsuleCollider2D")
        {
            isDoor = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() ==
        "UnityEngine.CapsuleCollider2D")
        {
            isDoor = false;
        }
    }
}
