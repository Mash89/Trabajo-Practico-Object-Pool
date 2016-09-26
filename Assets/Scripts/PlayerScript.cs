using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;

    private Animator playerAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

    private bool attack;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadious;

    [SerializeField]
    private LayerMask ground;

    private bool inGround;

    private bool jump;

    private bool jumpAttack;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    void Start ()
    {
        facingRight = true;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
	}

    void Update()
    {
        playerInput();
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        inGround = Grounded();
        PlayerMovement(horizontal);
        Flip(horizontal);
        PlayerAttacks();
        HandleLayers();
        ResetValues();
	}

    private void PlayerMovement(float horizontal)
    {
        if (playerRigidBody.velocity.y <0)
        {
            playerAnimator.SetBool("inLand", true);
        }
        if (!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (inGround || airControl))
        {
            playerRigidBody.velocity = new Vector2(horizontal * movementSpeed, playerRigidBody.velocity.y);
        }
        if (inGround && jump)
        {
            inGround = false;
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
            playerAnimator.SetTrigger("jump");
        }
        playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void PlayerAttacks()
    {
        if (attack && inGround && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            playerAnimator.SetTrigger("attack");
            playerRigidBody.velocity = Vector2.zero;
        }
        if (jumpAttack && !inGround && !this.playerAnimator.GetCurrentAnimatorStateInfo(1).IsName("jumpAttack"))
        {
            playerAnimator.SetBool("jumpAttack", true);
        }
        if (!jumpAttack && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            playerAnimator.SetBool("jumpAttack", false);
        }
    }

    private void playerInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            attack = true;
            jumpAttack = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            jump = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 faceScale = transform.localScale;
            faceScale.x *= -1;
            transform.localScale = faceScale;
        }
    }

    private bool Grounded()
    {
        if (playerRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadious, ground);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        playerAnimator.ResetTrigger("jump");
                        playerAnimator.SetBool("inLand", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!inGround)
        {
            playerAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            playerAnimator.SetLayerWeight(1, 0);
        }
    }

    private void ResetValues()
    {
        attack = false;
        jump = false;
        jumpAttack = false;
    }

}
