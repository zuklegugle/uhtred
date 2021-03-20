using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public Rigidbody2D rb;

    public float movement_speed = 1;
    public float sneaking_speed = .25f;

    private bool sneaking = false;
    private float current_speed = 1f;

    private Vector2 moveInput;


    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x, moveInput.y) * current_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sneaking)
        {
            current_speed = movement_speed;
        }
        else
        {
            current_speed = sneaking_speed;
        }

        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //sprite.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            //sprite.flipX = false;
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
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
