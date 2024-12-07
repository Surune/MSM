using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonScript : MonoBehaviour
{
    private Button SettingsButton;
    private Button BGMButton;
    private Button SaveButton;
    private Button LoadButton;

    private Font arial;
    public Canvas MainStoryCanvas;
    private Transform SettingsParent;
    //private GameObject ccc;
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
        //arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        SettingsButton = MainStoryCanvas.transform.GetChild(0).transform.GetComponentInChildren<Button>();
        SettingsButton.onClick.AddListener(SettingsButtonFunction);
        SettingsParent = MainStoryCanvas.transform.GetChild(8);
        SettingsParent.localScale = new Vector2(0, 0);

        BGMButton = SettingsParent.GetChild(0).GetComponent<Button>();
        SaveButton = SettingsParent.GetChild(1).GetComponent<Button>();
        LoadButton = SettingsParent.GetChild(2).GetComponent<Button>();

        BGMButton.onClick.AddListener(BGMButtonFunction);
        SaveButton.onClick.AddListener(SaveButtonFunction);
        LoadButton.onClick.AddListener(LoadButtonFunction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SettingsButtonFunction()
    {
        SettingsParent.localScale = new Vector2(1f - SettingsParent.localScale.x, 1f - SettingsParent.localScale.y);

        /*
        if (!GameObject.Find("settingsbutton active test text"))
        {
            ccc = new GameObject();
            ccc.name = "settingsbutton active test text";
            ccc.transform.parent = GameObject.Find("Canvas").transform;
            ccc.AddComponent<Text>();
            ccc.GetComponent<Text>().font = arial;
            ccc.GetComponent<Text>().text = "settingsbutton pressed";
            ccc.GetComponent<Text>().fontSize = 80;
            ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
            ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
            ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
            ccc.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1); //원래 0.01f
        }
        else
        {
            Destroy(ccc);
        }*/
    }

    private void BGMButtonFunction()
    {

    }
    private void SaveButtonFunction()
    {
        //인벤토리
        //노드
        //스킬
        //스탯
        //
    }
    private void LoadButtonFunction()
    {

    }
}
