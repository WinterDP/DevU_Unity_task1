using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float flightSpeed = 25f;

    private Vector3 flightDirection;

    private Rigidbody rigidBody;

    private void Awake ()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start ()
    {
        // Destruir o proj�til caso este se perca por 10
        Destroy(gameObject, 10f);
    }

    private void Update ()
    {
        // Atualizar a velocidade conforme a dire��o e a velocidade padr�o
        rigidBody.velocity = flightDirection * flightSpeed;
    }

    private void OnTriggerEnter (Collider other)
    {
        // Realizar as colis�es
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player!");
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Occluder"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection (Vector3 direction)
    {
        // Muda a dire��o do proj�til para ele "olhar" para a dire��o do ataque
        flightDirection = direction.normalized;
        transform.LookAt(direction);
    }
}
