using UnityEngine;

public class CowboyGun : MonoBehaviour
{
    public GameObject yellowSquarePrefab // This is where the yellow square prefab gets dragged after being dragged into Assets
    public Transform gunMouth; // This is where the "Create Empty" Gun Mouth gets dragged to
    public float bulletFlalfness = 10f; // Determine how close the bullet is to the speed of The Flalf (The Flash)
    // Note: float values use less memory


    // signature: Update(): void -> void

    // purpose: expects nothing and returns nothing
    //          with the side effect of updating all objects within it, every frame.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // If the space bar is currently being pressed
        {
            lightEmUp(); // Call the function that shoots the yellowSquareBullet
        }
    }

    // signature: lightEmUp(): void -> void

    // purpose: expects nothing and returns nothing
    //          with the side effect of having the Cowboy shoot out mini yellow squares for bullets.

    void lightEmUp()
    {
        GameObject yellowSquareBullet = Instantiate(yellowSquarePrefab, gunMouth.position, Quaternion.identity);
        Ridigbody2D rigBod = bullet.GetComponent<Rigidbody2D>();

        if (rigBod != null)
        {
            Vector2 wayFacing = transform.right; // Adjust this when the cowboy switches directions. Initiate the direction as a Vector before assigning it to rigBod
            rigBod.velocity = wayFacing * bulletFlalfness; // This assigns the speed and direction of the rigidBody of the mini yellow square
        }
    }

}
