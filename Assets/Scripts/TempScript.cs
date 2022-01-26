using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class TempScript : Agent
{
    //public Transform food;
    private int count;

    public override void OnEpisodeBegin()
    {
        //Debug.Log(food.GetComponent<Rigidbody>().velocity);
        //food.GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //food.GetComponent<Rigidbody>().velocity = Vector3.zero;
        count = 1200;
        float f1 = Random.Range(-10f, 10f);
        float f2 = Random.Range(-10f, 10f);
        float f3 = Random.Range(-2f, 2f);
        float f4 = Random.Range(-2f, 2f);
        Vector3 foodPos = new Vector3(f1, 0.5f, f2);
        Vector3 mePos = new Vector3(f3, 0.5f, f4);
        while (Vector3.Distance(foodPos, mePos) < 1.5f)
        {
            f1 = Random.Range(-3f, 3f);
            f2 = Random.Range(-3f, 3f);
            foodPos = new Vector3(f1, 0.5f, f2);
        }
        //Debug.Log("Distance: " + Vector3.Distance(foodPos, mePos));
        //food.localPosition = foodPos;
        transform.localPosition = mePos;
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        //food.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
    public override void OnActionReceived(float[] vectorAction)
    {
        //Debug.Log(food.GetComponent<Rigidbody>().velocity);
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        //food.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Debug.Log(food.GetComponent<Rigidbody>().velocity);
        count--;
        if (count <= 0)
        {
            SetReward(-1);
            EndEpisode();
        }
        for (int i = 0; i < vectorAction.Length; i++)
        {
            if (i == 0)
                transform.position += new Vector3(vectorAction[i] * Time.deltaTime, 0, 0);
            else
                transform.position += new Vector3(0, 0, vectorAction[i] * Time.deltaTime);
            //Debug.Log("Number: " + vectorAction[i] + " Position " + i);
        }
        //Debug.Log(GetComponent<Rigidbody>().velocity);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        //sensor.AddObservation(food.transform.localPosition);
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        SetReward(1 + count / 1200f);
        EndEpisode();
    }*/
}
