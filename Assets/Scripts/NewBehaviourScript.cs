using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public float movement_speed = 1;

    Vector2 movement_direction = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement_direction.Normalize();

        transform.position += new Vector3(movement_direction.x, movement_direction.y, 0) * Time.deltaTime * movement_speed;
        
        if (movement_direction.x < 0)
        {
            sprite.flipX = true;
        } else if (movement_direction.x > 0)
        {
            sprite.flipX = false;
        }


        if (movement_direction.magnitude > 0)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }
}
