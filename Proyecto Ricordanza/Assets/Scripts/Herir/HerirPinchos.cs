using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerirPinchos : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private int puntosDeDaño = 5; // Daño que los pinchos causan al jugador
    private Vector3 posicionInicialJugador; // Posición inicial del jugador

    private void Start()
    {
        // Obtener la posición inicial del jugador al inicio del juego
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            posicionInicialJugador = jugador.transform.position;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player' en la escena.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el componente Jugador
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            if (jugador == null)
            {
                Debug.LogError("El objeto colisionado no tiene el componente 'Jugador'.");
                return;
            }

            // Aplicar daño al jugador
            jugador.ModificarVida(-puntosDeDaño);
            Debug.Log("Puntos de daño realizados al jugador por los pinchos: " + puntosDeDaño);

            // Transportar al jugador a su posición inicial
            collision.gameObject.transform.position = posicionInicialJugador;
        }
    }
}
