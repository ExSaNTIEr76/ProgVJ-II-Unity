using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Cinemachine.DocumentationSortingAttribute;

public class Jugador : MonoBehaviour
{
    [SerializeField]
    private PerfilJugador perfilJugador;
    public PerfilJugador PerfilJugador { get => perfilJugador; }


    //---------- Eventos del Jugador ----------//

    [SerializeField]
    private UnityEvent<int> OnLivesChanged;

    [SerializeField]
    private UnityEvent<string> OnTextChanged;

    private void Start()
    {
        OnLivesChanged.Invoke(perfilJugador.Vida);
        OnTextChanged.Invoke(GameManager.Instance.GetScore().ToString());
    }

    public void ModificarVida(int puntos)
    {
        perfilJugador.Vida += puntos;
        GameManager.Instance.AddScore(puntos * 100);
        OnTextChanged.Invoke(GameManager.Instance.GetScore().ToString());
        OnLivesChanged.Invoke(perfilJugador.Vida);

        if (!EstasVivo())
        {
            Debug.Log("PERDISTE");
            ApplicationManager.Instance.GoToPortada();
        }
    }

    private bool EstasVivo()
    {
        return perfilJugador.Vida > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Meta")) { return; }

        Debug.Log("GANASTE");
    }
}