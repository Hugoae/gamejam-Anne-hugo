using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [Header("Réfs")]
    public GameObject turretPrefab;
    public Transform player;

    [Header("Zone de spawn (plan XZ)")]
    public Vector2 areaMin = new Vector2(-8f, -8f);
    public Vector2 areaMax = new Vector2( 8f,  8f);
    public float groundY = 0.5f;

    [Header("Paramètres")]
    public float intervalSeconds = 10f;
    public float minDistanceFromPlayer = 2.5f;

    void Start()
    {
        // ✅ Premier spawn dans 10 s (pas immédiat)
        InvokeRepeating(nameof(SpawnOne), intervalSeconds, intervalSeconds);
    }

    void SpawnOne()
    {
        if (!turretPrefab || !player) return;

        for (int tries = 0; tries < 20; tries++)
        {
            float x = Random.Range(areaMin.x, areaMax.x);
            float z = Random.Range(areaMin.y, areaMax.y);
            Vector3 pos = new Vector3(x, groundY, z);

            // éviter spawn trop proche du joueur
            Vector2 pj = new Vector2(player.position.x, player.position.z);
            Vector2 sp = new Vector2(x, z);
            if ((sp - pj).sqrMagnitude < minDistanceFromPlayer * minDistanceFromPlayer)
                continue;

            GameObject t = Instantiate(turretPrefab, pos, Quaternion.identity);

            // branche Attac automatiquement
            var att = t.GetComponent<Attac>();
            if (att)
            {
                if (!att.Target) att.Target = player;
                if (!att.Emitter)
                {
                    Transform found = t.transform.Find("BulletEmit");
                    if (found) att.Emitter = found;
                }
            }

            break;
        }
    }
}