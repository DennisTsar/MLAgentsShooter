using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EnemyController2 : Agent
{
    public GameObject player;
    private int count;
    private int testing;    
    public GameObject bullet;
    private float shootTimer = 0f;
    public bool success = false;
    private float rotation = 0;
    public GameObject wallLeft, wallRight, wallTop, wallBottom;


    public override void OnActionReceived(float[] vectorAction)
    {
        //transform.Translate(new Vector2(vectorAction[0], vectorAction[1]) * Time.deltaTime, Space.World);
        //Debug.Log(vectorAction[0]);
        //
        //if ((int)vectorAction[3] == 0)
        //Shoot();

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        transform.position += new Vector3(vectorAction[0] * Time.deltaTime, 0, 0);
        transform.position += new Vector3(0, vectorAction[1] * Time.deltaTime, 0);

        //Debug.Log("Action 3: " + vectorAction[3]);
        if (vectorAction[3] > 0 && shootTimer < 0)
        {
            shootTimer = 0.5f;
            Instantiate(bullet, (Vector2)transform.position + (Vector2)(transform.rotation * (new Vector2(1.09f, 0))), transform.rotation *= Quaternion.Euler(0, 0, -90));
        }
        shootTimer -= Time.deltaTime;

        rotation += vectorAction[2];

        transform.eulerAngles = new Vector3(0, 0, rotation);
        //transform.Rotate(new Vector3(0, 0, vectorAction[2]));
        //Debug.Log("Number: " + vectorAction[i] + " Position " + i);
        count--;
        if (count < 1)
        {
            SetReward(0);
            //Debug.Log("e"); 
            EndEpisode();
        }
        if(success)
        {
            //Debug.Log("Success!");
            SetReward(2);
            EndEpisode();
        }
        
        //Test 123 hehe hahah
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(player.transform.localPosition.x);
        sensor.AddObservation(player.transform.localPosition.y);
        sensor.AddObservation(transform.localRotation.z);
        sensor.AddObservation(wallBottom.transform.position);
        sensor.AddObservation(wallRight.transform.position);
        sensor.AddObservation(wallTop.transform.position);
        sensor.AddObservation(wallLeft.transform.position);
        //sensor.AddObservation(player.transform.localRotation.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            SetReward(-2);
            player.GetComponent<EnemyController2>().success = true;
            EndEpisode();
            //Debug.Log("Episode End");

        }
        //if (collision.gameObject.tag == "Enemy")
        //    return;
        //EndEpisode();
    }
    public override void OnEpisodeBegin()
    {
        //Time.timeScale = 0.1f;
        //Debug.Log("123");
        //Debug.Log("Episode Begin");
        count = 20000;
        success = false;
        //transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localPosition = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }
}