using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float attackRange;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform attackPointDir;

    [SerializeField] private GameObject projectile;

    [SerializeField] private float attackCooldown;

    private float rotationSpeed = 2f;

    private bool canAttack = true;

    private bool canSeePlayer = false;

    private ActionOnTimer actionOnTimer;

    private GameObject player;

    private void Awake ()
    {
        player = GameObject.Find("Player");
        actionOnTimer = GameObject.Find("GameManager").GetComponent<ActionOnTimer>();
    }

    private void Update ()
    {
        // Se o player existe
        if (player)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            // Se o player estiver no raio de ataque
            if (distanceToPlayer <= attackRange)
            {
                // Checar a posição do player para ver se ele não está escondido
                CheckPlayer();

                // Se ele pode ver o player e pode atacar
                if (canSeePlayer && canAttack)
                {
                    // Atire no player
                    FireAtPlayer();

                    // Aciona o cooldown
                    actionOnTimer.SetTimer(attackCooldown, ResetCooldown);
                }
            }
        }
    }

    private void ResetCooldown ()
    {
        canAttack = true;
    }

    private void CheckPlayer ()
    {
        Vector3 enemyDirection = transform.position;
        Vector3 playerDirection = player.transform.position;

        // Estamos dando sort nessa array pois o RaycastAll nem sempre garante a ordem de retorno dos resultados
        RaycastHit[] hit = 
            Physics.RaycastAll(enemyDirection, playerDirection - enemyDirection, attackRange).OrderBy(obj => obj.distance).ToArray();

        //Debug.DrawRay(enemyDirection, playerDirection - enemyDirection, Color.magenta);

        // Caso haja colisões
        if (hit.Length > 0)
        {
            if (hit[0].collider.gameObject.CompareTag("Player"))
            {
                // Se o primeiro objeto da nossa array é o player
                canSeePlayer = true;
                LookAtPlayer();
            }
            else
            {
                // Caso contrário, não pode ver o player
                canSeePlayer = false;
            }
        }
    }

    private void FireAtPlayer ()
    {
        // Se pode ver o jogador, atirar nele
        if (canSeePlayer)
        {
            canAttack = false;
            GameObject newProjectile = Instantiate(projectile, attackPoint.position, attackPoint.rotation);
            newProjectile.GetComponent<ProjectileController>().SetDirection(attackPointDir.position - attackPoint.position);
        }
    }

    private void LookAtPlayer ()
    {
        // Se pode ver o jogador, olhar para ele
        if (canSeePlayer)
        {
            // Rotaciona aos poucos até o player
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
