using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;

public class MainGameManager : MonoBehaviour
{
    // Start is called before the first frame update

    

    private const int 특징 = 0;
    private const int 넘버링 = 1;
    private const int 상황비중 = 2;
    private const int 상황 = 3;
    private const int 상황가짓수 = 4;
    private const int 필요조건 = 5;
    private const int 선택지 = 6;
    private const int 액션여부 = 7;
    private const int 선택지가짓수 = 8;
    private const int 결과확률 = 9;
    private const int 결과 = 10;
    private const int 체력 = 11;
    private const int 멘탈 = 12;
    private const int 돈 = 13;
    private const int 능력치 = 14;
    private const int 경험치 = 15;
    private const int 스킬습득 = 16;
    private const int 특별 = 17;

    private const int 스탯개수 = 6;
    private const int 스킬개수 = 6;

    static string REGEX_PARENTHESIS = @"(?:\(|\{|\[)|(?:\]|\}|\))";
    private Font arial;
    private Text storytext;
    private List<Dictionary<string, object>> data;
    private ArrayListHeader TextArrayList = new ArrayListHeader();

    private int SituationWeightTotal = 0;
    private int SituationWeight = 0;
    private int StoryEventComplete = 0;

    private int ExpIntVariable = 0;
    private int LevelVariable = 1;
    private int[] StatNumArray;
    private int[] SkillNumArray;
    private string[] StatNameArray;
    private string[] SkillNameArray;
    private Button[] StatButtonArray;
    private Button[] SkillButtonArray;
    private Button SkillPointButton;
    private Button StatPointButton;
    private int StatAvailableVariable = 0;
    private int SkillAvailableVariable = 0;


    private Button NextButton;
    private Button BeforeButton;
    private Button ChoiceButton1;
    private Button ChoiceButton2;
    private Button ChoiceButton3;
    private Button SkillButton;
    private Button StatButton;
    private Button LevelButton;
    private Button EXPButton;
    //private Button SaveButton;
    //private Button LoadButton;

    private Transform SkillParent;
    private Transform StatParent;
    private Transform EXPParent;


    private System.Random random;
    private GameObject ccc; //button active test text 및 next page error 표시, 상황봐서 없앨것
    //private GameObject cccd; 확률 화면 정중간에 표시하는 것

    public Text InventoryText;
    private ScrollRect InventoryScrollView;
    private List<string> InventoryContentList = new List<string>();
    private Vector2 InventoryContentLocation = new Vector2(160, 230);
    private Transform InventoryContentObject;

    //public List<string> InventoryContentList = new List<string>();

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

    public static MainGameManager MainManager = null;
    private void Awake()
    {
        Screen.SetResolution(720, 1080, true);

        if (MainManager == null)
        {
            MainManager = this;
        }
        else if (MainManager != this)
        {
            Destroy(gameObject);
        }
        Debug.Log("1월4일이랑께");
        //DontDestroyOnLoad(MainManager);
        
    }

    void Start()
    {
        random = new System.Random();
        //arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        storytext = MainStoryCanvas.transform.GetChild(7).GetChild(0).GetComponent<Text>();

        NextButton = MainStoryCanvas.transform.GetChild(5).GetComponent<Button>();
        BeforeButton = MainStoryCanvas.transform.GetChild(6).GetComponent<Button>();
        ChoiceButton1 = MainStoryCanvas.transform.GetChild(2).GetComponent<Button>();
        ChoiceButton2 = MainStoryCanvas.transform.GetChild(3).GetComponent<Button>();
        ChoiceButton3 = MainStoryCanvas.transform.GetChild(4).GetComponent<Button>();

        SkillButton = MainStoryCanvas.transform.GetChild(9).GetComponent<Button>();
        StatButton = MainStoryCanvas.transform.GetChild(10).GetComponent<Button>();
        SkillParent = MainStoryCanvas.transform.GetChild(11);
        StatParent = MainStoryCanvas.transform.GetChild(12);

        EXPParent = MainStoryCanvas.transform.GetChild(15);

        LevelButton = MainStoryCanvas.transform.GetChild(13).GetComponent<Button>();
        //EXPButton = MainStoryCanvas.transform.GetChild(15).GetComponent<Button>();


        NextButton.onClick.AddListener(NextButtonFunction);
        BeforeButton.onClick.AddListener(BeforeButtonFunction);

        ChoiceButton1.onClick.AddListener(ChoiceButton1Function);
        ChoiceButton2.onClick.AddListener(ChoiceButton2Function);
        ChoiceButton3.onClick.AddListener(ChoiceButton3Function);

        StatButton.onClick.AddListener(StatButtonFunction);
        SkillButton.onClick.AddListener(SkillButtonFunction);

        InventoryScrollView = MainStoryCanvas.transform.GetChild(14).GetComponent<ScrollRect>();
        InventoryContentObject = InventoryScrollView.transform.GetChild(0).GetChild(0);



       // TextArrayList = GameManager.gameManager.GameManagerTextArrayList;

        /*
        nextbutton = GameObject.Find("NextButton").GetComponent<Button>();
        beforebutton = GameObject.Find("BeforeButton").GetComponent<Button>();
        nextbutton.onClick.AddListener(nextbuttonfunction);
        beforebutton.onClick.AddListener(beforebuttonfunction);
                
        InventoryScrollView = GameObject.Find("InventoryScorllView").GetComponent<ScrollRect>();

        choice1 = GameObject.Find("Choice1Button").GetComponent<Button>();
        choice2 = GameObject.Find("Choice2Button").GetComponent<Button>();
        choice3 = GameObject.Find("Choice3Button").GetComponent<Button>();
        choice1.onClick.AddListener(choice1buttonfunction);
        choice2.onClick.AddListener(choice2buttonfunction);
        choice3.onClick.AddListener(choice3buttonfunction);
         */

        ChoiceButton1.transform.localScale = new Vector3(0, 0, 0);
        ChoiceButton2.transform.localScale = new Vector3(0, 0, 0);
        ChoiceButton3.transform.localScale = new Vector3(0, 0, 0);


        TextArrayList = GameManager.gameManager.TextArrayList;
        StoryEventComplete = GameManager.gameManager.StoryEventComplete;

        //각 칸 앞에 @나 # 같은거 써두고, 전체 구분자를 #, 로 하는 것 해보기
        /*
        TextAsset data = Resources.Load("sample") as TextAsset;
        var lines = Regex.Split(data.text, @"\n");
        //Debug.Log(lines.Length);


        for (var i = 1; i < lines.Length - 1; i++)
        {
            // Debug.Log("for 문 begin");
            var values = Regex.Split(lines[i], @",#");

            //for (var j = 0; j < values.Length; j++) Debug.Log(values[j]);

            //Debug.Log("한줄 끝");


            if (values[특징] == "줄글")
            {
                //var eventletters = Regexc.Split(values[0], REGEX_PARENTHESIS);
                //TextArrayList.Add(eventletters[0]);
                TextArrayList.Add(values[상황비중], Int32.Parse(values[넘버링]));
                for (var j = 상황; j < values.Length; j++)
                {
                    Debug.Log(values[j]);
                    if (values[j] == "줄글끝") break;
                    else TextArrayList.Add(values[넘버링], values[j]);
                }
                //버튼에 뜨는 글자 확인하기
                TextArrayList.ToHead(TextArrayList.head.Count - 1);
            }
            else if (values[특징] == "이벤트")
            {
                try
                {
                    TextArrayList.Add(values[상황], Int32.Parse(values[넘버링]), Int32.Parse(values[상황비중]));
                }
                catch (System.IndexOutOfRangeException e1)
                {
                    Debug.Log("이벤트 엑셀 칸 부족, 여기 함수나 뭐 경고글 추가하든지 말든지");
                }

                int forvaluesvariabletemp = Int32.Parse(values[상황가짓수]);
                for (var j = 0; j < forvaluesvariabletemp; j++)
                {

                    TextArrayList.Add(values[필요조건], values[선택지],values[액션여부]);
                    if (values[액션여부] == "N") ////.Equals를 써야 될수도 있음
                    {
                        int forvaluesvariabletemp2 = Int32.Parse(values[선택지가짓수]);
                        for (var k = 0; k < forvaluesvariabletemp2; k++)
                        {
                            //Debug.Log("A");
                            // Debug.Log(values[결과확률]);
                            //Debug.Log("B");
                            //Debug.Log(values[결과]);
                            //Debug.Log("C");
                            //Debug.Log(values[체력]);
                            //Debug.Log("D");
                            //Debug.Log(values[멘탈]);
                            //Debug.Log("E");
                            //Debug.Log(values[돈]);
                            //Debug.Log("F");
                            //Debug.Log(values[능력치]);
                            //Debug.Log("G");

                            TextArrayList.Add(Int32.Parse(values[결과확률]), values[결과], Int32.Parse(values[체력]), Int32.Parse(values[멘탈]), Int32.Parse(values[돈]), values[능력치], Int32.Parse(values[경험치]),values[스킬습득], Int32.Parse(values[특별]));
                            values = Regex.Split(lines[++i], @",#");
                            TextArrayList.ToBefore();
                        }
                        TextArrayList.ToBefore();
                    }
                    else if (values[액션여부] == "Y")
                    {
                        int forvaluesvariabletemp2 = Int32.Parse(values[선택지가짓수]);
                        if(forvaluesvariabletemp2==-2)
                        {
                            for (var k = 0; k < 2; k++)
                            {                                
                                TextArrayList.Add(Int32.Parse(values[결과확률]), values[결과], Int32.Parse(values[체력]), Int32.Parse(values[멘탈]), Int32.Parse(values[돈]), values[능력치], Int32.Parse(values[경험치]), values[스킬습득], Int32.Parse(values[특별]));
                                values = Regex.Split(lines[++i], @",#");
                                TextArrayList.ToBefore();
                            }
                            TextArrayList.ToBefore();

                        }
                        else if(forvaluesvariabletemp2>0)
                        {
                            for (var k = 0; k < forvaluesvariabletemp2; k++)
                            {
                                //Debug.Log("A");
                                // Debug.Log(values[결과확률]);
                                //Debug.Log("B");
                                //Debug.Log(values[결과]);
                                //Debug.Log("C");
                                //Debug.Log(values[체력]);
                                //Debug.Log("D");
                                //Debug.Log(values[멘탈]);
                                //Debug.Log("E");
                                //Debug.Log(values[돈]);
                                //Debug.Log("F");
                                //Debug.Log(values[능력치]);
                                //Debug.Log("G");

                                TextArrayList.Add(Int32.Parse(values[결과확률]), values[결과], Int32.Parse(values[체력]), Int32.Parse(values[멘탈]), Int32.Parse(values[돈]), values[능력치], Int32.Parse(values[경험치]), values[스킬습득], Int32.Parse(values[특별]));
                                values = Regex.Split(lines[++i], @",#");
                                TextArrayList.ToBefore();
                            }
                            TextArrayList.ToBefore();
                        }
                        
                    }
                }

                TextArrayList.ToHead(TextArrayList.head.Count - 1);

                i--;

            }

        }
        var ivalues = Regex.Split(lines[lines.Length - 2], @",#");
        Debug.Log(ivalues[0]);
        Debug.Log("FFFFFFFFFFFF"+ivalues[넘버링]+"Ggggggg"+ivalues[상황비중]);
        TextArrayList.tophead.Add(new Node[Int32.Parse(ivalues[넘버링])]);
        TextArrayList.tophead.Add(new Node[Int32.Parse(ivalues[상황비중])]);
        //Debug.Log("FFFFFFFFFFFF");


        //StoryEventComplete = new bool[Int32.Parse(ivalues[넘버링])];

        //for (var i = 0; i < Int32.Parse(ivalues[넘버링]); i++) StoryEventComplete[i] = false;


        for (var i = 0; i < TextArrayList.head.Count; i++)
        {
            TextArrayList.Add(TextArrayList.head[i].character, TextArrayList.head[i].numbering, TextArrayList.head[i]);
        }
        */

        /*
        for (var i = 1; i < lines.Length; i++)
        {
            Debug.Log("for 문 begin");
            var values = Regex.Split(lines[i], @",");

            if (values[0] != "") //상황
            {
                var eventletters = Regex.Split(values[0], REGEX_PARENTHESIS);
                TextArrayList.Add(eventletters[0]);
            }
            else
            {
                //TextArrayList.ToHead(TextArrayList.head.Count-1);
            }

            if (values[1] != "") //선택지
            {
                var choiceletters = Regex.Split(values[1], REGEX_PARENTHESIS);
                //Debug.Log("choiceletters[0]" + choiceletters[1] + "choiceletters[1]" + choiceletters[2]);
                TextArrayList.Add(choiceletters[1], choiceletters[2]);
            }
            else
            {
                TextArrayList.ToNext(TextArrayList.current.next.Count - 1);
            }

            if (values[2] != "") //결과
            {
                var resultletters = Regex.Split(values[2], REGEX_PARENTHESIS + "|%");

                if (values[3] != "") //영향
                {
           
        //int a = Int32.Parse(resultletters[1]);                
        //Debug.Log("여길 보랑꼐"+values[5].Substring(0, values[5].Length - 1));


        //선택지 없는 스토리를 연속해서 계속 진행해 나갈일이 생길경우, if 문으로 values의 길이가 6보다 큰 항에 대해서 따로 하거나, 영향 칸을 순서쌍 형식이 아닌 다른 형식으로 하여 무조건 줄의 끝까지 add 되도록 해야함
        //스토리 진행 과정 중에 능력치를 얻게 되는 것 까지 고려할 경우 체력,돈,멘탈도 숫자의 순서쌍 형태가 아니라 string 형태로 해야할 가능성


        //Debug.Log("여길 보랑꼐" + values[6]);
        if (values[6] != "") //능력치
                    {
                        TextArrayList.Add(Int32.Parse(resultletters[1]), resultletters[3], Int32.Parse(values[3].Substring(1)), Int32.Parse(values[4]), Int32.Parse(values[5].Substring(0, values[5].Length - 1)), values[6]);
                    }
                    else
                    {
                        TextArrayList.Add(Int32.Parse(resultletters[1]), resultletters[3], Int32.Parse(values[3].Substring(1)), Int32.Parse(values[4]), Int32.Parse(values[5].Substring(0, values[5].Length - 1)), null);
                    }
                    
                    Debug.Log("parse 다음");
                }
            }
            else
            {
                TextArrayList.ToHead(TextArrayList.head.Count - 1);
            }
        }*/
        //Debug.Log("ASD");
        storytext = MainStoryCanvas.transform.GetChild(7).GetChild(0).transform.GetComponent<Text>();

        StatNumArray = new int[스탯개수];
        for (var i = 0; i < 스탯개수; i++) StatNumArray[i] = 0;

        SkillNumArray = new int[스킬개수];
        for (var i = 0; i < 스킬개수; i++) SkillNumArray[i] = 0;

        StatNameArray = new string[스탯개수]
        { "힘", "민첩", "운", "지식","체력", "스테미나"};

        SkillNameArray = new string[스킬개수]
        {"파이어볼","라이트닝","블리자드","라이프드레인","텔레포트","디택트포스" };

        StatPointButton = StatParent.GetChild(0).GetComponent<Button>();
        StatButtonArray = new Button[스탯개수];
        for(var i = 0; i < 스탯개수; i++)
        {
            StatButtonArray[i] = StatParent.GetChild(i + 1).GetComponent<Button>();
            //StatButtonArray[i].transform.localPosition = new Vector3(StatButtonArray[i].transform.localPosition.x, StatButtonArray[i].transform.localPosition.y, i);
            //StatButtonArray[i].onClick.AddListener(delegate { EachStatUpButtonFunction(i); });
        }
        SkillButtonArray = new Button[스킬개수];
        SkillPointButton = SkillParent.GetChild(0).GetComponent<Button>();
        for (var i = 0; i < 스킬개수; i++)
        {
            SkillButtonArray[i] = SkillParent.GetChild(i + 1).GetComponent<Button>();
            //SkillButtonArray[i].transform.localPosition = new Vector3(SkillButtonArray[i].transform.localPosition.x, SkillButtonArray[i].transform.localPosition.y,i);
            //SkillButtonArray[i].onClick.AddListener(delegate { EachSkillUpButtonFunction(2); });
        }

        StatButtonArray[0].onClick.AddListener(delegate { EachStatUpButtonFunction(0); });
        StatButtonArray[1].onClick.AddListener(delegate { EachStatUpButtonFunction(1); });
        StatButtonArray[2].onClick.AddListener(delegate { EachStatUpButtonFunction(2); });
        StatButtonArray[3].onClick.AddListener(delegate { EachStatUpButtonFunction(3); });
        StatButtonArray[4].onClick.AddListener(delegate { EachStatUpButtonFunction(4); });
        StatButtonArray[5].onClick.AddListener(delegate { EachStatUpButtonFunction(5); });


        SkillButtonArray[0].onClick.AddListener(delegate { EachSkillUpButtonFunction(0); });
        SkillButtonArray[1].onClick.AddListener(delegate { EachSkillUpButtonFunction(1); });
        SkillButtonArray[2].onClick.AddListener(delegate { EachSkillUpButtonFunction(2); });
        SkillButtonArray[3].onClick.AddListener(delegate { EachSkillUpButtonFunction(3); });
        SkillButtonArray[4].onClick.AddListener(delegate { EachSkillUpButtonFunction(4); });
        SkillButtonArray[5].onClick.AddListener(delegate { EachSkillUpButtonFunction(5); });

        SkillParent.localScale = new Vector2(0, 0);
        StatParent.localScale = new Vector2(0, 0);

        
        ButtonUpdateFunction();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextButtonFunction()
    {
        



        /*
       if (!GameObject.Find("nextbutton active test text"))
       {
           ccc = new GameObject();
           ccc.name = "nextbutton active test text";
           ccc.transform.parent = GameObject.Find("Canvas").transform;
           ccc.AddComponent<Text>();
           ccc.GetComponent<Text>().font = arial;
           ccc.GetComponent<Text>().text = "nextbutton pressed";
           ccc.GetComponent<Text>().fontSize = 80;
           ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
           ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
           ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
           ccc.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 1);
       }
       else
       {
           Destroy(ccc);
       }*/


        /////////////////////////if(줄글 성립 여부 함수) 
        /////////////////////////else{ 이하 전체 포함}
        Debug.Log(StoryEventCompletion(StoryEventComplete + 1).ToString());
        if (StoryEventCompletion(StoryEventComplete + 1))
        {
            Debug.Log("if ㄴㅁ라ㅓㄴ;ㄹㄴ어람너리;ㅁ널;너;ㅣ넒낭ㄻㄴㄹㅇ;ㅁㄴㅇㄹ");
            try
            {
                if (TextArrayList.current.newskillname != "")
                {
                    
                    Debug.Log(TextArrayList.current.newskillname.Substring(1)  + "느금마"+ GameManager.gameManager.SkillAwakePoolList[0].EachChildSkillList[0].name);
                    //if (!GameManager.gameManager.SkillAwakePoolList[Int32.Parse(TextArrayList.current.newskillname.Substring(0, 1))].jobunlocked)
                    if (!GameManager.gameManager.SkillAwakePoolList[Int32.Parse(TextArrayList.current.newskillname.Substring(0, 1))].jobunlocked)
                    {
                        JobUnlockFunction(Int32.Parse(TextArrayList.current.newskillname.Substring(0, 1)));

                        for (var i = 0; i < GameManager.gameManager.TotalPoolJobNum; i++)
                        {
                            if (GameManager.gameManager.SkillCardList[Int32.Parse(TextArrayList.current.newskillname.Substring(0, 1))][i].name == TextArrayList.current.newskillname.Substring(1))
                            {
                                GameManager.gameManager.SkillCardList[Int32.Parse(TextArrayList.current.newskillname.Substring(0, 1))][i].unLocked = true;

                            }
                        }

                    }
                    else
                    {
                        for (var i = 0; i < ActionReadyGameManager.actionReadyGameManager.NewTotalJobNum; i++)
                        {
                            if (GameManager.gameManager.SkillCardList[GameManager.gameManager.SkillAwakePoolList[Int32.Parse(TextArrayList.current.newskillname.Substring(0, 1))].jobnum][i].name == TextArrayList.current.newskillname.Substring(1))
                            {
                                GameManager.gameManager.SkillCardList[GameManager.gameManager.SkillAwakePoolList[Int32.Parse(TextArrayList.current.newskillname.Substring(0, 1))].jobnum][i].unLocked = true;
                            }
                        }
                    }

                }


                TextArrayList.ToNext();
                Debug.Log("정상");
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                
                Debug.Log("넘침" + TextArrayList.current.character.ToString());
                Debug.Log(TextArrayList.current.character);
                if (TextArrayList.current.character == 3) //마지막 노드일 경우 ////////////////////////////////////////////////////////////////////////////////////////////////////// 정확히 모르겠어 ㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠ
                {
                    Debug.Log(SituationWeightTotal.ToString() + "토탈값" + TextArrayList.tophead[1][StoryEventComplete].next.Count.ToString() + TextArrayList.tophead[1][StoryEventComplete].text);
                    /////  next head로 넘어가는 부분 tophead 이용하여 손대기 
                    //SituationWeight = TextArrayList.current.next[0].weight;
                    var r = random.Next(0, SituationWeightTotal);
                    //if (0 <= r && r < SituationWeight) TextArrayList.ToHead(StoryEventComplete, 0);

                    for (var i = 0; i < TextArrayList.tophead[1][StoryEventComplete].next.Count; i++)
                    {

                        if (SituationWeight <= r && r < SituationWeight + TextArrayList.tophead[1][StoryEventComplete].next[i].weight)
                        {
                            Debug.Log(r.ToString() + "adfasdfasfasdF" + i.ToString());
                            TextArrayList.ToHead(StoryEventComplete, i);
                            Debug.Log(StoryEventComplete.ToString() + i.ToString());
                        }

                        SituationWeight += TextArrayList.tophead[1][StoryEventComplete].next[i].weight;
                    }

                    SituationWeight = 0;

                    //var r = random.Next(0, TextArrayList.tophead[1][StoryEventComplete].next.Count);
                    StoryEventComplete++;

                }
                else if (TextArrayList.current.character == 4) //마지막 노드일 경우 ////////////////////////////////////////////////////////////////////////////////////////////////////// 정확히 모르겠어 ㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠ
                {
                    //                    Debug.Log(SituationWeightTotal.ToString() + "토탈값" + TextArrayList.tophead[1][StoryEventComplete].next.Count.ToString() + TextArrayList.tophead[1][StoryEventComplete].text);
                    /////  next head로 넘어가는 부분 tophead 이용하여 손대기 
                    //SituationWeight = TextArrayList.current.next[0].weight;
                    //                  var r = random.Next(0, SituationWeightTotal);
                    //if (0 <= r && r < SituationWeight) TextArrayList.ToHead(StoryEventComplete, 0);
                    /*
                    for (var i = 0; i < TextArrayList.tophead[1][StoryEventComplete].next.Count; i++)
                    {

                        if (SituationWeight <= r && r < SituationWeight + TextArrayList.tophead[1][StoryEventComplete].next[i].weight)
                        {
                            Debug.Log(r.ToString() + "adfasdfasfasdF" + i.ToString());
                            TextArrayList.ToHead(StoryEventComplete, i);
                            Debug.Log(StoryEventComplete.ToString() + i.ToString());
                        }

                        SituationWeight += TextArrayList.tophead[1][StoryEventComplete].next[i].weight;
                    }


                    SituationWeight = 0;
                    */
                    //var r = random.Next(0, TextArrayList.tophead[1][StoryEventComplete].next.Count);

                    TextArrayList.ToTopHead(1, StoryEventComplete + 1);
                    TextArrayList.ToNext();
                    //StoryEventComplete++;

                }
                else
                {

                    if (!GameObject.Find("no next page"))
                    {
                        ccc = new GameObject();
                        ccc.name = "no next page";
                        ccc.transform.parent = GameObject.Find("Canvas").transform;
                        ccc.AddComponent<Text>();
                        //ccc.GetComponent<Text>().font = arial;
                        ccc.GetComponent<Text>().text = "no next page";
                        ccc.GetComponent<Text>().fontSize = 80;
                        ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
                        ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
                        ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
                        ccc.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1); //원래 0.01f
                    }
                    else
                    {
                        Destroy(ccc);
                    }
                }
            }
            catch (System.NullReferenceException e)
            {
                if (!GameObject.Find("next page error"))
                {
                    ccc = new GameObject();
                    ccc.name = "next page error";
                    ccc.transform.parent = GameObject.Find("Canvas").transform;
                    ccc.AddComponent<Text>();
                    ccc.GetComponent<Text>().font = arial;
                    ccc.GetComponent<Text>().text = "next page error";
                    ccc.GetComponent<Text>().fontSize = 80;
                    ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
                    ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
                    ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
                    ccc.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);  //원래 0.01f
                }
                else
                {
                    Destroy(ccc);
                }
            }
        }
        else
        {
            Debug.Log("elsel ㄴㅁ라ㅓㄴ;ㄹㄴ어람너리;ㅁ널;너;ㅣ넒낭ㄻㄴㄹㅇ;ㅁㄴㅇㄹ");
            try
            {
                if (TextArrayList.current.next[0].possibility == -1)
                {

                    if (TextArrayList.current.next[0].weight == -1) TextArrayList.ToNext();
                    else
                    {
                        Debug.Log("씨발 먼가 이ㅏㅅㅇ해");
                        var r = random.Next(0, SituationWeightTotal);
                        if (0 <= r && r < TextArrayList.current.next[0].weight) TextArrayList.ToNext();
                        for (var i = 0; i < TextArrayList.current.next.Count - 1; i++)
                        {
                            if (TextArrayList.current.next[i].weight <= r && r < TextArrayList.current.next[i + 1].weight) TextArrayList.ToNext(i + 1);
                        }
                    }
                }
                else
                {
                    

                    if (TextArrayList.current.toactionscene)
                    {
                        GameManager.gameManager.TextArrayList = TextArrayList;
                        GameManager.gameManager.StoryEventComplete = StoryEventComplete;
                        UnityEngine.SceneManagement.SceneManager.LoadScene("ActionReady");

                    }
                    else
                    {
                        var r = random.Next(0, 100);
                        if (0 <= r && r < TextArrayList.current.next[0].possibility) TextArrayList.ToNext();
                        for (var i = 0; i < TextArrayList.current.next.Count - 1; i++)
                        {
                            if (TextArrayList.current.next[i].possibility <= r && r < TextArrayList.current.next[i + 1].possibility) TextArrayList.ToNext(i + 1);
                        }
                    }

                    
                }
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (TextArrayList.current.character == 4 || TextArrayList.current.character == 3) //마지막 노드일 경우
                {
                    
                    switch (TextArrayList.current.specialresult)
                    {

                        case 1:
                            LevelVariable++;
                            LevelUpFunction(LevelVariable);

                            break;
                        case 2:
                            break;
                        default:
                            
                            break;

                    }
                    
                    if(TextArrayList.current.ability != "")
                    {
                        if (TextArrayList.current.ability.Substring(0, 1) == "+")
                        {

                            //Debug.Log("지금 봐야할것");
                            //Debug.Log(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1));
                            //Debug.Log(InventoryContentList.IndexOf(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1)));
                            // for(int i=0;i<InventoryContentList.Count;i++) Debug.Log(InventoryContentList[i]);

                            //Debug.Log(InventoryContentList.IndexOf(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1)));
                            if (InventoryContentList.IndexOf(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1)) == -1)
                            {
                                InventoryAdded(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1));
                                InventoryContentList.Add(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1));
                                //   Debug.Log(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1) + "가 추가되었습니다");

                            }


                            //InventoryContentList.find
                        }
                        else if (TextArrayList.current.ability.Substring(0, 1) == "-")
                        {

                            //  Debug.Log("이것이 즈으으응거다");
                            // Debug.Log(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1));

                            var destroyingobjectnum = InventoryContentList.IndexOf(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1));
                            //    Debug.Log(InventoryContentObject.transform.childCount + "+" + destroyingobjectnum+"+"+ InventoryContentList.IndexOf(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1)));
                            //find를 getchild로 바꾸기
                            ////////////////////////////////////////////////////

                            //if (GameObject.Find("InventoryObject" + destroyingobjectnum) != null)
                            if (destroyingobjectnum != -1 && InventoryContentObject.transform.childCount > destroyingobjectnum)
                            {
                                //Debug.Log(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1) + "가 삭제되었습니다");
                                //        Debug.Log("if 통과");

                                if (InventoryContentLocation.x == 160) InventoryContentLocation = new Vector2(0, InventoryContentLocation.y);
                                else if (InventoryContentLocation.x == -160) InventoryContentLocation = new Vector2(160, InventoryContentLocation.y + 70);
                                else if (InventoryContentLocation.x == 0) InventoryContentLocation = new Vector2(-160, InventoryContentLocation.y);

                                //Destroy(GameObject.Find("InventoryObject" + destroyingobjectnum));
                                Destroy(InventoryContentObject.transform.GetChild(destroyingobjectnum).gameObject);

                                //Debug.Log("여기를 봐라 인간" + "InventoryObject" + destroyingobjectnum);
                                //Debug.Log("jsalkfjas;lkjfa;lsdjfa" + (destroyingobjectnum + 1).ToString());
                                InventoryContentList.Remove(TextArrayList.current.ability.Substring(1, TextArrayList.current.ability.Length - 1));
                                /*
                                if (InventoryContentLocation.x == -180) InventoryContentLocation = new Vector2(180, InventoryContentLocation.y + 58);
                                else if (InventoryContentLocation.x == 180) InventoryContentLocation = new Vector2(0, InventoryContentLocation.y);
                                else if (InventoryContentLocation.x == 0) InventoryContentLocation = new Vector2(-180, InventoryContentLocation.y);
                                */
                                for (var i = destroyingobjectnum; i < InventoryContentList.Count; i++)
                                {
                                    //var findingobject = GameObject.Find("InventoryObject" + (i + 1).ToString());
                                    var findingobject = InventoryContentObject.transform.GetChild(i + 1).gameObject;
                                    if (findingobject != null)
                                    {
                                        //         Debug.Log(findingobject.name +"+"+i);
                                        findingobject.name = "InventoryObject" + i;

                                        //       Debug.Log(findingobject.transform.localPosition.x);

                                        if (findingobject.transform.localPosition.x == 90) findingobject.transform.localPosition = new Vector2(410, findingobject.transform.localPosition.y + 70);
                                        else if (findingobject.transform.localPosition.x == 410) findingobject.transform.localPosition = new Vector2(250, findingobject.transform.localPosition.y);
                                        else if (findingobject.transform.localPosition.x == 250) findingobject.transform.localPosition = new Vector2(90, findingobject.transform.localPosition.y);


                                    }

                                }

                            }


                            
                        }
                    }

                    /*if (TextArrayList.current.toactionscene != "")
                    {


                    }*/
                    

                    


                        /////  next head로 넘어가는 부분 tophead 이용하여 손대기 
                        //SituationWeight = TextArrayList.current.next[0].weight;

                        var r = random.Next(0, SituationWeightTotal);
                    Debug.Log("ASdfasdfas" + StoryEventComplete.ToString() + "Asdfasdfa");
                    //if (0 <= r && r < SituationWeight) TextArrayList.ToHead(StoryEventComplete, 0);
                    for (var i = 0; i < TextArrayList.tophead[1][StoryEventComplete - 1].next.Count; i++)
                    {
                        if (SituationWeight <= r && r < SituationWeight + TextArrayList.tophead[1][StoryEventComplete - 1].next[i].weight) TextArrayList.ToHead(StoryEventComplete - 1, i);

                        SituationWeight += TextArrayList.tophead[1][StoryEventComplete - 1].next[i].weight;
                    }

                    SituationWeight = 0;

                    //var r = random.Next(0, TextArrayList.tophead[1][StoryEventComplete].next.Count);

                }
                else
                {

                    if (!GameObject.Find("no next page"))
                    {
                        ccc = new GameObject();
                        ccc.name = "no next page";
                        ccc.transform.parent = GameObject.Find("Canvas").transform;
                        ccc.AddComponent<Text>();
                        //ccc.GetComponent<Text>().font = arial;
                        ccc.GetComponent<Text>().text = "no next page";
                        ccc.GetComponent<Text>().fontSize = 80;
                        ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
                        ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
                        ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
                        ccc.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1); //원래 0.01f
                    }
                    else
                    {
                        Destroy(ccc);
                    }
                }
            }
            catch (System.NullReferenceException e)
            {
                if (!GameObject.Find("next page error"))
                {
                    ccc = new GameObject();
                    ccc.name = "next page error";
                    ccc.transform.parent = GameObject.Find("Canvas").transform;
                    ccc.AddComponent<Text>();
                    ccc.GetComponent<Text>().font = arial;
                    ccc.GetComponent<Text>().text = "next page error";
                    ccc.GetComponent<Text>().fontSize = 80;
                    ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
                    ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
                    ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
                    ccc.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);  //원래 0.01f
                }
                else
                {
                    Destroy(ccc);
                }
            }
        }
        ButtonUpdateFunction();
    }

    private void BeforeButtonFunction()
    {
        /*
        if (!GameObject.Find("beforebutton active test text"))
        {
            ccc = new GameObject();
            ccc.name = "beforebutton active test text";
            ccc.transform.parent = GameObject.Find("Canvas").transform;
            ccc.AddComponent<Text>();
            ccc.GetComponent<Text>().font = arial;
            ccc.GetComponent<Text>().text = "beforebutton pressed";
            ccc.GetComponent<Text>().fontSize = 80;
            ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
            ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
            ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
            ccc.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 1);
        }
        else
        {
            Destroy(ccc);
        }*/
        try
        {
            if (TextArrayList.current.before != null)
            {
                if (TextArrayList.current.character == 3)
                {
                    //Debug.Log( TextArrayList.current.before.text);
                    for (var i = 0; i < TextArrayList.current.before.next.Count; i++)
                    {
                        TextArrayList.current.before.next[i].BeforeNextButtonAble = false;
                    }
                    TextArrayList.current.BeforeNextButtonAble = true;
                    //Debug.Log("ajafdfds;lkjfdaskjl;afdskjlfdaskjl;fasdk;ljfasdkjlf;asdkfd");
                }
                TextArrayList.ToBefore();
                //Debug.Log("ajafdfds;lkjfdaskjl;afdskjlfdaskjl;fasdk;ljfasdkjlf;asdkfd");
            }
            else
            {
                //Debug.Log("++++++++++++++++++++++++");
                if (!GameObject.Find("no before page"))
                {
                    ccc = new GameObject();
                    ccc.name = "no before page";
                    ccc.transform.parent = GameObject.Find("Canvas").transform;
                    ccc.AddComponent<Text>();
                    //ccc.GetComponent<Text>().font = arial;
                    ccc.GetComponent<Text>().text = "no before page";
                    ccc.GetComponent<Text>().fontSize = 80;
                    ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
                    ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
                    ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
                    ccc.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1); //원래 0.01f
                }
                else
                {
                    Destroy(ccc);
                }
            }
        }
        catch (System.NullReferenceException e)
        {
            //Debug.Log("-----------");
            if (!GameObject.Find("no before page"))
            {
                ccc = new GameObject();
                ccc.name = "no before page";
                ccc.transform.parent = GameObject.Find("Canvas").transform;
                ccc.AddComponent<Text>();
                //ccc.GetComponent<Text>().font = arial;
                ccc.GetComponent<Text>().text = "no before page";
                ccc.GetComponent<Text>().fontSize = 80;
                ccc.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
                ccc.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
                ccc.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
                ccc.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1); //원래 0.01f
            }
            else
            {
                Destroy(ccc);
            }
        }
        ButtonUpdateFunction();
    }
    private void ChoiceButton1Function()
    {
        try
        {
            for (var i = 0; i < TextArrayList.current.next.Count; i++)
            {
                TextArrayList.current.next[i].BeforeNextButtonAble = true;
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("beforenextbuttonable 에 에러난거니까 확인해봐 시발");
        }


        try
        {
            TextArrayList.ToNext(0);
        }
        catch (System.ArgumentOutOfRangeException e)
        {

        }
        catch (System.NullReferenceException e)
        {

        }
        ButtonUpdateFunction();
    }
    private void ChoiceButton2Function()
    {
        try
        {
            for (var i = 0; i < TextArrayList.current.next.Count; i++)
            {
                TextArrayList.current.next[i].BeforeNextButtonAble = true;
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("beforenextbuttonable 에 에러난거니까 확인해봐 시발");
        }

        try
        {
            TextArrayList.ToNext(1);
        }
        catch (System.ArgumentOutOfRangeException e)
        {

        }
        catch (System.NullReferenceException e)
        {

        }
        ButtonUpdateFunction();
    }
    private void ChoiceButton3Function()
    {
        try
        {
            for (var i = 0; i < TextArrayList.current.next.Count; i++)
            {
                TextArrayList.current.next[i].BeforeNextButtonAble = true;
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("beforenextbuttonable 에 에러난거니까 확인해봐 시발");
        }

        try
        {
            TextArrayList.ToNext(2);
        }
        catch (System.ArgumentOutOfRangeException e)
        {

        }
        catch (System.NullReferenceException e)
        {

        }
        ButtonUpdateFunction();
    }

    private void InventoryAdded(string AddingContentText)
    {

        //Debug.Log("addging text" + " " + AddingContentText);

        Text InventoryContents = Instantiate(InventoryText) as Text;


        InventoryContents.name = "InventoryObject" + InventoryContentList.Count;
        InventoryContents.text = AddingContentText;

        //if (InventoryContentList.Count == 0) InventoryContentLocation = new Vector2(-160, 160);



        if (InventoryContentLocation.x == 160) InventoryContentLocation = new Vector2(-160, InventoryContentLocation.y - 70);
        else if (InventoryContentLocation.x == -160) InventoryContentLocation = new Vector2(0, InventoryContentLocation.y);
        else if (InventoryContentLocation.x == 0) InventoryContentLocation = new Vector2(160, InventoryContentLocation.y);

        InventoryContents.transform.localPosition = InventoryContentLocation;

        //InventoryContents.transform.localPosition = InventoryContentLo6r cation;

        if (InventoryScrollView != null)
        {
            InventoryContents.transform.SetParent(InventoryContentObject, false);
        }
        /*
        if (InventoryContentLocation.x == 180) InventoryContentLocation = new Vector2(-180, InventoryContentLocation.y - 58);
        else if (InventoryContentLocation.x == -180) InventoryContentLocation = new Vector2(0, InventoryContentLocation.y);
        else if (InventoryContentLocation.x == 0) InventoryContentLocation = new Vector2(180, InventoryContentLocation.y);
        */

    }

    private bool StoryEventCompletion(int n)
    {
        Debug.Log(n.ToString() + "컴플릿션");
        //if (aaaaaa > 20) StartActionSceneFunction();
        switch (n)
        {
            case 1:
                if (StoryEventComplete == 0)
                {
                    SituationWeightTotal = 0;
                    for (var i = 0; i < TextArrayList.tophead[1][0].next.Count; i++) SituationWeightTotal += TextArrayList.tophead[1][0].next[i].weight;
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            case 2:
                if (LevelVariable > 3)
                {

                    SituationWeightTotal = 0;
                    for (var i = 0; i < TextArrayList.tophead[1][1].next.Count; i++) SituationWeightTotal += TextArrayList.tophead[1][1].next[i].weight;
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            case 3:
                if (LevelVariable > 3)
                {

                    SituationWeightTotal = 0;
                    for (var i = 0; i < TextArrayList.tophead[1][1].next.Count; i++) SituationWeightTotal += TextArrayList.tophead[1][1].next[i].weight;
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            case 4:
                if (LevelVariable > 10)
                {

                    SituationWeightTotal = 0;
                    for (var i = 0; i < TextArrayList.tophead[1][1].next.Count; i++) SituationWeightTotal += TextArrayList.tophead[1][1].next[i].weight;
                    return true;
                }
                else
                {
                    return false;
                }

                break;

            default:

                break;

        }

        Debug.Log(n.ToString() + "뭔가 문제가 생겼는걸");
        return false;
    }

    IEnumerator EXPSliderIncreaseFunction(float eee)
    {
        if (eee > 0)
        {
            for (var i = 0; i < 40; i++)
            {
                EXPParent.GetChild(1).GetComponent<Image>().fillAmount += eee / 40;
                yield return new WaitForSeconds(0.05f);
            }

        }
        else if (eee < 0)
        {
            float fff = 1f - EXPParent.GetChild(1).GetComponent<Image>().fillAmount;

            for (var i = 0; i < 40; i++)
            {
                EXPParent.GetChild(1).GetComponent<Image>().fillAmount += fff / 40;
                yield return new WaitForSeconds(0.05f);
            }
         
            EXPParent.GetChild(1).GetComponent<Image>().fillAmount = 0f;
            yield return new WaitForSeconds(0.1f);

            fff = ExpIntVariable / 100f/LevelVariable;

            for (var i = 0; i < 40; i++)
            {
                EXPParent.GetChild(1).GetComponent<Image>().fillAmount += fff / 40;
                yield return new WaitForSeconds(0.05f);
            }

            
        }


    }

    private void ButtonUpdateFunction()
    {
        //storytext.font = arial;

        ExpIntVariable += TextArrayList.current.exp;
        int currentlevel = LevelVariable;
        if (ExpIntVariable >= 100*LevelVariable)
        {
            ExpIntVariable -= 100 * LevelVariable;
            LevelVariable++;
            LevelUpFunction(LevelVariable);
        }

        if (EXPParent.GetChild(2).GetComponent<Text>().text.ToString() != ExpIntVariable.ToString())
        {
            float eee = ExpIntVariable/(currentlevel) / 100f - EXPParent.GetChild(1).GetComponent<Image>().fillAmount;

            StartCoroutine(EXPSliderIncreaseFunction(eee));


            EXPParent.GetChild(2).GetComponent<Text>().text = ExpIntVariable.ToString();
        }


        SkillPointButton.transform.GetChild(0).GetComponent<Text>().text = SkillAvailableVariable.ToString();
        StatPointButton.transform.GetChild(0).GetComponent<Text>().text = StatAvailableVariable.ToString();

        LevelButton.transform.GetChild(0).GetComponent<Text>().text = LevelVariable.ToString();
        //EXPButton.transform.GetChild(0).GetComponent<Text>().text = ExpIntVariable.ToString();

        //Debug.Log(data.Count.ToString());
        try
        {
            storytext.text = TextArrayList.current.text;
        }
        catch (System.NullReferenceException e)
        {
            storytext.text = "no before page";
        }


        storytext.fontSize = 80;
        storytext.alignment = TextAnchor.UpperCenter;
        storytext.GetComponent<RectTransform>().localPosition = new Vector3(0, 1, 0);
        storytext.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 200);
        storytext.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1); //원래 0.01f

        /*
     if (!GameObject.Find("sssssss"))
        {
            cccd = new GameObject();
            cccd.name = "sssssss";
            cccd.transform.parent = GameObject.Find("Canvas").transform;
            cccd.AddComponent<Text>();
            cccd.GetComponent<Text>().font = arial;
            cccd.GetComponent<Text>().text = TextArrayList.current.possibility.ToString();
            cccd.GetComponent<Text>().fontSize = 80;
            cccd.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
            cccd.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            cccd.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
            cccd.GetComponent<RectTransform>().localScale = new Vector3(0.005f, 0.01f, 1);
        }
        else
        {
            cccd.GetComponent<Text>().text = TextArrayList.current.possibility.ToString();
        } 
     
     */
        // Debug.Log(TextArrayList.current.text);
        if (TextArrayList.current.character == 2)
        {
            NextButton.transform.localScale = new Vector3(0, 0, 0);
            //Debug.Log("current next count"+TextArrayList.current.next.Count);
            if (TextArrayList.current.next.Count > 0)
            {
                if (TextArrayList.current.next[0].BeforeNextButtonAble)
                {
                    ChoiceButton1.transform.localScale = new Vector3(1f, 1f, 1);
                    ChoiceButton1.GetComponentInChildren<Text>().text = TextArrayList.current.next[0].requirement + TextArrayList.current.next[0].text;
                }
            }
            if (TextArrayList.current.next.Count > 1)
            {
                if (TextArrayList.current.next[1].BeforeNextButtonAble)
                {
                    ChoiceButton2.transform.localScale = new Vector3(1f, 1f, 1);
                    ChoiceButton2.GetComponentInChildren<Text>().text = TextArrayList.current.next[1].requirement + TextArrayList.current.next[1].text;
                }
            }
            if (TextArrayList.current.next.Count > 2)
            {
                if (TextArrayList.current.next[2].BeforeNextButtonAble)
                {
                    ChoiceButton3.transform.localScale = new Vector3(1f, 1f, 1);
                    ChoiceButton3.GetComponentInChildren<Text>().text = TextArrayList.current.next[2].requirement + TextArrayList.current.next[2].text;
                }
            }

        }
        else
        {
            NextButton.transform.localScale = new Vector3(1, 1, 1);
            ChoiceButton1.transform.localScale = new Vector3(0, 0, 0);
            ChoiceButton2.transform.localScale = new Vector3(0, 0, 0);
            ChoiceButton3.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private void SkillButtonFunction()
    {
        SkillParent.localScale = new Vector2(1f - SkillParent.localScale.x, 1f - SkillParent.localScale.y);
        StatParent.localScale = new Vector2(0, 0);
    }

    private void StatButtonFunction()
    {
        StatParent.localScale = new Vector2(1f - StatParent.localScale.x, 1f - StatParent.localScale.y);
        SkillParent.localScale = new Vector2(0, 0);
    }

    private void EachSkillUpButtonFunction(int n)
    {
        //  Debug.Log("asdfasfsadfasdfsafsafasdf" + n.ToString());
        if (SkillAvailableVariable > 0)
        {
            SkillNumArray[n]++;
            SkillAvailableVariable--;
        }
        SkillButtonArray[n].transform.GetChild(0).GetComponent<Text>().text = SkillNumArray[n].ToString();
        ButtonUpdateFunction();
    }

    private void EachStatUpButtonFunction(int n)
    {
        if (StatAvailableVariable > 0)
        {
            StatNumArray[n]++;
            StatAvailableVariable--;
        }
        StatButtonArray[n].transform.GetChild(0).GetComponent<Text>().text = StatNumArray[n].ToString();
        ButtonUpdateFunction();
    }
    private void LevelUpFunction(int afterlevel)
    {
        StatAvailableVariable++;
        SkillAvailableVariable++;
    }
    /*
    private void StartActionSceneFunction()
    {
       // GameManager.gameManager.GameManagerTextArrayList = TextArrayList;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Action",UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }*/

    private void JobUnlockFunction(int jobnum)
    {
        GameManager.gameManager.SkillCardList.Add(GameManager.gameManager.SkillAwakePoolList[jobnum].EachChildSkillList);
        GameManager.gameManager.SkillAwakePoolList[jobnum].jobunlocked = true;
        GameManager.gameManager.NewTotalJobNum++;
        GameManager.gameManager.SkillAwakePoolList[jobnum].jobnum = GameManager.gameManager.NewTotalJobNum;
        GameManager.gameManager.SaveCreatingNewJobButtonFunction(GameManager.gameManager.SkillAwakePoolList[jobnum].jobname);

    }

}
