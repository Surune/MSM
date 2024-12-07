using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;

[System.Serializable]

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public static GameManager instance;

    public static GameManager gameManager = null;

    /*
    public List<string> CardTextList = new List<string>();
    public List<int[]> CardIntList = new List<int[]>();

    public List<bool> CardDeckList = new List<bool>();
    */
   // public List<SkillPoolClass> SkillPoolList = new List<SkillPoolClass>();


    public List<SkillCardClass> EachJobCardList = new List<SkillCardClass>();

    public List<List<SkillCardClass>> SkillCardList = new List<List<SkillCardClass>>();

    public List<SkillCardClass> ChosenCardList = new List<SkillCardClass>();

    public bool CardUsed = false;
    public int CardListNum = 0;

    public int NewTotalJobNum = 0;



    public List<SkillPoolClass> SkillAwakePoolList = new List<SkillPoolClass>();
    public int TotalPoolJobNum = 3;
    public List<string> HowManyJobButton = new List<string>();


    public ArrayListHeader TextArrayList = new ArrayListHeader();
    public int StoryEventComplete = 0;

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



    /*
     * Illust 0
     * Text 1
     * Damage 2
     * Cost 3
     * Heal 4
     */

    private void Awake()
    {
        Screen.SetResolution(720, 1080, true);

        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);         
        }

        DontDestroyOnLoad(gameManager);

        Debug.Log("GameManager Start");

        SkillAwakePoolList.Add(new SkillPoolClass("법사"));
        SkillAwakePoolList.Add(new SkillPoolClass("전사"));
        SkillAwakePoolList.Add(new SkillPoolClass("사제"));

        SkillAwakePoolList[0].CreateSkillFunction("FireBall", 1, 1, 0);
        SkillAwakePoolList[0].CreateSkillFunction("Lightning", 2, 1, 0);
        SkillAwakePoolList[0].CreateSkillFunction("Blizzard", 3, 2, 0);
        SkillAwakePoolList[0].CreateSkillFunction("MindControl", 4, 3, 0);
        SkillAwakePoolList[0].CreateSkillFunction("SandBurst", 5, 4, 0);
        SkillAwakePoolList[0].CreateSkillFunction("SunShine", 0, 5, 10);

        SkillAwakePoolList[1].CreateSkillFunction("Strike", 1, 1, 0);
        SkillAwakePoolList[1].CreateSkillFunction("Double Strike", 2, 2, 0);
        SkillAwakePoolList[1].CreateSkillFunction("Swing", 3, 3, 0);
        SkillAwakePoolList[1].CreateSkillFunction("Kill", 4, 4, 0);
        SkillAwakePoolList[1].CreateSkillFunction("Taunt", 5, 5, 0);
        SkillAwakePoolList[1].CreateSkillFunction("Self Heal", 5, 10, 0);

        SkillAwakePoolList[2].CreateSkillFunction("Heal", 1, 1, 0);
        SkillAwakePoolList[2].CreateSkillFunction("Holy Magic", 2, 2, 0);
        SkillAwakePoolList[2].CreateSkillFunction("Summon Angle", 3, 3, 0);
        SkillAwakePoolList[2].CreateSkillFunction("Reseraction", 4, 4, 0);
        SkillAwakePoolList[2].CreateSkillFunction("Dark Magic", 5, 5, 0);
        SkillAwakePoolList[2].CreateSkillFunction("Silence", 5, 10, 0);


        // SkillPoolList = CreateSkillPoolScript.createskillpoolscript.SkillAwakePoolList;


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

                    TextArrayList.Add(values[필요조건], values[선택지], values[액션여부]);
                    if (!TextArrayList.current.toactionscene) ////.Equals를 써야 될수도 있음
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

                            TextArrayList.Add(Int32.Parse(values[결과확률]), values[결과], Int32.Parse(values[체력]), Int32.Parse(values[멘탈]), Int32.Parse(values[돈]), values[능력치], Int32.Parse(values[경험치]), values[스킬습득], Int32.Parse(values[특별]));
                            values = Regex.Split(lines[++i], @",#");
                            TextArrayList.ToBefore();
                        }
                        TextArrayList.ToBefore();
                    }
                    else if (TextArrayList.current.toactionscene)
                    {
                        int forvaluesvariabletemp2 = Int32.Parse(values[선택지가짓수]);
                        if (forvaluesvariabletemp2 == -2)
                        {
                            for (var k = 0; k < 2; k++)
                            {
                                TextArrayList.Add(Int32.Parse(values[결과확률]), values[결과], Int32.Parse(values[체력]), Int32.Parse(values[멘탈]), Int32.Parse(values[돈]), values[능력치], Int32.Parse(values[경험치]), values[스킬습득], Int32.Parse(values[특별]));
                                values = Regex.Split(lines[++i], @",#");
                                TextArrayList.ToNext();
                            }
                            TextArrayList.ToBefore();

                        }
                        else if (forvaluesvariabletemp2 > 0)
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
        Debug.Log("FFFFFFFFFFFF" + ivalues[넘버링] + "Ggggggg" + ivalues[상황비중]);
        TextArrayList.tophead.Add(new Node[Int32.Parse(ivalues[넘버링])]);
        TextArrayList.tophead.Add(new Node[Int32.Parse(ivalues[상황비중])]);
        //Debug.Log("FFFFFFFFFFFF");


        //StoryEventComplete = new bool[Int32.Parse(ivalues[넘버링])];

        //for (var i = 0; i < Int32.Parse(ivalues[넘버링]); i++) StoryEventComplete[i] = false;


        for (var i = 0; i < TextArrayList.head.Count; i++)
        {
            TextArrayList.Add(TextArrayList.head[i].character, TextArrayList.head[i].numbering, TextArrayList.head[i]);
        }

        TextArrayList.ToTopHead(1, 1);
        TextArrayList.ToNext();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCreatingNewJobButtonFunction(string s)
    {
        HowManyJobButton.Add(s);
    }

}
