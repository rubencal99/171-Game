using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Display different weapons when change weapon
public class DisplayWeaponSecondary : MonoBehaviour
{
    public static DisplayWeaponSecondary Instance {get; private set;}
    private void Awake(){
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject obj;
    public PlayerWeapon w;
    public Image weaponImage;
    public SpriteRenderer SR;
    public Sprite weaponSprite;

    // Start is called before the first frame update
    void Start()
    {
        if(!obj)
        {
            obj = Player.instance.gameObject;
        }
        w = obj.GetComponentInChildren<PlayerWeapon>();
        weaponImage = this.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if(w.Secondary)
        {
            SR = w.Secondary.GetComponent<SpriteRenderer>();
            weaponSprite = SR.sprite;
            weaponImage.sprite = weaponSprite;
            var col = weaponImage.color;
            col.a = 100f;
            weaponImage.color = col;
             weaponImage.enabled = true;
        } else weaponImage.enabled = false;
        
    }

}