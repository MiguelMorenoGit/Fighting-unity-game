using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // kakakakakaka2
    //kakakakakkaaaaaaaaaaaaaaaaa
    //Variables movimiento
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private Vector3 movePlayer;
    private Vector3 direction;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    //Variables Animacion
    public Animator playerAnimatorController;

    //Variables camara
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    //Variables fisicas - TODO solucionar el tema pendientes y activar
    // public bool isOnSlope = false;
    // private Vector3 hitNormal;
    // public float slideVelocity;
    // public float slopeForceDown;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove,0,verticalMove); // los almacenamos en Vector3
        playerInput = Vector3.ClampMagnitude(playerInput, 1); // Y limitamos su magnitud a 1 para evitar acelerones en movimientos diagonales

        //Decimos en que direccion mirara el personaje
        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = playerInput * playerSpeed;
        player.transform.LookAt(player.transform.position + movePlayer);

        //classic 2d movement 
        //direction = player.transform.position + new Vector3(0,0, horizontalMove); //classic 2d movement
        //player.transform.LookAt(direction);

        //Iniciamos Gravedad
        SetGravity();
        //Iniciamos las skills
        PLayerSkills();



        player.Move(movePlayer * Time.deltaTime); // iniciamos el movimiento del player

        Debug.Log(player.velocity.magnitude);
        Debug.Log(direction);

        // playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.velocity.magnitud * playerSpeed);
 
    }

    // Funcion camara
    void CamDirection() {

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //Funcion para las habilidades de nuestro jugador
    public void PLayerSkills() {

        //Si estamos tocando el suelo y pulsamos el boton "Jump"
        if (player.isGrounded && Input.GetButtonDown("Jump")) {

            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;

            playerAnimatorController.SetTrigger("PlayerJump");
        }
    }

    //Funcion para la Gravedad
    public void SetGravity() {

        movePlayer.y = -gravity * Time.deltaTime ;
        //Si estamos tocando el suelo - isGrounded es una funcion integrada en el controller
        if (player.isGrounded){

            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;

        } else {

            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            // playerAnimatorController.SetFloat("PlayerVerticalVelocity", player.velocity.y);
        }

        // SlideDown(); T0DO hacer que funcione bien cuando estas frente a un angulo recto(pared,escalones etc)
        // playerAnimationController.SetBool("IsGrounded", player.isGrounded);
    }

    // public void SlideDown() { 
    //     // angulo >= angulo maximo del characterController
    //     isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit && Vector3.Angle(Vector3.up, hitNormal) < 89f;

    //     if (isOnSlope) {
    //         movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
    //         movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

    //         movePlayer.y += slopeForceDown;
    //     }
    // }

    // private void OnControllerColliderHit(ControllerColliderHit hit) {
    //     hitNormal = hit.normal;
    // }

}
