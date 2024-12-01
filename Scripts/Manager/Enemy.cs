using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//敌人脚本

//敌人行动枚举
public enum ActionType
{
    None,
    Defend,
    Attack,
}

public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data; //敌人数据表

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //ui相关
    public Transform attackTf;
    public Transform defemdTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //数值相关
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //组件相关
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        ani = transform.GetComponent<Animator>();

        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();

        attackTf = actionObj.transform.Find("attack");
        defemdTf = actionObj.transform.Find("defend");

        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //设置血条，行动力位置
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down*0.2f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);

        SetRandomAction();

        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);

        UpdateHp();
        UpdateDefend();
    }

    public void SetRandomAction()
    {
        int ran = Random.Range(1, 3);
        type = (ActionType)ran;
        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defemdTf.gameObject.SetActive(true);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defemdTf.gameObject.SetActive(false);
                break;
        }
    }

    public void UpdateHp()
    {
        hpTxt.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    public void UpdateDefend()
    {
        defendTxt.text = Defend.ToString();
    }


    //被攻击卡选中
    public void Onselect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.red);
    }

    //未选中
    public void OnUnselect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.black);
    }

    //受伤
    public void Hit(int val)
    {
        if (Defend >= val)
        {
            Defend -= val;

            //播放受伤
            ani.Play("hit", 0, 0);
        }
        else
        {
            val -= Defend;
            Defend = 0;
            CurHp -= val;
            if (CurHp <= 0)
            {
                CurHp = 0;
                //播放死亡
                ani.Play("die");

                //敌人从列表中移除
                EnemyManager.Instance.DeleteEnemy(this);
                
                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //受伤
                ani.Play("hit", 0, 0);
            }
        }

        //刷新血量等ui
        UpdateDefend();
        UpdateHp();
    }

    //隐藏怪物头部的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defemdTf.gameObject.SetActive(false);
    }

    //执行敌人行动
    public IEnumerator DoAction()
    {
        HideAction();

        //播放对应动画
        ani.Play("attack");
        //等待某一时间后执行对应行为
        yield return new WaitForSeconds(0.5f);

        switch (type)
        {
            case ActionType.None: 
                break;
            case ActionType.Defend:
                Defend += 1;
                UpdateDefend();
                break;
            case ActionType.Attack:
                FightManager.Instance.GetPlayerHit(Attack,this);
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;
        }

        //等待动画播放完
        yield return new WaitForSeconds(1);
        //播放待机
        if (this!=null)
        {
            ani.Play("idle");
        }
    }
}
