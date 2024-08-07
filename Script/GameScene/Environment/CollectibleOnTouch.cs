using UnityEngine;

public class CollectibleOnTouch : MonoBehaviour
{
    private bool isCollected = false; // To prevent multiple triggers

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            string collectibleName = gameObject.name.ToLower(); // Get the name of the collectible item and convert to lowercase
            Debug.Log("Collected: " + collectibleName);
            if (collectibleName == "rainbowcake(clone)")
            {
                // Play a specific sound effect for rainbowcake
                collision.gameObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("rare item sfx"));
            }
            else
            {
                // Play a default sound effect for other collectible items
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
            // Add score or other collection logic here
            Destroy(gameObject);
        }
    }
}
