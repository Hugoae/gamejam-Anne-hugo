using UnityEngine;
using System.Collections;

public class Attac : MonoBehaviour
{
    [Header("Références")]
    public GameObject Projectile;
    public Transform Emitter;       // Embout du canon
    public Transform Target;

    [Header("Paramètres de tir")]
    public float frequency = 1f;    // secondes entre tirs
    public float impulse = 20f;     // force d'impulsion appliquée à la balle
    public float spawnOffset = 0.6f;

    [Header("Filtrage par Tag (solution 1)")]
    [Tooltip("Tous les objets portant ce Tag seront ignorés par les projectiles (pas de collision).")]
    public string friendlyTag = "Turret";
    [Tooltip("Ignorer aussi les colliders de la tourelle qui tire (fortement conseillé).")]
    public bool ignoreShooterColliders = true;

    void Start()
    {
        StartCoroutine(AutoAttack());
    }

    IEnumerator AutoAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(frequency);

        while (true)
        {
            if (Projectile && Emitter && Target)
            {
                // 1) Direction vers la cible + orientation du canon
                Vector3 dir = (Target.position - Emitter.position).normalized;
                Emitter.rotation = Quaternion.LookRotation(dir, Vector3.up);
                Vector3 spawnPos = Emitter.position + dir * spawnOffset;

                // 3) Instancier la balle
                GameObject bullet = Instantiate(Projectile, spawnPos, Emitter.rotation);

                if (bullet.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.AddForce(dir * impulse, ForceMode.Impulse);
                    rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                }

                var bulletCol = bullet.GetComponent<Collider>();
                if (bulletCol != null)
                {
                    if (ignoreShooterColliders)
                    {
                        var myCols = GetComponentsInChildren<Collider>();
                        foreach (var c in myCols) Physics.IgnoreCollision(bulletCol, c, true);
                    }

                    //ignorer TOUT objet de la scène portant le tag Turret sinon les balles sont stoppées par ces dernières
                    if (!string.IsNullOrEmpty(friendlyTag))
                    {
                        var friendlies = GameObject.FindGameObjectsWithTag(friendlyTag);
                        foreach (var go in friendlies)
                        {
                            if (ignoreShooterColliders && go == gameObject) continue;

                            var cols = go.GetComponentsInChildren<Collider>();
                            foreach (var c in cols) Physics.IgnoreCollision(bulletCol, c, true);
                        }
                    }
                }
            }
            yield return wait;
        }
    }
}