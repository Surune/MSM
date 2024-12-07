using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExpansionScript : MonoBehaviour
{
    public SpriteRenderer Board;
    public Transform CardReadyViewRect;

    public SkillCardClass ClassofthisCard;

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
            Card.transform.localScale = new Vector2(originScale.x*10, originScale.y*10);
        }
    }

    private void OnMouseUp()
    {
        Card.transform.localScale = originScale;
        Card.transform.position = originPos;
         
    }

}
