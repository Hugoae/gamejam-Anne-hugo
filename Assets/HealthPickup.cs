using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Effet")]
    public float healAmount = 40f;

    [Header("Look (optionnel)")]
    public float spinSpeed = 90f; // deg/s
    public float bobAmplitude = 0.1f;
    public float bobSpeed = 2f;

    Vector3 basePos;

    void Start()
    {
        basePos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
        transform.position = basePos + Vector3.up * (Mathf.Sin(Time.time * bobSpeed) * bobAmplitude);
    }

    void OnTriggerEnter(Collider other)
    {
        var hp = other.GetComponentInParent<SystemedeSante>();
        if (hp != null)
        {
            hp.TakeDamage(-healAmount);
            Destroy(gameObject);
        }
    }
}
