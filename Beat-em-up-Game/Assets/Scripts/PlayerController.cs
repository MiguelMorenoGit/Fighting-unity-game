using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    //Variables movimiento
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private Vector3 direction;
    public float gravity = 9.8f;
    public float fallVelocity;
    //Variables Animacion
    public Animator playerAnimatorController;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        // player.Move(new Vector3(horizontalMove,0,verticalMove) * playerSpeed * Time.deltaTime);

        playerInput = new Vector3(horizontalMove,0,verticalMove); // los almacenamos en Vector3
        playerInput = Vector3.ClampMagnitude(playerInput, 1); // Y limitamos su magnitud a 1 para evitar acelerones en movimientos diagonales

        playerInput = playerInput * playerSpeed;
        //Decimos en que direccion mirara el personaje
        //direction = player.transform.position + new Vector3(0,0, horizontalMove); //classic 2d movement
        direction = player.transform.position + new Vector3(-verticalMove,0, horizontalMove);  // Classic 3d movement
        player.transform.LookAt(direction);

        //Iniciamos Gravedad
        SetGravity();

        // PLayerSkills();

        player.Move(playerInput * Time.deltaTime); // iniciamos el movimiento del player

        Debug.Log(player.velocity.magnitude);
        Debug.Log(direction);

        // playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.velocity.magnitud * playerSpeed);
 
    }

    //Funcion para la Gravedad
    public void SetGravity() {

        playerInput.y = -gravity * Time.deltaTime ;
        //Si estamos tocando el suelo - isGrounded es una funcion integrada en el controller
        if (player.isGrounded){

            fallVelocity = -gravity * Time.deltaTime;
            playerInput.y = fallVelocity;

        } else {

            fallVelocity -= gravity * Time.deltaTime;
            playerInput.y = fallVelocity;
            // playerAnimatorController.SetFloat("PlayerVerticalVelocity", player.velocity.y);
        }
        // playerAnimationController.SetBool("IsGrounded", player.isGrounded);
    }

    //Funcion para las habilidades de nuestro jugador
    public void PLayerSkills() {

        //Si estamos tocando el suelo y pulsamos el botn "Jump"
        if (player.isGrounded && Input.GetButtonDown("Jump")) {

            playerAnimatorController.SetTrigger("PlayerJump");
        }
    }

}
