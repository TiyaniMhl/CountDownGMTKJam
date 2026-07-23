using UnityEngine;

public class Destructible : MonoBehaviour
{
    private void Hit()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<Bullet>().SomethingHit();
            Hit();
        }
    }
}
