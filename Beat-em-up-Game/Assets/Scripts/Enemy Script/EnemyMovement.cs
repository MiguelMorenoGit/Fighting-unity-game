using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //components
    private Transform playerTarget;
    private CharacterAnimation enemy_Anim;
    private Rigidbody myBody;

    // public variables
    public float speed = 5f;
    public float attack_Distance = 1f;

    // private variables
    private float chase_Player_After_Attack = 1f;
    private float current_Attack_Time;
    private float default_Attack_Time = 2f;
    private bool followPlayer;
    private bool attackPlayer;

    private void Awake() {
        enemy_Anim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();
        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        Attack();
    }

    void FollowTarget() {

        // si no queremos que persiga al player
        if (!followPlayer) {
            return;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance) {

            transform.LookAt(new Vector3(playerTarget.position.x, 0, playerTarget.position.z));
            myBody.velocity = transform.forward * speed;

            if (myBody.velocity.sqrMagnitude != 0) { // comprobamos que nos estamos moviendo
                enemy_Anim.Walk(true);
            }


        } else if ( Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance) {
            
            myBody.velocity = Vector3.zero;
            enemy_Anim.Walk(false);

            followPlayer = false;
            attackPlayer = true;
        }
    }

    void Attack() {

        if (!attackPlayer) {
            return;
        }

        current_Attack_Time += Time.deltaTime;
        transform.LookAt(new Vector3(playerTarget.position.x, 0, playerTarget.position.z));

        if (current_Attack_Time > default_Attack_Time) {
            enemy_Anim.EnemyAttack(Random.Range(0, 3)); // el 3 nunca estara incluido si tuvieramos 4 aatques deberiamos poner 5
            current_Attack_Time = 0f;
        }

        if (Vector3.Distance(transform.position, playerTarget.position) > 
                attack_Distance + chase_Player_After_Attack) {

            attackPlayer = false;
            followPlayer = true;
        }



    }
}
