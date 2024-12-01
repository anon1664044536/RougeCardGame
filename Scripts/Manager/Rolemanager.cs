using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolemanager : MonoBehaviour
{
    public static Rolemanager Instanse = new Rolemanager();
    public List<string> cardList;
    public void Init()
    {
        cardList = new List<string>();

        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");

        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");
        cardList.Add("1002");

        cardList.Add("1003");

        cardList.Add("1004");

        cardList.Add("1005");

        cardList.Add("1006");
    }
}
