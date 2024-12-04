using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score;
    private int vidas = 5; // Valor predeterminado de vidas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            score = 1000; // Valor predeterminado de puntos
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetGameDefaults()
    {
        // Restablecer valores predeterminados
        score = 1000;
        vidas = 5;

        // Guardar en PersistenceManager
        PersistenceManager.Instance.SetInt(PersistenceManager.KeyScore, score);
        PersistenceManager.Instance.SetInt("Vidas", vidas);
    }

    public int GetVidas()
    {
        return vidas;
    }

    public void ModificarVidas(int cantidad)
    {
        vidas += cantidad;
        if (vidas < 0) vidas = 0;
    }
}
