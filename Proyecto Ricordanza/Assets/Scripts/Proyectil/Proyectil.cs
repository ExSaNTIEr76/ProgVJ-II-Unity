using System.Collections;
using UnityEngine;

public abstract class Proyectil : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 30f)]
    protected float speed = 10f;

    [SerializeField]
    protected float lifeTime = 0.5f;

    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Mover();
        StartCoroutine(DesactivarDespuesDeTiempo());
    }

    protected abstract void Mover();

    private IEnumerator DesactivarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
