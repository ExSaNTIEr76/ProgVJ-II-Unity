using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    [SerializeField]
    private PerfilJugador perfilJugador;
    public PerfilJugador PerfilJugador { get => perfilJugador; }

    [SerializeField]
    private UnityEvent<int> OnLivesChanged;

    [SerializeField]
    private UnityEvent<string> OnTextChanged;

    private SpriteRenderer miSpriteRenderer;

    // Referencias a los objetos de UI
    [Header("Pantallas de Fin de Juego")]
    [SerializeField] private GameObject pantallaGameOver;
    [SerializeField] private GameObject pantallaGanaste;

    private void Awake()
    {
        miSpriteRenderer = GetComponent<SpriteRenderer>();
        if (miSpriteRenderer == null)
        {
            Debug.LogError("No se encontró un SpriteRenderer en el jugador.");
        }

        // Validar que las pantallas estén asignadas
        if (pantallaGameOver == null || pantallaGanaste == null)
        {
            Debug.LogError("Las pantallas de GameOver o Ganaste no están asignadas.");
        }
    }

    private void Start()
    {
        OnLivesChanged.Invoke(perfilJugador.Vida);
        OnTextChanged.Invoke(GameManager.Instance.GetScore().ToString());
    }

    private void Update()
    {
        // Reiniciar el juego al presionar Spacebar
        if (Input.GetKeyDown(KeyCode.Space) && (pantallaGameOver.activeSelf || pantallaGanaste.activeSelf))
        {
            ReiniciarJuego();
        }
    }

    public void ModificarVida(int puntos)
    {
        perfilJugador.Vida += puntos;

        if (puntos < 0)
        {
            StartCoroutine(ParpadeoRojo());
        }

        GameManager.Instance.AddScore(puntos * 100);
        OnTextChanged.Invoke(GameManager.Instance.GetScore().ToString());
        OnLivesChanged.Invoke(perfilJugador.Vida);

        if (!EstasVivo())
        {
            MostrarGameOver();
        }
    }

    private bool EstasVivo()
    {
        return perfilJugador.Vida > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meta"))
        {
            MostrarGanaste();
        }
    }

    private IEnumerator ParpadeoRojo()
    {
        Color colorOriginal = miSpriteRenderer.color;

        miSpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        miSpriteRenderer.color = colorOriginal;
    }

private void MostrarGameOver()
{
    if (pantallaGameOver != null)
    {
        pantallaGameOver.SetActive(true);
        Time.timeScale = 0;

        // Reiniciar los valores predeterminados
        GameManager.Instance.ResetGameDefaults();
    }
    Debug.Log("Game Over: Fin del juego.");
}

private void MostrarGanaste()
{
    if (pantallaGanaste != null)
    {
        pantallaGanaste.SetActive(true);
        Time.timeScale = 0;

        // Reiniciar los valores predeterminados
        GameManager.Instance.ResetGameDefaults();
    }
    Debug.Log("Ganaste: Fin del nivel.");
}

    private void ReiniciarJuego()
    {
        // Restablecer el tiempo del juego
        Time.timeScale = 1;

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Juego reiniciado.");
    }
}
