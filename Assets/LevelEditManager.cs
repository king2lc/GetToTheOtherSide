using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditManager : MonoBehaviour
{
    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] ItemImage;

    public int CurrentPressedButton;

    private void Update()
    {
        Vector2 screenPosistion = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 relativeMousePosistion = Camera.main.ScreenToWorldPoint(screenPosistion);

        //need to add controls for controller here...
        if(Input.GetMouseButtonDown(0) && ItemButtons[CurrentPressedButton].IsClicked)
        {
            ItemButtons[CurrentPressedButton].IsClicked = false;
            Instantiate(ItemPrefabs[CurrentPressedButton], new Vector3(relativeMousePosistion.x, relativeMousePosistion.y, 0), Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }
    }
}
