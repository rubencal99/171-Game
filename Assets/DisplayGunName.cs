using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGunName : MonoBehaviour
{
    public GameObject obj;
    private PlayerWeapon w;
    public Text GunNameText;
    // Start is called before the first frame update
    void Start()
    {
        w = obj.GetComponentInChildren<PlayerWeapon>();
    }

    //Update is called once per frame
    async void Update()
    {
        if (w != null) {
            /*if(w.gun)
            {
                GunNameText.text = w.gun.name;
            }
            else if(w.melee)
            {
                GunNameText.text = w.melee.name;
            }*/
            GunNameText.text = w.weapon.name;
        }
    }
}
