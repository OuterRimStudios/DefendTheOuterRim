using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void IsIdle(bool isIdle)
    {
        anim.SetBool("IsIdle", isIdle);
    }

    public void PlayerMovement(float horizontal, float vertical)
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
    }

    public void PlayerAcceleration(bool accelerating)
    {
        anim.SetBool("Accelerating", accelerating);
    }

    public void Fire()
    {
        anim.SetTrigger("Fire");
    }

    public void IsDead(bool isDead)
    {
        anim.SetBool("IsDead", isDead);
    }
}
