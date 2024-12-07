using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardDragScript : MonoBehaviour
{
    public SpriteRenderer Board;

    private GameObject Card;

    private Vector2 originPos = new Vector2(0, 0);

    private Vector2 originScale = new Vector2(0, 0);

    private float time = 0f;
    private int CardLongClickTimeLimit = 2;

    /*
     * Illust 0
     * Text 1
     * Damage 2
     * Cost 3
     * Heal 4
     */

    private void Start()
    {
        Card = transform.gameObject;
        originScale = Card.transform.localScale;
    }

    private void Update()
    {
        //Debug.Log(Input.GetMouseButtonDown(0));
    }

    private void OnMouseDown()
    {
        originScale = Card.transform.localScale;
        originPos = Card.transform.position;

        time = 0;




        //Sample_BoardManager.instance.holdingBlock = blocks;
    }

    private void OnMouseDrag()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Card.transform.position = mousePos;

        time += Time.deltaTime;

        if (time > CardLongClickTimeLimit)
        {
            Card.transform.localScale = new Vector2(originScale.x*2, originScale.y*2);

        }
    }

    private void OnMouseUp()
    {
        Card.transform.localScale = originScale;
        if (Card.transform.position.y > -1.5f)
        {
            ActionGameManager.actionGameManager.EnemyHealthInt -= Int32.Parse(Card.transform.GetChild(2).GetComponentInChildren<Text>().text);
            ActionGameManager.actionGameManager.ManaInt -= Int32.Parse(Card.transform.GetChild(3).GetComponentInChildren<Text>().text);
            ActionGameManager.actionGameManager.MyHealthInt += Int32.Parse(Card.transform.GetChild(4).GetComponentInChildren<Text>().text);
            ActionGameManager.actionGameManager.UpdateHealthMana();
            

            ActionGameManager.actionGameManager.EnemyUIClass.health[0]-= Int32.Parse(Card.transform.GetChild(2).GetComponentInChildren<Text>().text);
            ActionGameManager.actionGameManager.PlayerUIClass.mana[0] -= Int32.Parse(Card.transform.GetChild(3).GetComponentInChildren<Text>().text);
            ActionGameManager.actionGameManager.PlayerUIClass.health[0] += Int32.Parse(Card.transform.GetChild(4).GetComponentInChildren<Text>().text);
            GameManager.gameManager.CardListNum--;
            GameManager.gameManager.CardUsed = true;
            ActionGameManager.actionGameManager.CharacterUIUpdate();
            Destroy(Card);
        }
        else
        {
            Card.transform.position = originPos;
        }



        /*
        if (Sample_BoardManager.instance.CheckFit() == false)
        {
            blocks.transform.position = originPos;
            Sample_BoardManager.instance.holdingBlock = null;
        }
        else
        {
            Sample_BoardManager.instance.FitBlocks();
        }*/
    }
}
