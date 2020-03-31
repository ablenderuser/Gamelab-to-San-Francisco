using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; 
    private Rigidbody2D rb2d;
    private bool facingRight;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here. 
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal. 
        float moveHorizontal = Input.GetAxis ("Horizontal"); 
        
        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical");
        
        //Use the two stored floats to create a new Vector2 variable movement. 
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        
        animator.SetFloat("Vitesse", 60*movement.magnitude * Time.fixedDeltaTime);
        
        if ( movement.x > 0 )
        {
			if (facingRight == false)
			{
				Flip();
			}
			animator.SetBool("Droite", true);
		}
		else
		{
			if ( movement.x < 0 )
			{
				if (facingRight == true)
				{
					Flip();
				}
				animator.SetBool("Droite", false);
			}
		}
		
		if ( movement.y > 0 )
		{
			animator.SetBool("Haut", true);
		}
		else
		{
			animator.SetBool("Haut", false);
		}
        
        //Call the translate function to move 
        rb2d.MovePosition(rb2d.position + 60*movement * Time.fixedDeltaTime );
        
	}

    private void Flip()
    {
		facingRight = !facingRight;

	// use negative scaling to reverse the sprite when the player is facing left
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
     }

}
