using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TempSpecial : SpecialAbility
{
    public override void Activate(Pet pet)
    {
        // Find closest enemy and attack
        Debug.Log("Casting Special.");
        GameObject target = pet.FindClosestEnemy();
        if (target != null)
        {
            GameObject bullet = GameObject.Instantiate(pet.bulletPrefab, pet.transform.position, Quaternion.identity);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            pet._animator.SetTrigger("creatureAttack");

            if (bulletComponent != null)
            {
                bulletComponent.SetTarget(target.transform);
                bulletComponent.transform.localScale = new Vector3(.5f, .5f, .5f);
                bulletComponent.damage = pet.attack * 5;
            }
        }
    }

}
