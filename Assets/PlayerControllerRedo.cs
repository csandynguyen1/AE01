using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControllerRedo : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxSpeed = 3f;
    public float dodgeDistance = 50f;
    // Each frame of physics, what percentage of the speed should be shaved off the velocity out of 1 (100%)
    public float idleFriction = 100f;

    public SwordAttack swordAttack;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;
    public bool canMove = true;

    public bool iframes = false;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = PowerupManager.Instance.playerSpeed;
        maxSpeed = moveSpeed + 1f;
    }

    public void setSpeed(float speed)
    {
        maxSpeed = speed+1f;
        moveSpeed = speed;
    }

    void FixedUpdate() {
        if(canMove)
        {
            if(moveInput != Vector2.zero) {
                // Move animation and add velocity

                // Accelerate the player while run direction is pressed
                // BUT don't allow player to run faster than the max speed in any direction
                rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime) * idleFriction, maxSpeed);

                // Control whether looking left or right
                if(moveInput.x > 0) {
                    spriteRenderer.flipX = false;
                    
                } else if (moveInput.x < 0) {
                    spriteRenderer.flipX = true; 
                    
                }

                animator.SetBool("isMoving", true);
                UpdateAnimatorParameters();
            } 
            else {
                // No movement so interpolate velocity towards 0
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
                animator.SetBool("isMoving", false);
            }
        }
        else {
                // No movement so interpolate velocity towards 0
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
                animator.SetBool("isMoving", false);
            }
    }


    // Get input values for player movement
    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    IEnumerator OnDodge (InputValue value) 
    {
            animator.SetTrigger("dodge");
            this.GetComponent<DamageableCharacter>().damageable = false;
            
            Vector2 dodgeDir = rb.velocity.normalized;
            Vector2 targetPos = rb.position + dodgeDir * dodgeDistance; 

            float startTime = Time.time;
            float journeyLength = Vector2.Distance(rb.position, targetPos);
            float elapsedTime = 0f;

            while (elapsedTime < 0.8f) // Change this value to adjust the duration of the dodge
            {
                elapsedTime = Time.time - startTime;
                float fractionOfJourney = Mathf.Clamp01(elapsedTime / 0.8f); // Duration of dodge
                rb.MovePosition(Vector2.Lerp(rb.position, targetPos, fractionOfJourney));
                yield return null; 
            }

            this.GetComponent<DamageableCharacter>().damageable = true;
        
    }

    // Not currently in use
    void UpdateAnimatorParameters() {
        //animator.SetFloat("moveX", moveInput.x);
        //animator.SetFloat("moveY", moveInput.y);
    }

    // OnFire = LMB, triggers attack
    void OnFire() 
    {
        animator.SetTrigger("swordAttack");
    }

    // Stop moving, swing sword in character direction
    public void SwordAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX == true)
            swordAttack.AttackLeft();
        else
            swordAttack.AttackRight();
    }

    // End animation and renable movement
    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

   public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement() 
    {
        canMove = true;
    }

    public void RestartLevel()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    
}
