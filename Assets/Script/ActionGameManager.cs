using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class ActionGameManager : MonoBehaviour
{
    public static ActionGameManager actionGameManager = null;

    public Button NextButton;

    //public Transform CardReadyViewRect;
    public SpriteRenderer Board;

    public SpriteRenderer StopCardDragBoard;

    public GameObject CardPrefab;

    private Vector2 originPos;

    public Button ManaButton;
    public Button MyHealthButton;
    public Button EnemyHealthButton;

    public Button OpenSkillListButton;

    public GameObject SkillListObject;

    public List<GameObject> EnemySkillList = new List<GameObject>();

    public GameObject PlayerUIObject;
    public GameObject EnemyUIObject;

    /*
     * health
     * mana
     * lv
     * exp
     * state
     */
    private bool WonThisRound = false;
    public int ManaInt=20;
    public int EnemyHealthInt=20;
    public int MyHealthInt=20;

    private List<GameObject> DeckCardList = new List<GameObject>();
    
    public CharacterUIClass PlayerUIClass= new CharacterUIClass();
    public CharacterUIClass EnemyUIClass = new CharacterUIClass();

    float timer=0f;
    int waitingTime=5;
    /*
     * Illust 0
     * Text 1
     * Damage 2
     * Cost 3
     * Heal 4
     */
    // Start is called before the first frame update

    private void Awake()
    {
        Screen.SetResolution(720, 1080, true);
        
        if (actionGameManager == null)
        {
            actionGameManager = this;
        }
        else if (actionGameManager != this)
        {
            Destroy(gameObject);
        }

        ManaButton.transform.localScale = new Vector2(0, 0);
        MyHealthButton.transform.localScale = new Vector2(0, 0);
        EnemyHealthButton.transform.localScale = new Vector2(0, 0);

    }
    void Start()
    {
        GameManager.gameManager.CardListNum = 0;

        for (var i = 0; i < GameManager.gameManager.ChosenCardList.Count; i++)
        {
            GameManager.gameManager.CardListNum++;
            GameObject TemporaryCardObject = Instantiate(CardPrefab) as GameObject;


            TemporaryCardObject.transform.SetParent(Board.transform, false);

            TemporaryCardObject.transform.localPosition = new Vector2(i - 2, 0);
            TemporaryCardObject.transform.localScale = new Vector2(1, 2);

            TemporaryCardObject.transform.GetChild(1).GetComponent<Text>().text = GameManager.gameManager.ChosenCardList[i].name;
            TemporaryCardObject.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.ChosenCardList[i].damage.ToString();
            TemporaryCardObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.ChosenCardList[i].cost.ToString();
            TemporaryCardObject.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = GameManager.gameManager.ChosenCardList[i].heal.ToString();
            //Debug.Log(TemporaryCardObject.GetComponent<CardScript>().ClassofthisCard.name+ "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
            //TemporaryCardObject.GetComponent<CardScript>().ClassofthisCard=GameManager.gameManager.ChosenCardList[i];

            DeckCardList.Add(TemporaryCardObject);

        }

        //CardReadyViewRect.localScale = new Vector2(0, 0);

        StopCardDragBoard.transform.localScale = new Vector2(0, 0);
        NextButton.onClick.AddListener(NextButtonFunction);
        UpdateHealthMana();

        OpenSkillListButton.onClick.AddListener(OpenSkillListButtonFunction);

        ManaButton.transform.localScale = new Vector2(1, 1);
        MyHealthButton.transform.localScale = new Vector2(1, 1);
        EnemyHealthButton.transform.localScale = new Vector2(1, 1);

        Debug.Log(PlayerUIClass.health[1]);
        CharacterUIUpdate();
    }
    
    // Update is called once per frame
    void Update()
    {
        
            

        if (GameManager.gameManager.CardUsed)
        {
            StopCardDragBoard.transform.localScale = new Vector2(1, 1);
            for (var i = 0; i < GameManager.gameManager.CardListNum; i++)
            {
                Board.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
            }
            timer += Time.deltaTime;
        }       

        if (timer > waitingTime)
        {

            StopCardDragBoard.transform.localScale = new Vector2(0, 0);
            for (var i = 0; i < GameManager.gameManager.CardListNum; i++)
            {
                Board.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
            }
            timer = 0;
            GameManager.gameManager.CardUsed = false;
        }

        HPCheckRoundWon();

    }
    void NextButtonFunction()
    {
        Debug.Log("action to ready");

        //UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("ActionReady"));

        if (WonThisRound) GameManager.gameManager.TextArrayList.ToNext();
        else GameManager.gameManager.TextArrayList.ToNext(1);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    public void UpdateHealthMana()
    {
        ManaButton.GetComponentInChildren<Text>().text = ManaInt.ToString();
        MyHealthButton.GetComponentInChildren<Text>().text = MyHealthInt.ToString();
        EnemyHealthButton.GetComponentInChildren<Text>().text = EnemyHealthInt.ToString();
    }
    void OpenSkillListButtonFunction()
    {
        SkillListObject.transform.localScale = new Vector2(1 - SkillListObject.transform.localScale.x, 1 - SkillListObject.transform.localScale.y);
        EnemySkillList[0].transform.localPosition = new Vector2(-2, 6);
    }
    public void CharacterUIUpdate()
    {
        PlayerUIObject.transform.GetChild(0).GetComponent<Slider>().maxValue = PlayerUIClass.health[1];
        PlayerUIObject.transform.GetChild(0).GetComponent<Slider>().value = PlayerUIClass.health[0];
        PlayerUIObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = PlayerUIClass.health[1].ToString();
        PlayerUIObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = PlayerUIClass.health[0].ToString();
        PlayerUIObject.transform.GetChild(1).GetComponent<Slider>().maxValue = PlayerUIClass.mana[1];
        PlayerUIObject.transform.GetChild(1).GetComponent<Slider>().value = PlayerUIClass.mana[0];
        PlayerUIObject.transform.GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = PlayerUIClass.mana[1].ToString();
        PlayerUIObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = PlayerUIClass.mana[0].ToString();
        PlayerUIObject.transform.GetChild(2).GetComponentInChildren<Text>().text = PlayerUIClass.lv.ToString();
        PlayerUIObject.transform.GetChild(3).GetComponent<Slider>().maxValue = PlayerUIClass.exp[1];
        PlayerUIObject.transform.GetChild(3).GetComponent<Slider>().value = PlayerUIClass.exp[0];
        PlayerUIObject.transform.GetChild(3).GetChild(0).GetComponentInChildren<Text>().text = PlayerUIClass.health[1].ToString();
        PlayerUIObject.transform.GetChild(3).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = PlayerUIClass.health[0].ToString();
        PlayerUIObject.transform.GetChild(4).GetComponentInChildren<Text>().text = "";


        EnemyUIObject.transform.GetChild(0).GetComponent<Slider>().maxValue = EnemyUIClass.health[1];
        EnemyUIObject.transform.GetChild(0).GetComponent<Slider>().value = EnemyUIClass.health[0];
        EnemyUIObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = EnemyUIClass.health[1].ToString();
        EnemyUIObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = EnemyUIClass.health[0].ToString();
        EnemyUIObject.transform.GetChild(1).GetComponent<Slider>().maxValue = EnemyUIClass.mana[1];
        EnemyUIObject.transform.GetChild(1).GetComponent<Slider>().value = EnemyUIClass.mana[0];
        EnemyUIObject.transform.GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = EnemyUIClass.mana[1].ToString();
        EnemyUIObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = EnemyUIClass.mana[0].ToString();
        EnemyUIObject.transform.GetChild(2).GetComponentInChildren<Text>().text = EnemyUIClass.lv.ToString();
        EnemyUIObject.transform.GetChild(3).GetComponent<Slider>().maxValue = EnemyUIClass.exp[1];
        EnemyUIObject.transform.GetChild(3).GetComponent<Slider>().value = EnemyUIClass.exp[0];
        EnemyUIObject.transform.GetChild(3).GetChild(0).GetComponentInChildren<Text>().text = EnemyUIClass.health[1].ToString();
        EnemyUIObject.transform.GetChild(3).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = EnemyUIClass.health[0].ToString();
        EnemyUIObject.transform.GetChild(4).GetComponentInChildren<Text>().text = "";
    }
    public void HPCheckRoundWon()
    {
        if (EnemyHealthInt < 18) WonThisRound = true;
        if(WonThisRound) NextButtonFunction();
    }
}