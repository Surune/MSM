using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIClass
{
    //[current, maximum]
    public int[] health = new int[2] { 100, 100 };
    public int[] mana = new int[2] { 100, 100 };
    public int lv = 0;
    public int[] exp = new int[2] { 0, 0 };
    public List<string> state=new List<string>();

    public List<SkillUIClass> SkillList = new List<SkillUIClass>();

    public CharacterUIClass()
    {
        health[1] = 100;
        exp[0] = -1;
    }
    public CharacterUIClass(int h, int m, int l, int e)
    {
        health[0] = h;
        health[1] = h;
        mana[0] = m;
        mana[1] = m;
        lv = l;
        exp[0] = 0;
        exp[1] = e;
    }
    public CharacterUIClass(int h, int m, int l)
    {
        health[0] = h;
        health[1] = h;
        mana[0] = m;
        mana[1] = m;
        lv = l;
    }
    public void AddtoSkillList(string n, int d, int c, int h, List<string> s)
    {
        SkillList.Add(new SkillUIClass(n, d, c, h, s));
    }
    //public void skilled
    public void Attacking(int ch, int cm)
    {
        health[0] -= ch;
        mana[0] -= cm;
    }
    public void Damaged(int ch, int cm)
    {
        health[0] -= ch;
        mana[0] -= cm;
    }
    public void Won(int e)
    {
        exp[0] += e;
    }
    public void Lost(int e)
    {
        exp[0] -= e;
    }
    public void LVUp()
    {
        lv += 1;
        exp[0] -= exp[1];
    }
    public void AddState(string s)
    {
        state.Add(s);
    }
    public void DeleteState(string s)
    {
        state.Remove(s);
    }
}
public class SkillUIClass
{
    //[current, maximum]
    public string name="Default Skill Name";
    public int damage=0;
    public int cost =0;
    public int heal = 0;
    public List<string> state = new List<string>();

    public SkillUIClass()
    {
        
    }
    public SkillUIClass(string n, int d, int c, int h)
    {
        name = n;
        damage = d;
        cost = c;
        heal = h;
    }
    public SkillUIClass(string n, int d, int c, int h, string s)
    {
        name = n;
        damage = d;
        cost = c;
        heal = h;
        state.Add(s);
    }
    public SkillUIClass(string n, int d, int c, int h, List<string> s)
    {
        name = n;
        damage = d;
        cost = c;
        heal = h;
        for(int i=0;i<s.Count;i++) state.Add(s[i]);
    }
}
