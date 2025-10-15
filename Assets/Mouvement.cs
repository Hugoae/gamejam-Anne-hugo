using UnityEngine;

public class mouvement : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Déplacement")]
    public float moveSpeed = 10f;        // vitesse normale

    [Header("Dash")]
    public float dashImpulse = 15f;     // force du dash
    public float dashCooldown = 0.5f;   // délai entre deux dashs
    public float dashLockTime = 0.12f;  // durée pendant laquelle on “verrouille” le dash

    Vector3 moveInput;
    Vector3 lastMoveDir;
    bool canDash = true;
    bool dashLock = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ;
        rb.useGravity = false; // vue top-down
    }

    void Update()
    {
        float x = 0f, z = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) x -= 1f;   // gauche
        if (Input.GetKey(KeyCode.RightArrow)) x += 1f;   // droite
        if (Input.GetKey(KeyCode.UpArrow)) z += 1f;   // avant
        if (Input.GetKey(KeyCode.DownArrow)) z -= 1f;   // arrière

        moveInput = new Vector3(x, 0f, z);
        if (moveInput.sqrMagnitude > 0.001f)
            lastMoveDir = moveInput.normalized;

        // ---- Dash sur ESPACE ----
        if (Input.GetKeyDown(KeyCode.Space) && canDash && !dashLock)
        {
            StartCoroutine(Dash());
        }

        if (!dashLock)
        {
            Vector3 vel = moveInput.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(vel.x, 0f, vel.z);
        }
    }

    System.Collections.IEnumerator Dash()
    {
        canDash = false;
        dashLock = true;

        Vector3 dir = lastMoveDir.sqrMagnitude > 0.001f ? lastMoveDir : Vector3.forward;
        rb.AddForce(dir * dashImpulse, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashLockTime);
        dashLock = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
