using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public float speed = 5f; // Velocidad del proyectil
    public float lifeTime = 3f; // Tiempo de vida del proyectil

    void Start()
    {
        // Destruir el proyectil después de 3 segundos
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mover el proyectil hacia la izquierda
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}

