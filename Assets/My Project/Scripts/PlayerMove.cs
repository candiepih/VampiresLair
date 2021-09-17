using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkRotateSpeed = 3.5f;
    [SerializeField] float runRotateSpeed = 4.5f;
    [SerializeField] float aimRotateSpeed = 5.0f;
    [SerializeField] float idleRotateSpeed = 0.5f;
    [SerializeField] ParticleSystem impactEfectPS;

    private Animator anim;
    private AnimatorStateInfo animInfo;
    private AnimatorStateInfo animInfoL2;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate called every fixedframerate frame
    void Update()
    {
        if (!SaveScript.gameOver)
        {
            PlayerLocomotion();
            PlayerRotation();
            PlayerJump();
            animInfoL2 = anim.GetCurrentAnimatorStateInfo(1);
            if (animInfoL2.IsTag("Idle"))
                anim.SetLayerWeight(1, 0);
            else if (animInfoL2.IsTag("Hit"))
                anim.SetLayerWeight(1, 1);
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("jump", true);
            rb.AddForce((Vector3.up * 2.0f), ForceMode.Impulse);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }

    private void PlayerLocomotion()
    {
        float verticalDirection = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            verticalDirection *= 2;

        anim.SetFloat("speed_vertical", verticalDirection);
    }


    private void PlayerRotation()
    {
        float horizontalDirection = Input.GetAxis("Mouse X");
        float rotateSpeed = idleRotateSpeed;
        animInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsTag("Walk"))
            rotateSpeed = walkRotateSpeed;
        else if (animInfo.IsTag("Aim"))
            rotateSpeed = aimRotateSpeed;
        else if (animInfo.IsTag("Run"))
            rotateSpeed = runRotateSpeed;

        this.transform.Rotate(Vector3.up * rotateSpeed * horizontalDirection);
    }

    IEnumerator StopBlood()
    {
        yield return new WaitForSeconds(0.3f);
        impactEfectPS.Stop();
    }

    private void Impact()
    {
        anim.SetTrigger("Hit");
        if (impactEfectPS.isPlaying)
            impactEfectPS.Stop();
        impactEfectPS.Play();
        StartCoroutine("StopBlood");
    }

    private void HitAttack()
    {
        if (AiMinion.minionType_Global == 1)
        {
            Impact();
        }
    }
}
