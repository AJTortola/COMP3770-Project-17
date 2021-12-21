using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 15;
    private Vector3 move;
    public float gravity = -10;
    public float jumpForce = 8;
    private Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Animator animator;
    Rigidbody rb;
    PhotonView PV;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

    }

    void Start()
    {
        if(!PV.IsMine){
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Debug.Log("done");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            animator.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));

            move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);

            if (isGrounded && velocity.y < 0)
                velocity.y = -1f;

            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }
            
            controller.Move(velocity * Time.deltaTime);
            
        }
        else{
            return;
        }
        
    }

    private void Jump()
    {
        velocity.y = jumpForce;
    }
}
