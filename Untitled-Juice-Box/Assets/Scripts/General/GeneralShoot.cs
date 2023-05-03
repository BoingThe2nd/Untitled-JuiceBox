using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralShoot: MonoBehaviour
{
    public enum ShotType {Regular, Explosive, Beam,  }


    [SerializeField]
    GameObject JuiceShot;



    public float ShotCooldown;
    public float ShotSpeed;
    public int ShotDamage;

    float CooldownCount;
    public bool AbleToShoot = true;
    public bool Shooting = false;

    public void Update()
    {
        if (GameManager.m_GameState != GameManager.GameState.Defend)
        {
            return;
        }
        if (Shooting)
        {
            TryShoot();
        }
        if (!AbleToShoot)
        {
            CooldownCount += Time.deltaTime;
            if (CooldownCount >= ShotCooldown)
            {
                AbleToShoot = true;
            }
        }
    }

    public void UpdateStats(float Speed, float Cooldown, int Damage)
    {
        ShotSpeed = Speed;
        ShotCooldown = Cooldown;
        ShotDamage = Damage;
    }

    public void ToggleShoot()
    {
        Shooting = !Shooting;
    }

    public void TryShoot()
    {
        if (GameManager.m_GameState != GameManager.GameState.Defend)
        {
            return;
        }

        if (AbleToShoot)
        {
            Shoot();
            AbleToShoot = false;
            CooldownCount = 0;
        }
    }


    public void Shoot()
    {
        if(GetComponent<TowerBase>() != null)
        {
            GameObject ReleasedShot = Instantiate(JuiceShot, transform.position + transform.forward, transform.rotation);
            ReleasedShot.GetComponent<Rigidbody>().velocity = transform.forward * ShotSpeed;
            ReleasedShot.GetComponent<BlobBehvaiour>().UpdateStats(ShotDamage);
        }
        else
        {
            GameObject ReleasedShot = Instantiate(JuiceShot, transform.position + transform.forward + Vector3.up, transform.rotation);
            ReleasedShot.GetComponent<Rigidbody>().velocity = transform.forward * ShotSpeed;
            ReleasedShot.GetComponent<BlobBehvaiour>().UpdateStats(ShotDamage);
        }

     
    }

}
