using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilRecto : Proyectil
{

    protected override void Mover()
    {
        // Establece la direccion del proyectil
        Vector2 direction = Vector2.down;
        // Aplica la velocidad al Rigidbody
        rb.velocity = direction * speed;
    }
}
