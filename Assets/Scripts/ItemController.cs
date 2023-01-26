using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemController : MonoBehaviour
{
    public int ItemID;
    public int currentQuantity;
    public TextMeshProUGUI currentQuantityText;
    public bool IsClicked = false;
    private LevelEditManager editor;
    // Start is called before the first frame update
    void Start()
    {
        currentQuantityText.text = currentQuantity.ToString();
        editor = GameObject.FindGameObjectWithTag("LevelEditManager").GetComponent<LevelEditManager>();
    }

    public void OnButtonClick()
    {
        if (currentQuantity > 0)
        {
            IsClicked = true;
            Vector2 screenPosistion = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 relativeMousePosistion = Camera.main.ScreenToWorldPoint(screenPosistion);
            Instantiate(editor.ItemImage[ItemID], new Vector3(relativeMousePosistion.x, relativeMousePosistion.y, 0), Quaternion.identity);
            currentQuantity--;
            currentQuantityText.text = currentQuantity.ToString();
            editor.CurrentPressedButton = ItemID;
        }
    }
}
