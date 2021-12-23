using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
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
    const float maxHealth = 100f;
    public float currentHealth = maxHealth;

    public Image healthBarImage;
    public GameObject ui;

    public string myTeam;
    PlayerManager playerManager;


    void Awake(){
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    void Start()
    {
        if(!PV.IsMine){
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Debug.Log("done");
        }
        healthBarImage = GameObject.Find("HealthBar").GetComponent<Image>();
        ui = GameObject.Find("Canvas (1)").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            bool pause = Input.GetKeyDown(KeyCode.Escape);

            //pause
            if(pause){
                GameObject.Find("Pause").GetComponent<Pause>().TogglePause();
            }

            if(Pause.paused){
                x = 0f;
                z = 0f;
            }

            //movement + jump
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

    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage){
        if(!PV.IsMine)
            return;
        
        currentHealth -= damage;
        healthBarImage.fillAmount = currentHealth / maxHealth;
        if(currentHealth <= 0){
            Die();
            healthBarImage.fillAmount = 1.00f;
        }
    }

    void Die(){
        
        playerManager.Die();
    }
}



