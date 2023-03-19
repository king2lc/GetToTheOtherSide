using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SkinManager : MonoBehaviour
{
   public SpriteRenderer sr;
   public List<Sprite> skins = new List<Sprite>();
   private int selectedSkin = 0;
   public GameObject playerskin;

   public void ChangeNext(){
    selectedSkin = selectedSkin +1;
    if(selectedSkin == skins.Count){
        selectedSkin = 0;

    }
    sr.sprite = skins[selectedSkin];
   }
   public void ChangePrev(){
    selectedSkin = selectedSkin -1;
    if(selectedSkin < 0){
        selectedSkin = skins.Count - 1;

    }
    sr.sprite = skins[selectedSkin];
   }
   public void PlayGame(){
    PrefabUtility.SaveAspre
   }
}
  

