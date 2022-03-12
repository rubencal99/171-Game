using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Display different weapons when change weapon
public class DisplayWeapon : MonoBehaviour
{
    public static DisplayWeapon Instance {get; private set;}
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
        w = obj.GetComponentInChildren<PlayerWeapon>();
        weaponImage = this.gameObject.GetComponent<Image>();
        UpdateWeapon();
    }

    public void UpdateWeapon()
    {
        SR = w.weapon.GetComponent<SpriteRenderer>();
        weaponSprite = SR.sprite;
        weaponImage.sprite = weaponSprite;
    }

}
