using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNameWeapon : MonoBehaviour
{
    public GameObject obj;
    public PlayerWeapon w;
    public Text WeaponName;
    // Start is called before the first frame update
    void Start()
    {
        w = obj.GetComponent<PlayerWeapon>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //WeaponName.text =  w.
    }
}
