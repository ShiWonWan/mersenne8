using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    // Movement values
    public float movementSpeed = 1f;
    public float jumpForce = 5f;
    public float gravityScale = 5f;
    private bool dobleSalto = true;
    public float rotateSpeed;

    // Damage anim values
    public bool isKnocking = false;
    public float knockbackLenght = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    // Movement controll
    private Vector3 moveDirection;
    public CharacterController charController;

    // Extra stuff (camera, model, animations)
    private Camera theCam;
    public GameObject playerModel;
    public Animator animator;
    public GameObject[] playerPieces;

    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the camera as the main on the scene
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    // Controll all the player movement
    // @param none
    // @return void
    private void PlayerMovement()
    {
        if (!isKnocking)
        {
            // Store Y value
            float yStore = moveDirection.y;

            // Set move direction with the axix forces
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * movementSpeed;
            moveDirection.y = yStore;

            // Leave the player jump if it's on the floor
            if (charController.isGrounded && Input.GetButtonDown("Jump"))
            {
                moveDirection.y = 0f;

                moveDirection.y = jumpForce;
            }
            else if (!charController.isGrounded && dobleSalto)
            {
                // Do double jump
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                    dobleSalto = false;
                    animator.SetBool("doubleJump", true);
                }
            }

            // Set default gounded vals
            if (charController.isGrounded)
            {
                dobleSalto = true;
                animator.SetBool("doubleJump", false);
            }

            // Set Y value with gravity implemented
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            // Move the player with the previous calculated values
            charController.Move(moveDirection * Time.deltaTime);

            // Rotation player
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }
        else
        {
            knockbackCounter -= Time.deltaTime;

            // Store Y value
            float yStore = moveDirection.y;

            // Set move direction with the knockBackPower force
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            
            // Move Player
            charController.Move(moveDirection * Time.deltaTime);
            animator.SetBool("Knocking", true);

            // Stop gravity force increase
            if (charController.isGrounded) moveDirection.y = 0f;

            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        // Animation parameters values
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        animator.SetBool("Grounded", charController.isGrounded);
        animator.SetBool("Knocking", false);
    }

    // Make player knock back (push back)
    // Designed to be used when receiving damage
    // @param none
    // @return void
    public void KnockBack()
    {
        isKnocking = true;
        knockbackCounter = knockbackLenght;
        moveDirection.y = knockbackPower.y;
    }

    // Move player to specif. position
    // @param Vector3
    // @return void
    public void movePlayerTo(Vector3 newPos)
    {
        charController.enabled = false;
        gameObject.transform.position = newPos;
        charController.enabled = true;
    }
}
