using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float damage = 10f;
    public float range = 10f;
    public float lookRadius = 20f;
    public Camera fpsCamera;
    public bool isDead = false;
    public ParticleSystem muzzleFlash;
    NavMeshAgent agent;
    Transform target;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        target = Player.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("shoot", 1, 1.5f);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            CancelInvoke();
        }
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius && !isDead)
        {
            agent.SetDestination(target.position);
        }
        if (distance <= agent.stoppingDistance && !isDead)
        {
            faceTarget();
        }
    }
    void faceTarget()
    {
        if (!isDead)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
    void shoot()
    {
        if (!isDead)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                muzzleFlash.Play();
                Debug.Log(hit.transform.name);
                Player player = hit.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.takeDamage(damage);
                }
            }
        }
    }
    public int takeDamage(float ammount)
    {
        health -= ammount;
        if (health <= 0f && !isDead)
        {
            die();
            return 1;
        }
        return 0;
    }
    void die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
