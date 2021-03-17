using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public float movement_speed = 1;
    public float sneaking_speed = .25f;

    private bool sneaking = false;
    
    private Vector2 moveInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if (!sneaking)
        {
            speed = movement_speed;
        }
        else
        {
            speed = sneaking_speed;
        }
        transform.position += new Vector3(moveInput.x, moveInput.y, 0) * Time.deltaTime * speed;

        if (moveInput.x < 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            sprite.flipX = false;
        }


        if (moveInput.magnitude > 0)
        {
            if (!sneaking)
            {
                animator.SetBool("running", true);
                animator.SetBool("sneaking", false);
            }
            else
            {
                animator.SetBool("running", false);
                animator.SetBool("sneaking", true);
            }
        }
        else
        {
            if (sneaking)
            {
                animator.SetBool("sneaking", false);
            }
            else
            {
                animator.SetBool("running", false);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        //Debug.Log(moveInput);
    }
    public void OnSneak(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sneaking = !sneaking;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("attack");
        }
    }
}
