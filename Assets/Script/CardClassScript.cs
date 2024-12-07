using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardClass
{
    public string name;
    //public int job;
    public int damage;
    public int cost;
    public int heal;
    public bool inDeck;
    public bool unLocked;

    public SkillCardClass()
    {
        name = "";
        //job = -1;
        damage = 0;
        cost = 0;
        heal = 0;
        inDeck = false;
        unLocked = false;
    }
    public SkillCardClass(string s, int d, int c, int h)
    {
        name = s;
        //job = j;
        damage = d;
        cost = c;
        heal = h;
        inDeck = false;
        unLocked = true;
    }
    public SkillCardClass(string s, int d, int c, int h,bool l)
    {
        name = s;
        //job = j;
        damage = d;
        cost = c;
        heal = h;
        inDeck = false;
        unLocked = l;
    }
    public SkillCardClass(string s, int d, int c, int h, bool b,bool l)
    {
        name = s;
        //job = j;
        damage = d;
        cost = c;
        heal = h;
        inDeck = b;
        unLocked = l;
    }
}