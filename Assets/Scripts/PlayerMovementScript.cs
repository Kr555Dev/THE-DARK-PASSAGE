using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public float Speed;
    public float MouseSensitivity;
    public Transform player;
    float roationX;
    public CharacterController controller;
    Vector3 velocity;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundClearance = 0.5f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpDistance = 1f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        if (Input.GetKeyDown("w")  || Input.GetKeyDown("s"))
        {
            FindObjectOfType<Audiomanager>().Play("FootSteps");
        }

        if (Input.GetKeyDown("space"))
        {

            FindObjectOfType<Audiomanager>().Play("Jump");
        }

        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement * Speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundClearance, groundMask);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpDistance * -2  * gravity);
        }
        
        //if (input.getkey("w"))
        //{
        //    transform.position += transform.forward * forwardspeed * time.deltatime;
        //}

        float mousex = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        player.Rotate(Vector3.up * mousex);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown("r"))
        {
            Restart();
        }

        if(transform.position.y < -200f)
        {
            Restart();

        }

    }

    private void OnCollisionEnter(Collision collided)
    {
        //if(collided.collider.tag == "endLevel")
        //{
        //    Debug.Log("complete");
        //    Invoke("Nextlevel", 1f);

        //    return;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "endLevel")
        {
            Debug.Log("complete");
            Invoke("Nextlevel", 1f);

            return;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void Nextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
