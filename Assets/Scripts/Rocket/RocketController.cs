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

        float horizontal = Input.GetAxisRaw("Horizontal");
        
        var eulerAngles = transform.eulerAngles;
        // Thanks to Seth:
        // https://stackoverflow.com/a/2321125
        var zAngleFixed = (Mathf.Atan2(Mathf.Sin(eulerAngles.z * Mathf.PI/180.0f), Mathf.Cos(eulerAngles.z * Mathf.PI/180.0f)) * eulerAngles.z / Mathf.PI) + 45.0f;
        if (zAngleFixed <= 90.0f && zAngleFixed >= -90.0f) {
            transform.Rotate(new Vector3(0, 0, -horizontal * rotateSpeed * Time.deltaTime));
        }
        else {
            if (zAngleFixed >= 90.0f)
                transform.rotation = Quaternion.Euler(0, 0, 89.5f);
            if (zAngleFixed <= -90.0f) 
                transform.rotation = Quaternion.Euler(0, 0, -89.5f);
        }
    }
}