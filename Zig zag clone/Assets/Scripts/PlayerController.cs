using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask roadMask;
    Rigidbody playerRigidbody;
    BoxCollider collider;
    Animator animator;
    [SerializeField] GameObject brainEffect;
    [SerializeField] GameManager gameManager;
    public static float speedMultiplier;
    bool raycastHit;

    // Start is called before the first frame update
    private void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<BoxCollider>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            animator.SetTrigger("GameStarted");
        }

        playerRigidbody.transform.position = transform.position + transform.forward * Time.deltaTime * speedMultiplier;

        CheckPlayerDeath();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeAnimationToFalling(IsGrounded());
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
           ChangeDirection();
        }
 
    }
    private void ChangeDirection()
    {
        if (!gameManager.gameStarted) return;
        playerRigidbody.transform.rotation = Quaternion.Inverse(playerRigidbody.rotation);
    }
    private bool IsGrounded()
    {
       raycastHit = Physics.Raycast(collider.bounds.center,Vector3.down, 10f,roadMask);
        if (raycastHit)
        {
            return true;
        }else
        {
            return false;
        }
    }

    private void ChangeAnimationToFalling(bool IsGrounded)
    {
        if (!IsGrounded)
        {
            animator.SetBool("IsFalling",true);
        }
        else
        {
            animator.SetBool("IsFalling",false);
        }
    }

    private void CheckPlayerDeath()
    {
        if (transform.position.y <= -3f)
        {
            gameManager.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Brain")) 
        {
            gameManager.IncreesScore();
            GameObject particals = Instantiate(brainEffect, other.bounds.center, Quaternion.identity);
            Destroy(particals, 2);
            Destroy(other.gameObject);
        }
    }
}
