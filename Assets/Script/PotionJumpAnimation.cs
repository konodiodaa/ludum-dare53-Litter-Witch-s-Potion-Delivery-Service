using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionJumpAnimation : MonoBehaviour
{
    public Vector3 spawnPosition;
    public float jumpForce = 200f;
    public float gravity = 9.81f;

    private bool isJumping = false;

    void Start()
    {
        // Set the initial position of the object
        transform.position = spawnPosition;

        // Start the jump animation
        StartCoroutine(JumpAnimation());
    }

    IEnumerator JumpAnimation()
    {
        // Wait for a random amount of time before starting the jump
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        // Jump!
        isJumping = true;
        float jumpTime = 0f;
        while (jumpTime < 1f)
        {
            // Calculate the height of the jump based on time in the air
            float jumpHeight = Mathf.Lerp(0f, jumpForce, jumpTime);

            // Calculate the velocity of the jump based on time in the air and gravity
            float jumpVelocity = jumpForce - gravity * jumpTime;

            // Update the position of the object
            transform.position = spawnPosition + Vector3.up * jumpHeight;

            // Increment the time in the air and apply gravity
            jumpTime += Time.deltaTime;
            transform.position -= Vector3.up * gravity * jumpTime * Time.deltaTime;

            yield return null;
        }

        // Wait for a random amount of time before starting the fall
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        // Fall!
        float fallTime = 0f;
        while (transform.position.y > spawnPosition.y)
        {
            // Calculate the height of the fall based on time in the air
            float fallHeight = Mathf.Lerp(0f, jumpForce, fallTime);

            // Calculate the velocity of the fall based on time in the air and gravity
            float fallVelocity = -gravity * fallTime;

            // Update the position of the object
            transform.position = spawnPosition + Vector3.up * fallHeight;

            // Increment the time in the air and apply gravity
            fallTime += Time.deltaTime;
            transform.position -= Vector3.up * gravity * fallTime * Time.deltaTime;

            yield return null;
        }

        // Reset the position and state of the object
        transform.position = spawnPosition;
        isJumping = false;

        // Restart the jump animation
        StartCoroutine(JumpAnimation());
    }
}
