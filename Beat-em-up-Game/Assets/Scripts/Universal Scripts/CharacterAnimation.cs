﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    private Animator anim;

    void Awake() {

        anim = GetComponent<Animator>();
        
    }

    // movements
    public void Walk(bool move) {
        anim.SetBool(AnimationTags.MOVEMENT, move);
    }
    public void Walk_Guard(bool move) {
        anim.SetBool(AnimationTags.MOVEMENT_GUARD, move);
    }

    // Punchs
    public void Punch_1() {
        anim.SetTrigger(AnimationTags.PUNCH_1_TRIGGER);
    }
    
    public void Punch_2() {
        anim.SetTrigger(AnimationTags.PUNCH_2_TRIGGER);
    }
    
    public void Punch_3() {
        anim.SetTrigger(AnimationTags.PUNCH_3_TRIGGER);
    }

    // Kicks
    public void Kick_1() {
        anim.SetTrigger(AnimationTags.KICK_1_TRIGGER);
    }
    public void Kick_2() {
        anim.SetTrigger(AnimationTags.KICK_2_TRIGGER);
    }
    public void Kick_Jump() {
        anim.SetTrigger(AnimationTags.KICK_JUMP_TRIGGER);
    }

    //  ENEMY ANIMATIONS

    public void EnemyAttack(int attack) {
        if(attack == 0) {
            anim.SetTrigger(AnimationTags.ATTACK_1_TRIGGER);
        }
        if(attack == 1) {
            anim.SetTrigger(AnimationTags.ATTACK_2_TRIGGER);
        }
        if(attack == 2) {
            anim.SetTrigger(AnimationTags.ATTACK_3_TRIGGER);
        }
    } // enemy attack

    public void Play_IdleAnimation() {
        anim.Play(AnimationTags.IDLE_ANIMATION);
    }

    public void KnockDown() {
        anim.SetTrigger(AnimationTags.KNOCK_DOWN_TRIGGER);
    }
    public void StandUp() {
        anim.SetTrigger(AnimationTags.STAND_UP_TRIGGER);
    }

    public void HeadHit() {
        anim.SetTrigger(AnimationTags.HEAD_HIT_TRIGGER);
    }
    public void MediumHit() {
        anim.SetTrigger(AnimationTags.MEDIUM_HIT_TRIGGER);
    }
    public void Death() {
        anim.SetTrigger(AnimationTags.DEATH_TRIGGER);
    }
    
    
}
