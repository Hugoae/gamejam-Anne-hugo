using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] float lifetime = 1f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
