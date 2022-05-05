using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAmmoPrimary : MonoBehaviour
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
    async void Update()
    {
        if (w.Primary != null && w.Primary.GetComponent<Gun>() != null){
            var weap = w.Primary.GetComponent<Gun>();
            ammo = weap.ammo;
            totalAmmo = weap.TotalAmmo;
            AmmoText.text = ammo.ToString() + " | " + totalAmmo.ToString();
        }
        else AmmoText.enabled = false;
    }
}
