using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Miguel!!!!!!

    //Variables movimiento
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;
    public CharacterController player;
    public float playerSpeed;

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

        //Decimos en que direccion mirara el personaje
        player.transform.LookAt(player.transform.position + new Vector3(0,0, horizontalMove));

        // PLayerSkills();
        // SetGravity();

        player.Move(playerInput * playerSpeed * Time.deltaTime); // iniciamos el movimiento del player

        Debug.Log(player.velocity.magnitude);

        // playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.velocity.magnitud * playerSpeed);
 
    }

    //Funcion para las habilidades de nuestro jugador
    public void PLayerSkills() {

        //Si estamos tocando el suelo y pulsamos el botn "Jump"
        if (player.isGrounded && Input.GetButtonDown("Jump")) {

            playerAnimatorController.SetTrigger("PlayerJump");
        }
    }

    //Funcion para la Gravedad
    public void SetGravity() {

        //Si estamos tocando el suelo
        if (player.isGrounded){

        } else {
            playerAnimatorController.SetFloat("PlayerVerticalVelocity", player.velocity.y);
        }
        // playerAnimationController.SetBool("IsGrounded", player.isGrounded);
    }
}
