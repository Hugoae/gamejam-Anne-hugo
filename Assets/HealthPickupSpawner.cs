using UnityEngine;
using System.Collections;

public class HealthPickupSpawner : MonoBehaviour
{
    [Header("Refs")]
    public GameObject pickupPrefab;
    public Transform player;

    [Header("Zone de spawn (plan XZ)")]
    public Vector2 areaMin = new Vector2(-8f, -8f);
    public Vector2 areaMax = new Vector2( 8f,  8f);
    public float groundY = 0.5f;

    [Header("Fréquence aléatoire")]
    public float minInterval = 10f;
    public float maxInterval = 15f;

    [Header("Sécurité de placement")]
    public LayerMask blockedMask;
    public float spawnCheckRadius = 0.6f;
    public float safeRadiusFromPlayer = 1.8f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            TrySpawnOnce();
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);
        }
    }

    void TrySpawnOnce()
    {
        if (!pickupPrefab || !player) return;

        const int maxTries = 25;
        for (int i = 0; i < maxTries; i++)
        {
            float x = Random.Range(areaMin.x, areaMax.x);
            float z = Random.Range(areaMin.y, areaMax.y);
            Vector3 pos = new Vector3(x, groundY, z);

            // Éviter d'être trop proche du joueur
            Vector2 pj = new Vector2(player.position.x, player.position.z);
            Vector2 sp = new Vector2(x, z);
            if ((sp - pj).sqrMagnitude < safeRadiusFromPlayer * safeRadiusFromPlayer)
                continue;

            // Éviter les murs / tourelles / joueur via un CheckSphere
            if (Physics.CheckSphere(pos, spawnCheckRadius, blockedMask, QueryTriggerInteraction.Ignore))
                continue;

            Instantiate(pickupPrefab, pos, Quaternion.identity);
            break;
        }
    }
}