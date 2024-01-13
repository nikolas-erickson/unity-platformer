using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class playerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private float horizontalInput;
    private float wallJumpCooldown;
    private Rigidbody2D body;
    private BoxCollider2D boxColl2d;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = 3f;
        jumpPower = 7f;
        //get references to components of player gameobject
        body = gameObject.GetComponent<Rigidbody2D>();
        boxColl2d = gameObject.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        //flip image of player sprite
        if(horizontalInput > .01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -.01f)
            transform.localScale = new Vector3(-1,1,1);

        anim.SetBool("groundedBool", isGrounded());
        anim.SetBool("runBool", horizontalInput != 0);

        if(wallJumpCooldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 2.5f;

            if(Input.GetKeyDown(KeyCode.W))
                jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
        
    }

    private void jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jumpTrigger");
        }
        else if (onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3.5f, 0);
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*1.5f, 3);
            }
            wallJumpCooldown = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return rayCastHit.collider != null;
    }
}
