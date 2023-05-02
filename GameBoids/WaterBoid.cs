using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO:Finish
//Created by Brandon Torres 

public class WaterBoid : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float maxSteerForce = 0.1f;
    public float perceptionRadius = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Calculate the average position and velocity of all nearby water boids and fish
        Vector3 averagePosition = Vector3.zero;
        Vector3 averageVelocity = Vector3.zero;
        int boidCount = 0;
        foreach (WaterBoid boid in FindObjectsOfType<WaterBoid>())
        {
            if (boid != this && Vector3.Distance(transform.position, boid.transform.position) < perceptionRadius)
            {
                averagePosition += boid.transform.position;
                averageVelocity += boid.rb.velocity;
                boidCount++;
            }
        }
        foreach (Fish fish in FindObjectsOfType<Fish>())
        {
            if (Vector3.Distance(transform.position, fish.transform.position) < perceptionRadius)
            {
                averagePosition += fish.transform.position;
                averageVelocity += fish.rb.velocity;
                boidCount++;
            }
        }
        averagePosition /= boidCount;
        averageVelocity /= boidCount;

        // Calculate the desired velocity
        Vector3 desiredVelocity = averagePosition - transform.position + averageVelocity;

        // Steer towards the desired velocity
        Vector3 steerForce = Vector3.ClampMagnitude(desiredVelocity - rb.velocity, maxSteerForce);
        rb.AddForce(steerForce);

        // Limit the speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}

public class Fish : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float maxSteerForce = 0.1f;
    public float perceptionRadius = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Calculate the average position and velocity of all nearby fish
        Vector3 averagePosition = Vector3.zero;
        Vector3 averageVelocity = Vector3.zero;
        int fishCount = 0;
        foreach (Fish fish in FindObjectsOfType<Fish>())
        {
            if (fish != this && Vector3.Distance(transform.position, fish.transform.position) < perceptionRadius)
            {
                averagePosition += fish.transform.position;
                averageVelocity += fish.rb.velocity;
                fishCount++;
            }
        }
        averagePosition /= fishCount;
        averageVelocity /= fishCount;

        // Calculate the desired velocity
        Vector3 desiredVelocity = averagePosition - transform.position + averageVelocity;

    }
}