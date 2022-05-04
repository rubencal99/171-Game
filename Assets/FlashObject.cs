using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour {

 // vars to Flash a colour when hit 
 public Color NormalColour;
  public Color FlashColour = Color.white;

 public Renderer GameMesh;
 public float FlashDelay = 0.025f;
 public int TimesToFlash = 3;

void Start() {
    NormalColour = this.GetComponent<MeshRenderer>().material.color;
}
 public void Flash(){

  StartCoroutine(OnCollisionEnter());
 }

 private IEnumerator  OnCollisionEnter() {
  var renderer = GameMesh; 
  if (renderer != null) {  

   for (int i = 1; i <= TimesToFlash; i++) {
    renderer.material.color = FlashColour; 
    yield return new WaitForSeconds (FlashDelay);
    renderer.material.color = NormalColour;
    yield return new WaitForSeconds (FlashDelay);
   }
  }
 }

}