using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public float movement_speed = 1;
    public float sneaking_speed = .25f;

    Vector2 movement_direction = new Vector2(0, 0);
    private bool sneaking = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement_direction.Normalize();
        float speed;
        if (!sneaking)
        {
            speed = movement_speed;
        } else
        {
            speed = sneaking_speed;
        }
        transform.position += new Vector3(movement_direction.x, movement_direction.y, 0) * Time.deltaTime * speed;
        
        if (movement_direction.x < 0)
        {
            sprite.flipX = true;
        } else if (movement_direction.x > 0)
        {
            sprite.flipX = false;
        }


        if (movement_direction.magnitude > 0)
        {
            if (!sneaking)
            {
                animator.SetBool("running", true);
                animator.SetBool("sneaking", false);
            } else
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
            } else
            {
                animator.SetBool("running", false);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("attack");
            Debug.Log("attack trigger");
        }

        if (Input.GetButtonDown("Sneak"))
        {
            sneaking = !sneaking;
        }
    }
}
