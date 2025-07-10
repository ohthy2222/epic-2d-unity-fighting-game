using UnityEngine; // required for literally everything in Unity 
using UnityEngineUI; // this is necessary for the healthbar to appear
using System.Collections; // this is required for coroutines (animations and other events that happen based on time. In this case, blinking.)

public class DummyBag : MonoBehaviour // Declaring a class with MonoBehaviour allows the GameObjects to be attached to the DummyBag class in the Unity Editor
{
    public int hpMax = 100; // Health Point quantity when the health bar is full

    public Image hpBarFill; // have the image for the health bar UI filled
    public SpriteRenderer spriteRenderer; // Declare the sprite renderer that displays 2D images like the dummy bag so that we can have it blink
    public Color dmgBlink = new Color(1f, 0f, 0f, 0.5f); // the dummy will blink this very light red when damaged
    public float blinkTime = 0.1f; // the duration in which the dummy bag blinks when it is damaged

    private int currHp; // tracks the current health points of the dummy bag
    private Color origColor; // keeps the original color of the dummy bag

    // signature: Start(): void -> void

    // purpose: expects nothing and returns nothing
    //          with the side effect of being called automatically
    //          upon scene startup and everything inside of the function
    //          happening at that time
    void Start()
    {
        currHp = hpMax;

        if (spriteRenderer == null) // if the inspector has no sprite renderer assigned inside of it...
        {
            spriteRenderer = GetComponent<spriteRenderer>(); // get the sprite renderer automatically from this 
        }

        if (spriteRenderer != null)
        {
            origColor = spriteRenderer.color; // assign the original color before the dummy bag switches colors
        }
    }

    // signature: OnTriggerEnter2D(): Collider2D -> void

    // purpose: expects Collider2D and returns nothing
    //          with the side effect of being called automatically
    //          upon a invisible hit area box (Collider2D) entering an objects trigger area
    void OnTriggerEnter2D(Collider2D collision) // collision is the type of invisible hit area box parameter that was mentioned in the purpose statement
    {
        if (collision.CompareTag("CowboyBullet"))
        {
            Damager(10); // the damager function makes it so that each yellow bullet deals 10 damage
            Destroy(collision.gameObject); // delete the bullet upon colliding with the dummy bag
        }
    }

    // signature: Damager(): int -> void

    // purpose: expects an int and returns nothing
    //          with the side effect of lowering the HP
    //          of the dummy bag in the game logic and  
    //          visually upon calling thehangeHpBar() function and
    //          destroying the cowboy's bullet

    void Damager(int dmgAmt)
    {
        currHp -= dmgAmt; // decrease the dummy bag's HP
        ChangeHpBar(); // update the visual HP bar accordingly
        StartCouroutine(dmgFlash); // start the damage blink animation

        if (currHp <= 0)
        {
            Destroy(gameObject); // delete the dummy because it lost all of it's health
        }
    }

    // signature: ChangeHpBar(): void -> void

    // purpose: expects nothing and returns nothing
    //          with the side effect of lowering the HP
    //          of the dummy bag visually.
    void ChangeHpBar()
    {
        if (hpBarFill != null) // As long as a fill Image exists
        {
            hpBarFill.fillAmount = (float)currHp / hpMax; // set the fill amount (which is a float between 0 and 1 by converting using "(float)") to the ratio of health remaining
        }
    }

    // signature: FlashDamage(): void -> IEnumerator

    // purpose: expects nothing and returns an IEnumerator (a coroutine method that can run, wait, and then keep going afterwards)
    //          with the side effect of changing the dummy bag's color
    IEnumerator FlashDamage()
    {
        if (spriteRenderer != null) // If the sprite renderer exists
        {
            spriteRenderer.color = dmgBlink; // change the color to very light red
            yield return new WaitForSeconds(blinkTime); // pause right here, wait for the duration of blinktime but don't freeze the whole game, and then keep going
            spriteRenderer.color = origColor; // put the dummy bag's color back to how it was
        }
    }
}