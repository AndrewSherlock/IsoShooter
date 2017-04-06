using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour {

    public float health;
    public float speed;
    public GameObject player;
    public Collider enemyFront;

    EnemyAttack enemyAttack;
    public bool gunsInRoom = true;
    float hitCooldown;
   
    public NavMeshAgent navMeshAI;


    public void Start()
    {
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAI = GetComponent<NavMeshAgent>();

        Debug.Log(enemyAttack);
        health = 100;
    }

    public void Update()
    {
        if (enemyAttack.gunObject == null && gunsInRoom)
        {
            CheckForWeapon();
        }

        if (enemyAttack.gunObject)
        {
            navMeshAI.stoppingDistance = 5;
        }

        if (!gunsInRoom && enemyAttack.gunObject == null && player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 10f)
            {
                if (player.GetComponentInChildren<PlayerAttack>().currentGun != null)
                {
                    navMeshAI.SetDestination(new Vector3(0.31f, transform.position.y, -7.95f));
                }
                else {
                    CombatWithPlayer(player);
                    if (Vector3.Distance(transform.position, player.transform.position) > 1.5f)
                    {
                        navMeshAI.Resume();
                    }
                }
            }
           
            if (hitCooldown > 0)
            {
                hitCooldown -= Time.deltaTime;
            }
            else
            {
                hitCooldown = 0;
            }
        }

        Debug.DrawRay(transform.position, -Vector3.forward * 100f, Color.green);
        Debug.DrawRay(transform.position, Vector3.forward * 100f, Color.green);
        Debug.DrawRay(transform.position, -Vector3.left * 100f, Color.red);
        Debug.DrawRay(transform.position, Vector3.left * 100f, Color.red);
    }

    void CombatWithPlayer(GameObject player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) > .8f)
        {
            navMeshAI.stoppingDistance = .8f;
            navMeshAI.SetDestination(player.transform.position);
        }
        else {
            if (hitCooldown <= 0)
            {
                print("Attacking position");
                navMeshAI.Stop();
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        Collider[] hitColliders = Physics.OverlapBox(enemyFront.bounds.center, enemyFront.bounds.extents);

        foreach(Collider c in hitColliders)
        {
            if(c.tag == "Player")
            {
                hitCooldown = 1f;
               // c.SendMessage("TakeDamage", 10); TODO: implement player damage
                print("I hit your punk ass");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
        Debug.Log("Life : " + health);
    }

    void Die()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }

    void CheckForWeapon()
    {
        float widthOfRoom = 0;
        float lengthOfRoom = 0;
        Vector3 centerPoint = Vector3.zero; 

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward * 100f, out hit))
        {
            if (hit.collider.tag == "Wall")
            {
                RaycastHit secondCheck;

                if (Physics.Raycast(transform.position, -Vector3.forward * 100f, out secondCheck))
                {
                    widthOfRoom = Vector3.Distance(hit.point, secondCheck.point);
                }
                centerPoint.z = (secondCheck.point.z + (widthOfRoom / 2));
            }
        }

        if (Physics.Raycast(transform.position, Vector3.left * 100f, out hit))
        {
            if (hit.collider.tag == "Wall")
            {
                RaycastHit secondCheck;

                if (Physics.Raycast(transform.position, -Vector3.left * 100f, out secondCheck))
                {
                    lengthOfRoom = Vector3.Distance(hit.point, secondCheck.point);
                }
                centerPoint.x = (secondCheck.point.x - (lengthOfRoom / 2));
            }
        }

        centerPoint.y = transform.position.y;
        Vector3 halfExtends = new Vector3(lengthOfRoom, 20, widthOfRoom);
        

        Collider[] hitObjects = Physics.OverlapBox( centerPoint, halfExtends);
        FindWeaponFromArray(hitObjects);
    }
 
    void FindWeaponFromArray(Collider[] objects)
    {
        Vector3 mostViableWeapon = Vector3.zero;
        float closetDistance = 100f;
        GameObject weapon = null;
        gunsInRoom = false;

        foreach (Collider c in objects)
        {
            if (c.tag == "Weapon")
            {
                gunsInRoom = true;
                float dist = Vector3.Distance(transform.position, c.transform.position);
                if (closetDistance > dist)
                {
                    mostViableWeapon = c.transform.position;
                    closetDistance = dist;
                    weapon = c.gameObject;
                }
            }
        }
        GoToWeaponOrRun(mostViableWeapon, weapon);
    }

    void GoToWeaponOrRun(Vector3 weaponPosition, GameObject weapon)
    {
        
        weaponPosition.y = transform.position.y;
        
        if (weaponPosition != Vector3.zero)
        {
            print("heading to gun");
            navMeshAI.SetDestination(weaponPosition);
            navMeshAI.stoppingDistance = 0;
            //transform.position = Vector3.MoveTowards(transform.position, weaponPosition, 3f * Time.deltaTime);
        }
    }
}
