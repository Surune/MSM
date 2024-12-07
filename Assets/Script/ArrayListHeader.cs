using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int numbering = -1;
    public bool BeforeNextButtonAble=true;
    public int weight = -1;
    public int character = 0;
    public int scenenum = -1; //상황 가짓수 때문에 일단 만들기는 함 
    public string text;
    public string requirement;
    public bool toactionscene=false;
    public int possibility;
    public int[] effects;
    public string ability;
    public int exp=0;
    public string newskillname = "";
    public int specialresult=0;
    public Node before;
    public List<Node> next = new List<Node>();

    public Node()
    {
        text = null;
        requirement = null;
        possibility = 0;
        effects = new int[3] { 0, 0, 0 };
        before = null;
        ability = null;
        //next = null;
    }
    public Node(string t, int n)
    {
        numbering = n;
        character = 1;
        text = t;
        requirement = null;
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = null;
        ability = null;
    }
    public Node(int n, string t, Node b)
    {
        numbering = n;
        character = 1;
        text = t;
        requirement = null;
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = b;
        ability = null;
        b.next.Add(this);
    }
    public Node(string t, int n, int w)
    {
        weight = w;
        numbering = n;
        character = 2;
        text = t;
        requirement = null;
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = null;
        ability = null;
    }
    public Node(int c, int n, Node h) //탑헤드, 1이 줄글, 2가 이벤트
    {
        numbering = n;
        character = c;
        text = null;
        requirement = null;
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = null;
        ability = null;
        next.Add(h);
    }
    /*
    public Node(string t)
    {
        character = 1;
        text = t;
        requirement = null;
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = null;
        ability = null;
        //next = null;
    }
    public Node(string t, Node b)
    {
        character = 2;
        text = t;
        requirement = null;
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = b;
        ability = null;
        b.next.Add(this);
        //next = null;
    }*/
    public Node(string r, string t,bool a ,Node b)
    {
        character = 3;
        text = t;
        if (r != "") requirement = "(" + r + ")";
        else requirement = "";
        toactionscene = a;
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = b;
        ability = "";
        //next = null;
        b.next.Add(this);
    }
    public Node(string r, string t, Node b)
    {
        character = 3;
        text = t;
        if (r != "") requirement = "(" + r + ")";
        else requirement = "";
        possibility = -1;
        effects = new int[3] { 0, 0, 0 };
        before = b;
        ability = "";
        //next = null;
        b.next.Add(this);
    }
    public Node(int p, string t, Node b, int h, int m, int g, string a, int e, string n, int s)
    {
        character = 4;
        text = t;
        requirement = null;
        if (b.next.Count == 0) possibility = p;
        else possibility = p + b.next[b.next.Count - 1].possibility;
        effects = new int[3] { h, m, g };
        before = b;
        ability = a;
        //next = null;
        b.next.Add(this);
        exp=e;
        newskillname = n;
        specialresult = s;
    }
}

public class ArrayListHeader
{
    public List<Node[]> tophead= new List<Node[]>();
    public List<Node> head = new List<Node>();
    public Node current = new Node();
    public Node death = new Node();
    public ArrayListHeader()
    {
        //head = null;
        current = null;
        death = null;
    }
    public void ToNext()
    {

        /*if(current.next.Count<=1) current = current.next[0];
        else if (current.next.Count <= 2) current = current.next[1];
        else current = current.next[2];
        */

        //if (current.next[0].possibility == -1 && current.next.Count==1) current = current.next[0];
        //else if(current.next[0].possibility == -1 && current.next.Count>1) current = current.next[2];

        current = current.next[0];

        /*if (current.next[0].possibility == -1) current = current.next[0];
        else
        {
            int j = random.Next(0, 100);
            if (0 <= j && j < current.next[0].possibility) current = current.next[0];
            for (var i = 0; i < current.next.Count - 1; i++)
            {
                if (current.next[i].possibility <= j && j < current.next[i + 1].possibility) current = current.next[i + 1];

            }
        }*/
    }
    public void ToNext(int n)
    {
        current = current.next[n];
    }
    public void ToBefore()
    {
        if (current.before != null) current = current.before;
    }
    public void ToHead()
    {
        current = current.before.before.before;
    }
    public void ToHead(int n)
    {
        current = head[n];
    }
    public void ToHead(int t,int n)
    {
        current = tophead[1][t].next[n];
    }
    public void ToTopHead(int c, int n)
    {
        current = tophead[c-1][n-1];
    }
    public void ToDeath()
    {
        current = death;
    }
    public void Add(string sentence, int numbered)
    {
        head.Add(new Node(sentence, numbered));
        current = head[head.Count - 1];
       // Debug.Log("head count" + head.Count);
    }
    public void Add(string sentence, int numbered, int weigh)
    {
        head.Add(new Node(sentence, numbered, weigh));
        current = head[head.Count - 1];
//        Debug.Log("head count" + head.Count);
    }
    /*
    public void Add(string sentence)
    {
        head.Add(new Node(sentence));
        current = head[head.Count - 1];
        Debug.Log("head count" + head.Count);
    }*/
    public void Add(string require, string sentence, string toact)
    {
        //  Debug.Log("current text" + current.text);
        if (toact.Equals("N")) current = new Node(require, sentence, false, current);
        else if (toact.Equals("Y")) current = new Node(require, sentence, true, current);
    }
    public void Add(string require, string sentence)
    {
        //  Debug.Log("current text" + current.text);
        current = new Node(require, sentence, current);
    }
    public void Add(int possibility, string sentence, int health, int mental, int money, string abili, int experi,string newski ,int specialres)
    {
        current = new Node(possibility, sentence, current, health, mental, money, abili, experi, newski, specialres);
        current = head[head.Count - 1];
    }
    public void Add(int numbered, string sentence)
    {
        current = new Node(numbered, sentence, current);
    }
    public void Add(int cha, int numbered, Node he) //tophead?????
    {
        if(current.character == cha && current.numbering == numbered)
        {
            current.next.Add(he);

        }
        else
        {
            current = new Node(cha, numbered, he);            
        }
        tophead[cha - 1][numbered - 1] = current;
    }    
}
