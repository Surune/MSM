using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class CreateSkillPoolScript : MonoBehaviour
{
    public static CreateSkillPoolScript createskillpoolscript = null;

    
    // Start is called before the first frame update
    private void Awake()
    {
        
        if (createskillpoolscript == null)
        {
            createskillpoolscript = this;
        }
        else if (createskillpoolscript != this)
        {
            Destroy(gameObject);
        }


        /*
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
        */
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateSkillPoolFunctionOrigin()
    {
        
        
        
        /*
        EachJobCardList.Add(new SkillCardClass("FireBall", 0, 1, 1, 0, true));
        EachJobCardList.Add(new SkillCardClass("Lightning", 0, 2, 2, 0, true));
        EachJobCardList.Add(new SkillCardClass("Blizzard", 0, 3, 3, 0, true));
        EachJobCardList.Add(new SkillCardClass("MindControl", 0, 4, 4, 0, true));
        EachJobCardList.Add(new SkillCardClass("SandBurst", 0, 5, 5, 0, true));
        EachJobCardList.Add(new SkillCardClass("SunShine", 0, 0, 5, 10, true));

        SkillCardList.Add(EachJobCardList);
        EachJobCardList = new List<SkillCardClass>();

        EachJobCardList.Add(new SkillCardClass("Strike", 1, 1, 1, 0, true));
        EachJobCardList.Add(new SkillCardClass("Double Strike", 1, 2, 2, 0, true));
        EachJobCardList.Add(new SkillCardClass("Swing", 1, 3, 3, 0, true));
        EachJobCardList.Add(new SkillCardClass("Kill", 1, 4, 4, 0, true));
        EachJobCardList.Add(new SkillCardClass("Taunt", 1, 5, 5, 0, false));
        EachJobCardList.Add(new SkillCardClass("Self Heal", 1, 0, 5, 10, false));

        SkillCardList.Add(EachJobCardList);
        EachJobCardList = new List<SkillCardClass>();


        EachJobCardList.Add(new SkillCardClass("Heal", 2, 1, 1, 0, false));
        EachJobCardList.Add(new SkillCardClass("Holy Magic", 2, 2, 2, 0, true));
        EachJobCardList.Add(new SkillCardClass("Summon Angel", 2, 3, 3, 0, false));
        EachJobCardList.Add(new SkillCardClass("Reseraction", 2, 4, 4, 0, true));
        EachJobCardList.Add(new SkillCardClass("Dark Magic", 2, 5, 5, 0, true));
        EachJobCardList.Add(new SkillCardClass("Silence", 2, 0, 5, 10, false));

        SkillCardList.Add(EachJobCardList);
        EachJobCardList = new List<SkillCardClass>();
        */
    }
}
