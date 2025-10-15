using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public SystemedeSante Sante;
    public Canvas deathCanvas;

    bool dejaFait = false;

    void Start()
    {
        if (deathCanvas) deathCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!dejaFait && Sante != null && Sante.IsDead)
        {
            udied();
            displayudied();
            dejaFait = true;
        }
    }

    void udied()
    {
        var timer = FindObjectOfType<Timer>();
        if (timer) timer.StopTimer();

        var rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void displayudied()
    {
        if (deathCanvas) deathCanvas.gameObject.SetActive(true);
    }
}
