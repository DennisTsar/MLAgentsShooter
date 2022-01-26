using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EnemyController : Agent
{
    public GameObject player;
    public GameObject wallLeft, wallRight, wallTop, wallBottom;
    private int count;
    private int testing;
    public override void OnActionReceived(float[] vectorAction)
    {
        Debug.Log("On Action Received");
        //transform.Translate(new Vector2(vectorAction[0], vectorAction[1]) * Time.deltaTime, Space.World);
        Debug.Log(vectorAction[0]);
        //
        //if ((int)vectorAction[3] == 0)
        //Shoot();
        transform.position += new Vector3(vectorAction[0] * Time.deltaTime, 0, 0);    
        transform.position += new Vector3(0, vectorAction[1] * Time.deltaTime, 0);
        transform.Rotate(new Vector3(0, 0, vectorAction[2]));
        //Debug.Log("Number: " + vectorAction[i] + " Position " + i);
        count--;
        if (count < 1)
        {
            SetReward(-1);
            Debug.Log("e");
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
            SetReward(1 + count / 1000f);
            //Debug.Log("Episode End");

        }
        if (collision.gameObject.tag == "Enemy")
            return;
        EndEpisode();
    }
    public override void OnEpisodeBegin()
    {
        //Debug.Log("123");
        //Debug.Log("Episode Begin");
        count = 2000;
        //transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localPosition = new Vector3(Random.Range(-6, 6), Random.Range(-6, 6), 0);
    }
}