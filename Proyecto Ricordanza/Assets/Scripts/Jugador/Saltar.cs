using System.Collections;
using UnityEngine;

public class Saltar : MonoBehaviour
{
    private Jugador jugador;

    // Variables de uso interno en el script
    private bool puedoSaltar = false; // Control si se puede saltar
    private bool saltando = false; // Control si ya está en el aire
    private float tiempoCoyote = 0f; // Contador para el Coyote Time
    [SerializeField] private float duracionCoyote = 0.2f; // Duración del Coyote Time

    // Variables para Raycast
    [Header("Configuración de Raycast")]
    [SerializeField] private Transform puntoDeRaycast; // Punto de origen del Raycast
    [SerializeField] private float distanciaRaycast = 0.2f; // Longitud del Raycast
    [SerializeField] private LayerMask capasDeteccion; // Capas que considera como suelo

    // Componentes
    private Rigidbody2D miRigidbody2D;
    private AudioSource miAudioSource;

    private void Awake()
    {
        jugador = GetComponent<Jugador>();
    }

    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAudioSource = GetComponent<AudioSource>();
        jugador = GetComponent<Jugador>();
    }

    private void Update()
    {
        // Detectar si el jugador está en el suelo o tiene tiempo de coyote
        puedoSaltar = EnSuelo() || tiempoCoyote > 0;

        // Verificar si el jugador presiona Space y puede saltar
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            RealizarSalto();

            if (miAudioSource != null && jugador.PerfilJugador.JumpSFX != null)
            {
                miAudioSource.PlayOneShot(jugador.PerfilJugador.JumpSFX);
            }
        }

        // Reducir el tiempo de coyote si no está en el suelo
        if (!EnSuelo())
        {
            tiempoCoyote -= Time.deltaTime;
        }
        else
        {
            tiempoCoyote = duracionCoyote; // Reiniciar el Coyote Time
        }
    }

    private void FixedUpdate()
    {
        if (saltando)
        {
            saltando = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reiniciar el estado de salto al tocar el suelo
        if (EnSuelo())
        {
            saltando = false;

            if (miAudioSource != null && jugador.PerfilJugador.CollisionSFX != null)
            {
                miAudioSource.PlayOneShot(jugador.PerfilJugador.CollisionSFX);
            }
        }
    }

    private void RealizarSalto()
    {
        // Aplicar la fuerza del salto
        miRigidbody2D.velocity = new Vector2(miRigidbody2D.velocity.x, jugador.PerfilJugador.FuerzaSalto);
        saltando = true;
        puedoSaltar = false;
    }

    private bool EnSuelo()
    {
        // Realizar un Raycast para detectar si está en el suelo
        RaycastHit2D hit = Physics2D.Raycast(puntoDeRaycast.position, Vector2.down, distanciaRaycast, capasDeteccion);
        Debug.DrawRay(puntoDeRaycast.position, Vector2.down * distanciaRaycast, hit.collider != null ? Color.green : Color.red);

        return hit.collider != null;
    }
}
