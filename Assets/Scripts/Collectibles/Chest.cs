using UnityEngine;

public class Chest : MonoBehaviour
{
    // Variables
    public GameObject[] spawnObjects; // Objects to spawn when the chest is opened
    Animator anim; // Animator component for chest animations
    Collider2D objCol; // Collider component for the object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            // Play the chest opening animation
            anim.SetTrigger("OpenChest");
            // Spawn 1 of the specified objects at the chest's position
            if (spawnObjects.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnObjects.Length);
                Instantiate(spawnObjects[randomIndex], transform.position, Quaternion.identity);
                // enable collider2D on the spawned object
                objCol = spawnObjects[randomIndex].GetComponent<Collider2D>();
                if (objCol != null)
                {
                    objCol.enabled = true;
                }
            }

            // Destroy the chest collision after opening
            Destroy(GetComponent<Collider2D>());
        }
    }
}
