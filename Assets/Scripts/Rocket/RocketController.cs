using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketController : MonoBehaviour {
    [Header("Components")] 
    public Rigidbody2D rb;
    public LineRenderer propellant;

    [Header("Rocket Properties")]
    public float propellantForce;
    public float rotateSpeed;

    public Vector2 startVelocity;

    private void Start() {
        rb.velocity = startVelocity;
    }

    void Update() {
        //shouldPropel = Input.GetButton("Jump");
        if (Input.GetButton("Jump")) {
            if (!propellant.enabled) propellant.enabled = true;
            
            rb.AddRelativeForce(Vector2.up * propellantForce * Time.deltaTime);
            propellant.SetPosition(1, new Vector3(0.625f, 0.5f + Random.Range(-0.2f, 0.2f), 0));
        }
        else {
            propellant.enabled = false;
        }
        //
        // if (rb.velocity.magnitude > maxSpeed) {
        //     rb.AddRelativeForce(-rb.velocity.normalized * velocityPushbackForce * Time.deltaTime);
        //     Debug.Log("!!! " + (-rb.velocity.normalized * velocityPushbackForce) + " !!!");
        // }

        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, 0, -horizontal * rotateSpeed * Time.deltaTime));
        
        Debug.Log("H: " + rb.velocity.x + " V: " + rb.velocity.y);
    }
}