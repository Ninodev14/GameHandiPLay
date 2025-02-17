using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerBehaviour : MonoBehaviour
{
    public float raycastDistance = 0.2f;
    [SerializeField] private LayerMask platformLayerMask;
    public float acceleration = 5f;
    public float maxSpeed = 20f;
    public float friction = 10f;
    public Rigidbody2D rb2D;
    public BoxCollider2D bc2D;
    private float speedX = 0f;
    private float targetSpeedX = 0f;
    void Start(){
        
        rb2D.gravityScale = 1f; // enable gravity
    }
    void Update()
    {
        // Calculate target speed based on input
        targetSpeedX = Input.GetAxis("Horizontal") * maxSpeed;

        // Smoothly change the current speed towards the target speed
        speedX = Mathf.Lerp(speedX, targetSpeedX, Time.deltaTime);
        speedX /= Mathf.Pow(friction, Time.deltaTime)*Mathf.Pow(friction, Time.deltaTime);
        
        // Move the player
        rb2D.velocity = new Vector2(speedX, rb2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded())
        {
        rb2D.gravityScale *= -1f;
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y * -1 , transform.localScale.z);
        }
        IsGrounded();
    }
    private bool IsGrounded(){
        
        RaycastHit2D hit = Physics2D.BoxCast(bc2D.bounds.center, bc2D.bounds.size,0f, (Vector2.down*rb2D.gravityScale), raycastDistance, platformLayerMask);
        return hit.collider != null;
    }
    
}