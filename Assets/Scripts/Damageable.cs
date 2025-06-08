using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Permite que un objeto tenga salud, pueda recibir daño y notifique al Animator cuando muere.
/// </summary>
public class Damageable : MonoBehaviour
{

    public UnityEvent<int, Vector2> damageableHit;

    // Referencia al componente Animator para controlar animaciones relacionadas con la vida/muerte.
    Animator animator;

    /// <summary>
    /// Salud máxima del objeto. Puede ser configurada desde el Inspector.
    /// </summary>
    [SerializeField]
    private float _maxHealth = 100;

    /// <summary>
    /// Propiedad pública para acceder o modificar la salud máxima.
    /// </summary>
    public float MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;

        }
    }

    /// <summary>
    /// Salud actual del objeto. Inicialmente 100.
    /// </summary>
    [SerializeField]
    private float _health = 100;

    /// <summary>
    /// Propiedad pública para acceder o modificar la salud actual.
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
    /// Indica si el objeto está vivo.
    /// </summary>

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvencible = false;
    private float timeSinceHit = 0f;
    public float invincibilityTimer = 0.25f;

    // The velocity should not be changed while this is true but needs to be respectfed by other components like the PlayerController
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }



    /// <summary>
    /// Propiedad pública para acceder o modificar el estado de vida.
    /// Si el objeto muere, actualiza el parámetro correspondiente en el Animator.
    /// </summary>
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            //if (!_isAlive)
            //{
            // Cambia el parámetro del Animator para reflejar la muerte.
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive: " + value + " on " + gameObject.name);
            //}
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

    private void Update()
    {
        if (isInvencible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                isInvencible = false;
                timeSinceHit = 0f;
            }

            timeSinceHit += Time.deltaTime;// tiempo entre frames  
        }
        //Hit(10f);
    }

    /// <summary>
    /// Returns wheter the damageable took damge or not
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool Hit(float damage, Vector2 knockback)
    {
        if (IsAlive && !isInvencible)
        {
            Health -= damage;
            isInvencible = true;
            if (Health <= 0)
            {
                IsAlive = false;
            }
            // Notify other subscriber components that the damageable was hit  to handle the knockback and such
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;

            damageableHit?.Invoke((int)damage, knockback);

            return true;
        }
        // unable to be hit
        return false;
    }

}
