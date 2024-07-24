using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject); // Destroy fireball on collision
    }
}
