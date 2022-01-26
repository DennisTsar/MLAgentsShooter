using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float speed = 1f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        */
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");

        if (collision.gameObject.tag == "Bullet")
        {
            health -= 1f ;
            Destroy(collision.gameObject);
        }
    }


}
