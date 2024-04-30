using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public GameObject bomb;
    private bool throwBombButton = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || throwBombButton)
        {
            Instantiate(bomb, transform.position, transform.rotation);
            throwBombButton = false;
        }
    }

    public void ThrowBombButton()
    {
        throwBombButton = true;
    }
}
