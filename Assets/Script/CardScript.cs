using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public SpriteRenderer Board;
    public Transform CardReadyViewRect;

    public SkillCardClass ClassofthisCard= new SkillCardClass();

    private GameObject Card;

    private Vector2 originPos = new Vector2(0, 0);

    private Vector2 originScale = new Vector2(0, 0);

    private Vector2 VariableFindingCardButtonFunction;

    private float time = 0f;
    private int CardLongClickTimeLimit = 3;

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
        originPos = Card.transform.position;
        VariableFindingCardButtonFunction =(new Vector2(originPos.x,originPos.y));
        originScale = Card.transform.localScale;
    }

    private void OnMouseDown()
    {
        originScale = Card.transform.localScale;
        originPos = Card.transform.position;

        time = 0;
        Card.transform.SetSiblingIndex(5);
        /*time += Time.deltaTime;
        

        if (time > CardLongClickTimeLimit)
        {
            Card.transform.localScale = new Vector2(originScale.x*2, originScale.y*2);
        }
               */


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

        for (var i = 0; i < GameManager.gameManager.CardListNum; i++)
        {
            /*
            if (!ActionReadyGameManager.actionReadyGameManager.CardChosenBoolList[i])
            {
                ActionReadyGameManager.actionReadyGameManager.CardReadyList[i].transform.SetSiblingIndex(i);
            }*/
            if (!GameManager.gameManager.SkillCardList[ActionReadyGameManager.actionReadyGameManager.CurrentJobNum][i].inDeck)
            {
                ActionReadyGameManager.actionReadyGameManager.CardReadyList[ActionReadyGameManager.actionReadyGameManager.CurrentJobNum][i].transform.SetSiblingIndex(i);
            }
            
        }

        if (originPos.y > -1.5f)
        {
            if (Card.transform.position.y < -1.5f)
            {
                //GameManager.gameManager.CardListNum++;
                Debug.Log("ASSAFSD" + ((int)VariableFindingCardButtonFunction.x - (int)VariableFindingCardButtonFunction.y / 2 * 3 + 4).ToString());
                //Debug.Log((int)originPos.x);
                //Debug.Log((int)originPos.y);


                Debug.Log("ㄲㄲㄲ");

                
                if (ActionReadyGameManager.actionReadyGameManager.ChosenCardNum >= 5) GoToOriginPos();
                else ActionReadyGameManager.actionReadyGameManager.CardButtonFunction(ActionReadyGameManager.actionReadyGameManager.CurrentJobNum, (int)VariableFindingCardButtonFunction.x - (int)VariableFindingCardButtonFunction.y / 2 * 3 + 4);
                Debug.Log("eeeeee");

            }
            else
            {
                GoToOriginPos();
            }
        }
        else
        {
            if (Card.transform.position.y > -1.5f)
            {
                //GameManager.gameManager.CardListNum++;
                Debug.Log("ASSAFSD" + ((int)VariableFindingCardButtonFunction.x - (int)VariableFindingCardButtonFunction.y / 2 * 3 + 4).ToString());
                //Debug.Log((int)originPos.x);
                //Debug.Log((int)originPos.y);

                

                ActionReadyGameManager.actionReadyGameManager.CardButtonFunction(ActionReadyGameManager.actionReadyGameManager.CurrentJobNum,(int)VariableFindingCardButtonFunction.x - (int)VariableFindingCardButtonFunction.y / 2 * 3 + 4);
                if (ActionReadyGameManager.actionReadyGameManager.ChosenCardNum >= 5) GoToOriginPos();

            }
            else
            {
                GoToOriginPos();
            }
        }
        



    }
    private void GoToOriginPos()
    {
        Card.transform.position = originPos;
    }
}
