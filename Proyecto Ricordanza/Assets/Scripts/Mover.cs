using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] float velocidad = 5f;

    private float moverHorizontal;
    private Vector2 direccion;
    private bool mirandoALaDerecha = true; // Nueva variable para controlar la dirección del jugador

    private Rigidbody2D miRigidbody2D;
    private Animator miAnimator;
    private SpriteRenderer miSprite;
    private BoxCollider2D miCollider2D;

    private int saltarMask;

    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
        miSprite = GetComponent<SpriteRenderer>();
        miCollider2D = GetComponent<BoxCollider2D>();
        saltarMask = LayerMask.GetMask("Pisos", "Plataformas");
    }

    private void Update()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
        direccion = new Vector2(moverHorizontal, 0f);

        // Verificar si el jugador está moviéndose hacia la derecha o izquierda
        if (moverHorizontal > 0 && !mirandoALaDerecha)
        {
            Voltear();
        }
        else if (moverHorizontal < 0 && mirandoALaDerecha)
        {
            Voltear();
        }

        // Actualizar la animación según la velocidad
        int velocidadX = Mathf.Abs((int)miRigidbody2D.velocity.x); // Abs para evitar valores negativos
        miAnimator.SetInteger("Velocidad", velocidadX);

        // Comprobar si está en el aire
        miAnimator.SetBool("EnAire", !EnContactoConPlataforma());
    }

    private void FixedUpdate()
    {
        // Aplicar la fuerza de movimiento al personaje
        miRigidbody2D.AddForce(direccion * velocidad);
    }

    private bool EnContactoConPlataforma()
    {
        return miCollider2D.IsTouchingLayers(saltarMask);
    }

    // Función para voltear el sprite
    private void Voltear()
    {
        mirandoALaDerecha = !mirandoALaDerecha;
        miSprite.flipX = !miSprite.flipX; // Voltear el sprite horizontalmente
    }
}
