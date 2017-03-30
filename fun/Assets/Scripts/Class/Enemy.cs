using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public string name;
    public float health;
    public float speed;

    EnemyAttack enemyAttack;

    public void Start()
    {
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        Debug.Log(enemyAttack);
        health = 100;
    }

    public void Update()
    {
        if (enemyAttack.gunObject == null)
        {
            CheckForWeapon();
        }

        Debug.DrawRay(transform.position, -Vector3.forward * 100f, Color.green);
        Debug.DrawRay(transform.position, Vector3.forward * 100f, Color.green);
        Debug.DrawRay(transform.position, -Vector3.left * 100f, Color.red);
        Debug.DrawRay(transform.position, Vector3.left * 100f, Color.red);
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
        Vector3 halfExtends = new Vector3(widthOfRoom, 20, lengthOfRoom) / 2;

        Collider[] hitObjects = Physics.OverlapBox(centerPoint, halfExtends);
        FindWeaponFromArray(hitObjects);
    }

    void FindWeaponFromArray(Collider[] objects)
    {
        Vector3 mostViableWeapon = Vector3.zero;
        float closetDistance = 100f;
        GameObject weapon = null;

        foreach (Collider c in objects)
        {
            if (c.tag == "Weapon")
            {
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
            transform.position = Vector3.MoveTowards(transform.position, weaponPosition, 3f * Time.deltaTime);
        }
    }
}
