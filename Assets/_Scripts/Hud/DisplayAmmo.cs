using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAmmo : MonoBehaviour
{   
    public GameObject obj;
    private PlayerWeapon w;
    private int ammo;
    private int totalAmmo;
    public Text AmmoText;
    // Start is called before the first frame update
    void Start()
    {
        w = obj.GetComponentInChildren<PlayerWeapon>(); // 
    }

    // Update is called once per frame
    void Update()
    {
        if (w != null){
            ammo = w.weapon.ammo;
            totalAmmo = w.weapon.TotalAmmo;
            AmmoText.text = ammo.ToString() + "             " + totalAmmo.ToString();
        }
    }
}
