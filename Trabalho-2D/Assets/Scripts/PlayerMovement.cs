using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variáveis
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private LayerMask jumpableGround;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource jumpSoudEffect;

    // Atualiz prim. frame
    private void Start()
    {
        // Variáveis constantes
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Atualiz frame p/ frame
    private void Update()
    {
        // set key run
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // set key jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoudEffect.Play();
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        UpdateAnimationUpdate(); // chama função de mov do player
    }

    // Movimentação do player
    private void UpdateAnimationUpdate()
    {
        MovementState state;

        if (dirX > 0f) // Mov run direita
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) // Mov run esquerda
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else // Mov parado
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f) // Mov jumping
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f) // Mov fallin
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state); // retorna o "state" dependendo da condição
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); // set de opção para jump apenas em grounds(terrain)
    }
}