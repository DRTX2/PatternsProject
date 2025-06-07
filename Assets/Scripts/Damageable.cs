using UnityEngine;

/// <summary>
/// Permite que un objeto tenga salud, pueda recibir da�o y notifique al Animator cuando muere.
/// </summary>
public class Damageable : MonoBehaviour
{
    // Referencia al componente Animator para controlar animaciones relacionadas con la vida/muerte.
    Animator animator;

    /// <summary>
    /// Salud m�xima del objeto. Puede ser configurada desde el Inspector.
    /// </summary>
    [SerializeField]
    private float _maxHealth;

    /// <summary>
    /// Propiedad p�blica para acceder o modificar la salud m�xima.
    /// </summary>
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    /// <summary>
    /// Salud actual del objeto. Inicialmente 100.
    /// </summary>
    private float _health = 100;

    /// <summary>
    /// Propiedad p�blica para acceder o modificar la salud actual.
    /// Si la salud llega a 0 o menos, el objeto se considera muerto.
    /// </summary>
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    /// <summary>
    /// Indica si el objeto est� vivo.
    /// </summary>
    private bool _isAlive = true;

    /// <summary>
    /// Propiedad p�blica para acceder o modificar el estado de vida.
    /// Si el objeto muere, actualiza el par�metro correspondiente en el Animator.
    /// </summary>
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            if (!_isAlive)
            {
                // Cambia el par�metro del Animator para reflejar la muerte.
                animator.SetBool(AnimationStrings.isAlive, false);
            }
        }
    }

    /// <summary>
    /// Inicializa la referencia al Animator y verifica su existencia.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    // M�todos Start y Update est�n presentes pero vac�os, por si se necesitan en el futuro.

    /// <summary>
    /// Llamado una vez antes del primer frame. (Actualmente vac�o)
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Llamado una vez por frame. (Actualmente vac�o)
    /// </summary>
    void Update()
    {
        
    }
}
