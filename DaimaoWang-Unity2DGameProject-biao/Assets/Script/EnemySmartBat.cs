using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmartBat : Enemy
{
    public float speed;
    public float radius;


    private Transform playerTransform;
    private bool isRun;
    // Start is called before the first frame update
    public void Start()
    {
        isRun = false;
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {

        base.Update();
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;


            if (distance < radius || isRun)
            {
                isRun = true;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }

        }
    }
}
