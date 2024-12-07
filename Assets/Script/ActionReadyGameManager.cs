using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class ActionReadyGameManager : MonoBehaviour
{
    //private const int 카드레디뷰순서 = 1;
    /*
     * 법사 0
     * 전사 1
     * 사제 2
     */

    public static ActionReadyGameManager actionReadyGameManager = null;

    public Canvas MainCanvas;

    public Canvas BoardCanvas;

    public Canvas ddd;

    //public Button Card;
    public SpriteRenderer Board;

    public Transform CardReadyViewRect;

    public GameObject CardPrefab;
    private GameObject CardObject;

    public List<List<GameObject>> CardReadyList = new List<List<GameObject>>(); //전체 카드 리스트
    public List<GameObject> TempCardReadyList = new List<GameObject>();
    //public List<bool> CardChosenBoolList = new List<bool>(); //전체 카드 리스트에서 카드가 픽 되었는지 여부

    public Button[] JobButton=new Button[1];

    public Button JobButtonPrefab;

    public int ChosenCardNum = 0;
    public int CurrentJobNum = 0;
    public int NewTotalJobNum = 0;
    //public int CurrentTotalJobNum = 0; //너무 actionreadyscene 로드마다 오래 걸릴 경우 사용할 변수
    

    private int[] SkillNumOfJob;

    public Button NextButton;

    private Vector2 originPos;

    //public int myType;
    //private GameObject blocks;

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

        if (actionReadyGameManager == null)
        {
            actionReadyGameManager = this;
        }
        else if (actionReadyGameManager != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(BoardCanvas);
    }

    // Start is called before the first frame update
    void Start()
    {
        NewTotalJobNum = GameManager.gameManager.NewTotalJobNum;
        Debug.Log(NewTotalJobNum + "DDDDDDD");

        for (var i = 0; i < GameManager.gameManager.NewTotalJobNum; i++)
        {
            Debug.Log("음음음음;;");
            CreateNewJobButtonFunction(GameManager.gameManager.HowManyJobButton[i]);
        }

        ResetReadyGameScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetReadyGameScene()
    {
        int n = 0;
        CardReadyList = new List<List<GameObject>>(); //전체 카드 리스트
        TempCardReadyList = new List<GameObject>();
        //CardChosenBoolList = new List<bool>(); //전체 카드 리스트에서 카드가 픽 되었는지 여부
        SkillNumOfJob = new int[NewTotalJobNum];
        Debug.Log(NewTotalJobNum + "SSSSS");
        
        
        
        for (var i = 0; i < NewTotalJobNum; i++) SkillNumOfJob[i] = 0;
        Debug.Log(GameManager.gameManager.SkillCardList.Count);

        for (var j = 0; j < NewTotalJobNum; j++)
        {
            for (var i = 0; i < GameManager.gameManager.SkillCardList[j].Count; i++)
            {
                Debug.Log("D");
                if (GameManager.gameManager.SkillCardList[j][i].unLocked)
                {
                    if (GameManager.gameManager.SkillCardList[j][i].inDeck)
                    {

                        GameObject TemporaryCardObject = Instantiate(CardPrefab) as GameObject;

                        TemporaryCardObject.transform.SetParent(Board.transform, false);


                        TemporaryCardObject.transform.localScale = new Vector2(1, 2);
                        TemporaryCardObject.transform.localPosition = new Vector2(SkillNumOfJob[j] - 2, 0);
                        SkillNumOfJob[j]++;
                        TemporaryCardObject.transform.GetChild(1).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].name;
                        TemporaryCardObject.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].damage.ToString();
                        TemporaryCardObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].cost.ToString();
                        TemporaryCardObject.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].heal.ToString();

                        TemporaryCardObject.GetComponent<CardScript>().ClassofthisCard = GameManager.gameManager.SkillCardList[j][i];

                        TempCardReadyList.Add(TemporaryCardObject);
                        //CardChosenBoolList.Add(true);
                        GameManager.gameManager.SkillCardList[j][i].inDeck = true;



                    }
                    else
                    {

                        GameObject TemporaryCardObject = Instantiate(CardPrefab) as GameObject;

                        TemporaryCardObject.transform.SetParent(CardReadyViewRect.GetChild(j), false);

                        TemporaryCardObject.transform.localScale = new Vector2(100, 100);
                        TemporaryCardObject.transform.localPosition = new Vector2((SkillNumOfJob[j] % 3) * 150 - 150, (SkillNumOfJob[j] / 3) * (-150) + 150);
                        SkillNumOfJob[j]++;
                        TemporaryCardObject.transform.GetChild(1).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].name;
                        TemporaryCardObject.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].damage.ToString();
                        TemporaryCardObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].cost.ToString();
                        TemporaryCardObject.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.SkillCardList[j][i].heal.ToString();

                        TemporaryCardObject.GetComponent<CardScript>().ClassofthisCard = GameManager.gameManager.SkillCardList[j][i];

                        TempCardReadyList.Add(TemporaryCardObject);
                        //CardChosenBoolList.Add(false);
                        GameManager.gameManager.SkillCardList[j][i].inDeck = false;

                    }
                }



                //    Debug.Log(i);

                //TemporaryCardReadyListText.transform.SetParent(CardReadyList[i].transform.GetChild(0), false);
            }

            CardReadyList.Add(TempCardReadyList);
            TempCardReadyList = new List<GameObject>();


        }



        /*
        CardReadyList[0].GetComponent<Button>().onClick.AddListener(delegate { CardButtonFunction(0); });
        CardReadyList[1].GetComponent<Button>().onClick.AddListener(delegate { CardButtonFunction(1); });
        CardReadyList[2].GetComponent<Button>().onClick.AddListener(delegate { CardButtonFunction(2); });
        CardReadyList[3].GetComponent<Button>().onClick.AddListener(delegate { CardButtonFunction(3); });
        CardReadyList[4].GetComponent<Button>().onClick.AddListener(delegate { CardButtonFunction(4); });
        CardReadyList[5].GetComponent<Button>().onClick.AddListener(delegate { CardButtonFunction(5); });
        */

        for (var k = 0; k < NewTotalJobNum; k++)
        {
            for (var i = 0; i < SkillNumOfJob[k]; i++)
            {
                var j = i;
                var q = k;
                CardReadyList[q][j].GetComponent<Button>().onClick.AddListener(delegate { CardButtonFunction(q, j); });
            }
        }

        for (var i = 0; i < NewTotalJobNum; i++)
        {
            
            var j = i;
            Debug.Log(j);
            Debug.Log(NewTotalJobNum);
            JobButton[j].onClick.AddListener(delegate { JobButtonFunction(j); });
        }



        //GameManager.gameManager
        CardReadyViewRect.localScale = new Vector2(1, 1);

        NextButton.onClick.AddListener(NextButtonFunction);
        CardReadyViewRect.localScale = new Vector2(0.01f, 0.01f);

        for (var i = 0; i < NewTotalJobNum; i++)
        {
            CardReadyViewRect.GetChild(i).localScale = new Vector2(0, 0);
        }
        CardReadyViewRect.GetChild(0).localScale = new Vector2(1, 1);

        JobButton[0].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex());
        for(var i = 1; i < NewTotalJobNum; i++)
        {
            var j = i;
            JobButton[j].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex() + 1);
        }
        //JobButton[1].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex() + 1);
        //JobButton[2].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex() + 1);
    }

    public void CardButtonFunction(int m, int n)
    {
        Debug.Log("갸갸갸갸갸갸+   " + CurrentJobNum);
        Debug.Log(n);

        if (ChosenCardNum >= 5)
        {
            Debug.Log("여기를 보자" + m + "    DDDD    " + n);
            if (GameManager.gameManager.SkillCardList[m][n].inDeck)
            {
                Debug.Log("FFFFFFFFFF");
                CardReadyList[m][n].transform.SetParent(CardReadyViewRect.transform.GetChild(m).transform, false);
                CardReadyList[m][n].transform.localPosition = new Vector2((n % 3) * 150 - 150, (n / 3) * (-150) + 150);
                CardReadyList[m][n].transform.localScale = new Vector2(100, 100);
                Debug.Log("FFFFFFFFFF");
                ChosenCardNum--;

                for (var i = 0; i < ChosenCardNum; i++)
                {
                    Board.transform.GetChild(i).localPosition = new Vector2(i - 2, 0);
                }
                Debug.Log("FFFFFFFFFF");
                GameManager.gameManager.SkillCardList[m][n].inDeck = false;
                
            }
            else
            {
                Debug.Log("카드 5개 다 고름");
                
            }
        }
        else
        {

            if (GameManager.gameManager.SkillCardList[m][n].inDeck)
            {
                Debug.Log("GGGGGGGG");
                CardReadyList[m][n].transform.SetParent(CardReadyViewRect.transform.GetChild(m).transform, false);
                CardReadyList[m][n].transform.localPosition = new Vector2((n % 3) * 150 - 150, (n / 3) * (-150) + 150);
                CardReadyList[m][n].transform.localScale = new Vector2(100, 100);
                Debug.Log("GGGGGGGG");
                ChosenCardNum--;

                for (var i = 0; i < ChosenCardNum; i++)
                {
                    Board.transform.GetChild(i).localPosition = new Vector2(i - 2, 0);
                }
                Debug.Log("GGGGGGGG");
                GameManager.gameManager.SkillCardList[m][n].inDeck = false;
            }
            else
            {
                Debug.Log("HHHHH");
                CardReadyList[m][n].transform.SetParent(Board.transform, false);
                CardReadyList[m][n].transform.localPosition = new Vector2(ChosenCardNum - 2, 0);
                CardReadyList[m][n].transform.localScale = new Vector2(1, 2);
                ChosenCardNum++;
                Debug.Log("HHHHH");
                GameManager.gameManager.SkillCardList[m][n].inDeck = true;
                Debug.Log("HHHHH");
            }

        }

    }
    void NextButtonFunction()
    {
        /*
        GameManager.gameManager.CardDeckList= new List<bool>();
        for (var i=0; i < GameManager.gameManager.CardTextList.Count; i++)
        {
            GameManager.gameManager.CardDeckList.Add(CardChosenBoolList[i]);
        }            
        */

        //DontDestroyOnLoad(BoardCanvas);

        
        for(var i = 0; i < Board.transform.childCount; i++)
        {
            GameManager.gameManager.ChosenCardList.Add(Board.transform.GetChild(i).GetComponent<CardScript>().ClassofthisCard);
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Action");
        Debug.Log("ready to action");

        //UnityEngine.SceneManagement.SceneManager.LoadScene("Action", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        //UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("Action"));
        
    }

    public void JobButtonFunction(int n)
    {
        Debug.Log("이건 어떨가     " + n);
        CurrentJobNum = n;
        JobButton[n].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex());
        
        for (var i = 0; i < NewTotalJobNum; i++)
        {
            if(i!=n) JobButton[i].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex()+1);

            CardReadyViewRect.GetChild(i).localScale = new Vector2(0, 0);
        }
        CardReadyViewRect.GetChild(n).localScale = new Vector2(1, 1);
    }

    public void CreateNewJobButtonFunction(string s)
    {
        
        
        JobButton = new Button[NewTotalJobNum];
        for (var i = 0; i < NewTotalJobNum-1; i++)
        {
            Debug.Log("하핳");
            var j = i;
            Debug.Log(NewTotalJobNum + "sfjsklfjsakldjflkasjdflaksdfj" + j);
            JobButton[j] = MainCanvas.transform.GetChild(j + 2).GetComponent<Button>();
        }
        Debug.Log("앗흥");



        Button TempJobButton= Instantiate(JobButtonPrefab) as Button;
        JobButton[NewTotalJobNum - 1] = TempJobButton;
        JobButton[NewTotalJobNum - 1].transform.SetParent(MainCanvas.transform);
        if (NewTotalJobNum==1) JobButton[NewTotalJobNum - 1].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex());
        else JobButton[NewTotalJobNum - 1].transform.SetSiblingIndex(CardReadyViewRect.GetSiblingIndex() + 1);
        JobButton[NewTotalJobNum - 1].name = s+"Button";
        JobButton[NewTotalJobNum - 1].transform.GetChild(0).GetComponent<Text>().text = s;
        JobButton[NewTotalJobNum - 1].transform.localPosition=new Vector2(2.5f,(4.5f-0.5f*NewTotalJobNum));
        Debug.Log(JobButton[0].name);
    }

    /*
    private void OnMouseDown()
    {
        originPos = Card.transform.position;
        //Sample_BoardManager.instance.holdingBlock = blocks;
    }

    private void OnMouseDrag()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Card.transform.position = mousePos;
    }

    private void OnMouseUp()
    {



        if (Card.transform.position.y > -1.5f)
        {
            Destroy(Card);
        }
        else
        {
            Card.transform.position = originPos;
        }



        
    }*/
}
