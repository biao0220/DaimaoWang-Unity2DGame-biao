using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject sickle;
    private bool sickleButton = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) || sickleButton)
        {//Quaternion.identity
            //Instantiate(sickle, transform.position, transform.rotation);
            Instantiate(sickle, transform.position, transform.rotation);
            sickleButton = false;
        }
    }

    public void SickleButton()
    {
        sickleButton = true;
    }
}
