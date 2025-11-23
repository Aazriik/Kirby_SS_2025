// Libraries
using UnityEngine;
using System.Collections;

public class SimplePickups: MonoBehaviour
{
    // Types of pickups
    public enum PickupType
    {
        Life,               // Increases player's life
        Powerup,            // Grants player a powerup
        Coin                // Increases player's coin count
    }

    // Type of this pickup
    public PickupType pickupType;

    // Spawn position offset
    public Vector3 spawnOffset = new Vector3(0, 0.3f, 0);

    void Start()
    {
        // Adjust the spawn position based on the offset
        transform.position += spawnOffset;

        // Disable the collider initially
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            // Enable the collider after 1 seconds
            StartCoroutine(EnableColliderAfterDelay(1f));
        }
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = true;
        }
    }


    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerController component from the player
            PlayerController pc = collision.GetComponent<PlayerController>();
            // Apply the effect based on the pickup type
            switch (pickupType)
            {
                case PickupType.Life:
                    // Increase player's life
                    pc.lives++;
                    break;
                case PickupType.Powerup:
                    // Grant player a powerup
                    pc.ApplyJumpForcePowerup();
                    break;
                case PickupType.Coin:
                    // Increase player's coin count
                    break;
            }
            // Destroy the pickup after collection
            Destroy(gameObject);
        }
    }
}
