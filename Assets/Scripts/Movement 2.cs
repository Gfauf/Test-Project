using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement2 : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Vector2 inputVector;
    public Rigidbody rigidBody;
    public Vector3 velocity;
    public float velocityMagnitude;
    public bool canJump;
    public Collision collider;
    public int collectibles = 0;

    public TMPro.TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        rigidBody.AddForce(inputVector.x * speed, 0f, inputVector.y * speed, ForceMode.Impulse);

        velocity = rigidBody.velocity;
        velocityMagnitude = velocity.magnitude;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigidBody.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            canJump = false;
        }


    }

    private void OnCollisionEnter(Collision collider)
    {
        Debug.Log("Colisión contra:" + collider.gameObject.name);

        if (collider.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }

        if (collider.gameObject.CompareTag("Killzone"))
        {
            Debug.Log("Dead");

            SceneManager.LoadScene(0);
        }

        if (collider.gameObject.CompareTag("Goal") && collectibles >= 5)
        {
            Debug.Log("Win");

            SceneManager.LoadScene(1);
        }
        else if (collider.gameObject.CompareTag("Goal") && collectibles < 5)
        {
            SceneManager.LoadScene(0);
        }

        if (collider.gameObject.CompareTag("Item"))
        {
            Destroy(collider.gameObject);
            collectibles ++;
            scoreText.text = collectibles.ToString();
        }
    }
}
