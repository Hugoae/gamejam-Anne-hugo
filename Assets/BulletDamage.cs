using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 10f;
    void OnCollisionEnter(Collision c)
    {
        var hp = c.collider.GetComponentInParent<SystemedeSante>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var hp = other.GetComponentInParent<SystemedeSante>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
