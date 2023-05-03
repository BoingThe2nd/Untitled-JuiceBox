using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    GeneralShoot m_GeneralShoot;



    //enum for Flavour

    public float MoveSpeed;

    //Most of these are temp, please replace with eventual auto caculate 
    public float ShotCoolDown;
    public float ShotSpeed;
    public int ShotDamage;

    private void Start()
    {
        m_GeneralShoot = GetComponent<GeneralShoot>();

        m_GeneralShoot.UpdateStats(ShotSpeed, ShotCoolDown, ShotDamage);
    
    }


}
