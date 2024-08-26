using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public Vector2 inputVector;
    public Rigidbody rigidBody;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        rigidBody.AddForce(inputVector.x * speed, 0f, inputVector.y * speed, ForceMode.Impulse);

        if (Physics.Raycast(transform.position, -Vector3.up, 0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);

        }


    }
}
