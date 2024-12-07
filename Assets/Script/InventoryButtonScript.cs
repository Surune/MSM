using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonScript : MonoBehaviour
{
    private Button InventoryButton;
    private ScrollRect InventoryScrollView;

    private Vector2 ContentLocation;
    private Font arial;

    public Canvas MainStoryCanvas;
    /*
      * SettingsButton 0 
      * InventoryButton 1 
      * Choice1Button 2 
      * Choice2Button 3
      * Choice3Button 4
      * NextButton 5
      * BeforeButton 6      
      * StoryParent 7
      * SettingsParent 8
      * SkillButton 9
      * StatButton 10
      * SkillParent 11
      * StatParent 12
      * LevelButton 13
      * InventoryScrollView 14
      * EXPParent 15
      */

    // Start is called before the first frame update
    void Start()
    {
        //arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.tff");
        InventoryScrollView = MainStoryCanvas.transform.GetChild(14).GetComponent<ScrollRect>();
        InventoryButton = MainStoryCanvas.transform.GetChild(1).GetComponent<Button>();
        InventoryButton.onClick.AddListener(InventoryButtonFunction);
        InventoryScrollView.transform.localScale = new Vector3(0, 0, 0);
        ContentLocation = new Vector2(-180, 234);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InventoryButtonFunction()
    {
        InventoryScrollView.transform.localScale = new Vector3(1f - InventoryScrollView.transform.localScale.x, 1f - InventoryScrollView.transform.localScale.y, 0); //원래 0.01f
    }

}
