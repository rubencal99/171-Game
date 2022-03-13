using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BackgroundSoundManager : MonoBehaviour
{
    public CircleCollider2D detect;
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
    void Start()
    {
        detect = GetComponent<CircleCollider2D>();
        // instantite music
        CombatMusicInst = FMODUnity.RuntimeManager.CreateInstance(CombatMusicEvent);
        CombatMusicInst.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        CombatMusicInst.start();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Read enemy type from collider, increment corresponding variable
        if (collider.gameObject.CompareTag("Enemy")){
            // Debug.Log(collider.gameObject.name + " detected");

            // this is probably the most scuffed way possible to do this, 
            // pls add enemy classes
            if (
                collider.gameObject.name == "Inmate" ||
                collider.gameObject.name == "LinearAttackEnemy" 
            )
            {
                prisonerCount += 1;
            }
            else if (
                collider.gameObject.name == "Warden" ||
                collider.gameObject.name == "Warden (Veteran)" ||
                collider.gameObject.name == "SharpShooter Warden" ||
                collider.gameObject.name == "SharpShooter Warden (Veteran)" ||
                collider.gameObject.name == "Rambo Warden" ||
                collider.gameObject.name == "_Base Warden"
            )
            {
                wardenCount += 1;
            }
            else if(
                collider.gameObject.name == "MedicEnemy" ||
                collider.gameObject.name == "BufferEnemy"
            )
            {
                supportCount += 1;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Read enemy type from collider, decriment corresponding variable
        if (collider.gameObject.CompareTag("Enemy")){
            // Debug.Log(collider.gameObject.name + " lost");

            // this is probably the most scuffed way possible to do this, 
            // pls add enemy classes
            if (
                collider.gameObject.name == "Inmate" ||
                collider.gameObject.name == "LinearAttackEnemy" 
            )
            {
                prisonerCount -= 1;
            }
            else if (
                collider.gameObject.name == "Warden" ||
                collider.gameObject.name == "Warden (Veteran)" ||
                collider.gameObject.name == "SharpShooter Warden" ||
                collider.gameObject.name == "SharpShooter Warden (Veteran)" ||
                collider.gameObject.name == "Rambo Warden" ||
                collider.gameObject.name == "_Base Warden"
            )
            {
                wardenCount -= 1;
            }
            else if(
                collider.gameObject.name == "MedicEnemy" ||
                collider.gameObject.name == "BufferEnemy"
            )
            {
                supportCount -= 1;
            }
        }
    }

    void Update()
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
    }

    void OnDestroy()
    {
        CombatMusicInst.release();
    }
}
