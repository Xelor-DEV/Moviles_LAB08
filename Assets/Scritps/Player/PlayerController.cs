using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float touchDeadZone = 50f;

    [Header("Score Settings")]
    [SerializeField] private float scoreMultiplier = 1f;

    private Rigidbody rb;
    private BoxCollider coll;
    private bool isGrounded;
    private float highestY;
    private Vector3 startPosition;
    private Vector2 touchStartPos;
    private bool isTouching = false;
    private bool canPassThrough = false;
    [Header("Platform Collision")]
    [SerializeField] private float platformPassThroughTime = 0.5f;
    private bool isJumpingFromPlatform = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        startPosition = transform.position;
        highestY = startPosition.y;
    }

    private void Update()
    {
        HandleMobileInput();
        UpdateScore();
        CheckFall();
    }

    private void HandleMobileInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    float touchPosX = touch.position.x;
                    float screenMiddle = Screen.width / 2f;

                   
                    if (touchPosX < screenMiddle - touchDeadZone)
                    {
                        rb.linearVelocity = new Vector3(-moveSpeed, rb.linearVelocity.y, 0);
                    }
                
                    else if (touchPosX > screenMiddle + touchDeadZone)
                    {
                        rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y, 0);
                    }
                    else
                    {
                        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                    }
                    break;

                case TouchPhase.Ended:
                    isTouching = false;
                    rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                    break;
            }
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void UpdateScore()
    {
        if (transform.position.y > highestY)
        {
            highestY = transform.position.y;
            int score = Mathf.FloorToInt((highestY - startPosition.y) * scoreMultiplier);
            GameManager.Instance.UpdateScore(score);
        }
    }

    private void CheckFall()
    {
        if (transform.position.y < startPosition.y - 5f)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataform") && !isJumpingFromPlatform)
        {
            ContactPoint contact = collision.contacts[0];

           
            if (contact.normal.y > 0.7f)
            {
                Destroy(collision.gameObject);

              
                GameManager.Instance.UpdateScore(1);

               
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

               
                StartCoroutine(EnablePlatformPassThrough());
            }
        }
    }
    private IEnumerator EnablePlatformPassThrough()
    {
        isJumpingFromPlatform = true;

        
        Collider playerCollider = GetComponent<Collider>();
        foreach (var platform in GameObject.FindGameObjectsWithTag("Plataform"))
        {
            Physics.IgnoreCollision(playerCollider, platform.GetComponent<Collider>(), true);
        }

        yield return new WaitForSeconds(platformPassThroughTime);

      
        foreach (var platform in GameObject.FindGameObjectsWithTag("Plataform"))
        {
            Physics.IgnoreCollision(playerCollider, platform.GetComponent<Collider>(), false);
        }

        isJumpingFromPlatform = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }

}