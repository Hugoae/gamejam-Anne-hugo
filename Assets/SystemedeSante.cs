using UnityEngine;

public class SystemedeSante : MonoBehaviour
{
    public event System.Action<float> OnchangedeSante;
    [SerializeField] private float maxSante = 100f;
    [SerializeField] private float actuelleSante;

    public float MaxSante
    {
        get { return maxSante; }
    }

    public float ActuelleSante
    {
        get { return actuelleSante; }
    }

    public bool IsDead
    {
        get { return actuelleSante <= 0; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actuelleSante = maxSante;
        if (OnchangedeSante != null)
        {
            OnchangedeSante(ObtenirSanteNormalisee());
        }
    }

    public float ObtenirSanteNormalisee()
    {
        return actuelleSante / maxSante;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        actuelleSante = Mathf.Clamp(actuelleSante - damage, 0f, maxSante);

        if (OnchangedeSante != null)
        {
            OnchangedeSante(ObtenirSanteNormalisee());
        }

        if (actuelleSante <= 0)
        {
            Debug.Log($"{gameObject.name} est mort !");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
