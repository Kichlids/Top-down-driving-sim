using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Constant Variables

    // Min and max speeds
    private const float MIN_SPEED = 0f;                 // Minimum speed of the car
    private const float MAX_SPEED = 120f;               // Maximum speed of the car   

    // Initial physics variables
    private const float INITIAL_MOVE_FORCE = 0f;
    private const float INITIAL_ROTATIONAL_FORCE = 15f;
    private const float INITIAL_ACCELERATION = 60f;
    private const float INITIAL_MAX_VELOCITY = 5f;
    private const float VELOCITY_INCREMENTS = 5F;

    #endregion

    private Rigidbody rb;                               // Rigidbody of the car

    private float moveForce;                            // Forward force exerting on the car
    private float rotForce;                             // Rotational force exerting on the car
    public float maxAbsMoveForce;                       // Maximum forward force that can be exerted on the car.

    private float acceleration;                         // Acceleration of the car in force

    public Light[] headlights;                          // Light sources of the car    
    private float headlightIntensity = 3.15f;            // Light intensity for the light sources
    
    private void Start()
    {
        // Get all necessary components
        rb = GetComponent<Rigidbody>();

        // Assign all initial values
        moveForce = INITIAL_MOVE_FORCE;
        rotForce = INITIAL_ROTATIONAL_FORCE;
        acceleration = INITIAL_ACCELERATION;

        SetMaxVelocity(INITIAL_MAX_VELOCITY);
    }

    private void Update()
    {
        // Change max velocity
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            ChangeVelocity(VELOCITY_INCREMENTS);
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            ChangeVelocity(-VELOCITY_INCREMENTS);

        // Turn on/off headlights
        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (Light light in headlights)
            {
                if (light.intensity == 0)
                    light.intensity = headlightIntensity;
                else
                    light.intensity = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        // Exert forward force
        if (Input.GetKey(KeyCode.W))
        {
            // Exert rotational force
            if (Input.GetKey(KeyCode.A))
                rb.AddTorque(rotForce * -transform.up);
            if (Input.GetKey(KeyCode.D))
                rb.AddTorque(rotForce * transform.up);

            // Accelerate forward force with a cap
            if (moveForce < maxAbsMoveForce)
                moveForce = moveForce + acceleration * Time.fixedDeltaTime;
            else
                moveForce = maxAbsMoveForce;
        }
        // Exert -forward force
        else if (Input.GetKey(KeyCode.S))
        {
            // Exert rotational force
            if (Input.GetKey(KeyCode.A))
                rb.AddTorque(rotForce * -transform.up);
            if (Input.GetKey(KeyCode.D))
                rb.AddTorque(rotForce * transform.up);

            // Accelerate forward force with a cap
            if (moveForce > -maxAbsMoveForce)
                moveForce = moveForce - acceleration * Time.fixedDeltaTime;
            else
                moveForce = -maxAbsMoveForce;
        }
        // Decrement force to 0
        else
        {
            if (moveForce > 3)
                moveForce = moveForce - acceleration * Time.deltaTime;
            else if (moveForce < -3)
                moveForce = moveForce + acceleration * Time.deltaTime;
            else
                moveForce = 0;
        }

        rb.AddForce(moveForce * transform.forward);
    }


    // Return the magnitude of the rigidbody's velocity
    public float GetCurrentVelocity()
    {
        return rb.velocity.magnitude;
    }

    // Formula derived from rigidbody mass=1, drag=2, angular drag=2
    public float GetMaxVelocity()
    {
        float maxVel = (float)System.Math.Round(0.4784f * maxAbsMoveForce - 5.7059f);
        return maxVel > 0 ? maxVel : 0;
    }

    // et max velocity
    private void SetMaxVelocity(float max)
    {
        maxAbsMoveForce = (max + 5.7059f) / 0.4784f;
    }

    // Increment or decrement velocity
    private void ChangeVelocity(float change)
    {
        float newVel = GetMaxVelocity() + change;

        if (newVel < MIN_SPEED)
            newVel = MIN_SPEED;
        else if (newVel > MAX_SPEED)
            newVel = MAX_SPEED;

        SetMaxVelocity(newVel);
    }
}
