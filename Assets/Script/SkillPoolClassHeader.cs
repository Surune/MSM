using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoolClass
{
    public string jobname = "";
    public bool jobunlocked = false;
    public int jobnum = -1;
    public List<SkillCardClass> EachChildSkillList = new List<SkillCardClass>(); 

    public SkillPoolClass()
    {
        jobname = "무명";
        jobunlocked = false;
        EachChildSkillList = new List<SkillCardClass>();
    }
    public SkillPoolClass(string n)
    {
        jobname = n;
        jobunlocked = false;
        EachChildSkillList = new List<SkillCardClass>();
    }
    public void CreateSkillFunction(string n, int d, int c, int h)
    {
        EachChildSkillList.Add(new SkillCardClass(n, d, c, h, false));
    }
    public void CreateSkillFunction(string n,int d, int c, int h, bool l)
    {
        EachChildSkillList.Add(new SkillCardClass(n, d, c, h, l));
    }

    
}
