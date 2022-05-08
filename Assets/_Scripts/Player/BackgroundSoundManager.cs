using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BackgroundSoundManager : MonoBehaviour
{
    public SphereCollider detect;
    public FMODUnity.EventReference CombatMusicEvent;
    private FMOD.Studio.EventInstance CombatMusicInst;
    [SerializeField]
    private int prisonerCount = 0;
    [SerializeField]
    private int wardenCount = 0;
    [SerializeField]
    private int supportCount = 0;
    [ParamRef]
    public string prisonerParam;
    [ParamRef]
    public string wardenParam;
    [ParamRef]
    public string supportParam;
    public Vector3 shopPos;
    public float shopDist;
    void Start()
    {
        detect = GetComponent<SphereCollider>();
        // instantite music
        CombatMusicInst = FMODUnity.RuntimeManager.CreateInstance(CombatMusicEvent);
        CombatMusicInst.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        CombatMusicInst.start();
    }
    void OnTriggerEnter(Collider collider)
    {
        // Read enemy type from collider, increment corresponding variable
        if (collider.gameObject.CompareTag("Enemy")){
            // Debug.Log(collider.gameObject.name + " detected");

            // this is probably the most scuffed way possible to do this, 
            // pls add enemy classes
            if (
                collider.gameObject.name == "Inmate 3D" 
            )
            {
                prisonerCount += 1;
            }
            else if (
                collider.gameObject.name == "Warden 3D" ||
                collider.gameObject.name == "Warden (Veteran) 3D" ||
                collider.gameObject.name == "SharpShooter Warden 3D" ||
                collider.gameObject.name == "SharpShooter Warden (Veteran) 3D" ||
                collider.gameObject.name == "Rambo Warden 3D" ||
                collider.gameObject.name == "_Base Warden 3D"
            )
            {
                wardenCount += 1;
            }
            else if(
                collider.gameObject.name == "MedicEnemy 3D" ||
                collider.gameObject.name == "BufferEnemy 3D"
            )
            {
                supportCount += 1;
            }
        }

        if (collider.gameObject.name == "ShopKeeper")
        {
            shopPos = collider.gameObject.transform.position;
            // Debug.Log("shopkeeper at " + shopPos); 
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Read enemy type from collider, decriment corresponding variable
        if (collider.gameObject.CompareTag("Enemy")){
            // Debug.Log(collider.gameObject.name + " lost");

            // this is probably the most scuffed way possible to do this, 
            // pls add enemy classes
            if (
                collider.gameObject.name == "Inmate 3D" 
            )
            {
                prisonerCount -= 1;
            }
            else if (
                collider.gameObject.name == "Warden 3D" ||
                collider.gameObject.name == "Warden (Veteran) 3D" ||
                collider.gameObject.name == "SharpShooter Warden 3D" ||
                collider.gameObject.name == "SharpShooter Warden (Veteran) 3D" ||
                collider.gameObject.name == "Rambo Warden 3D" ||
                collider.gameObject.name == "_Base Warden 3D"
            )
            {
                wardenCount -= 1;
            }
            else if(
                collider.gameObject.name == "MedicEnemy 3D" ||
                collider.gameObject.name == "BufferEnemy 3D"
            )
            {
                supportCount -= 1;
            }
        }
    }

    void FixedUpdate()
    {
        // if count > 0, increment relevant FMOD param by 0.1 until == 1
        // else, decrement relevant FMOD param by 0.1 until == 0 
        if (prisonerCount > 0){
            RuntimeManager.StudioSystem.setParameterByName(prisonerParam, 1.0f);
        }
        else {
            RuntimeManager.StudioSystem.setParameterByName(prisonerParam, 0.0f);
        }
        
        if (wardenCount > 0){
            RuntimeManager.StudioSystem.setParameterByName(wardenParam, 1.0f);
        }
        else {
            RuntimeManager.StudioSystem.setParameterByName(wardenParam, 0.0f);
        }

        if (supportCount > 0){
            RuntimeManager.StudioSystem.setParameterByName(supportParam, 1.0f);
        }
        else {
            RuntimeManager.StudioSystem.setParameterByName(supportParam, 0.0f);
        }

        // set shopDist and update ShopDistance param
        if (shopPos != null)
        {
            shopDist = Vector3.Distance(shopPos, transform.position);
            // Debug.Log("distance from shop: " + shopDist);
            CombatMusicInst.setParameterByName("ShopDistance", shopDist);
        }
    }

    void OnDestroy()
    {
        CombatMusicInst.release();
    }
}
