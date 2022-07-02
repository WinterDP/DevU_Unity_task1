using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character;

    [SerializeField] private float speed = 10f; 

    private float currentAngle;
    private float dampeningTime = 0.2f;

    [SerializeField] private Transform cameraTransform;

    private void Awake ()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update ()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Direção do input normalizada
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Caso haja input
        if (inputDirection.magnitude >= 0.1f)
        {
            // O ângulo para qual queremos girar o player, somado com o ângulo da câmera
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // O ângulo atual, suavizado para chegar no destino (interpolado)
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentAngle, dampeningTime);

            // Mudamos o ângulo do player com base no ângulo na suavização
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Multiplicamos o vetor de movimentação para frente com o ângulo de qual a câmera está olhando
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Realizando o movimento
            character.Move(moveDirection * speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos ()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);
        Debug.DrawLine(transform.position, transform.forward, Color.cyan);
        //Debug.DrawLine(transform.position, (transform.position + transform.forward), Color.yellow);
    }
}
