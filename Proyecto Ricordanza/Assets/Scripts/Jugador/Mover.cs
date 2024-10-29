using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private float moverHorizontal;
    private Vector2 direccion;
    private bool mirandoALaDerecha = true;

    private Rigidbody2D miRigidbody2D;
    private Animator miAnimator;
    private SpriteRenderer miSprite;
    private BoxCollider2D miCollider2D;
    private Jugador jugador;

    private int saltarMask;

    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
        miSprite = GetComponent<SpriteRenderer>();
        miCollider2D = GetComponent<BoxCollider2D>();
        saltarMask = LayerMask.GetMask("Pisos", "Plataformas");
        jugador = GetComponent<Jugador>();

        // Verifica que el perfil esté asignado
        if (jugador.PerfilJugador == null)
        {
            Debug.LogError("Perfil de jugador no asignado en el componente Jugador.");
        }
    }

    private void Update()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
        direccion = new Vector2(moverHorizontal, 0f);

        // Comprobar la dirección y voltear el sprite si es necesario
        if (moverHorizontal > 0 && !mirandoALaDerecha)
        {
            Voltear();
        }
        else if (moverHorizontal < 0 && mirandoALaDerecha)
        {
            Voltear();
        }

        // Actualizar la animación según la velocidad en X
        int velocidadX = Mathf.Abs((int)miRigidbody2D.velocity.x);
        miAnimator.SetInteger("Velocidad", velocidadX);

        // Actualizar si está en el aire
        miAnimator.SetBool("EnAire", !EnContactoConPlataforma());
    }

    private void FixedUpdate()
    {
        // Se actualiza la velocidad directamente para controlar mejor el movimiento
        miRigidbody2D.velocity = new Vector2(direccion.x * jugador.PerfilJugador.VelocidadHorizontal, miRigidbody2D.velocity.y);
    }

    private bool EnContactoConPlataforma()
    {
        return miCollider2D.IsTouchingLayers(saltarMask);
    }

    private void Voltear()
    {
        mirandoALaDerecha = !mirandoALaDerecha;
        miSprite.flipX = !miSprite.flipX;
    }
}