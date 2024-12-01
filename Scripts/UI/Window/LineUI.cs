using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//曲线界面
public class LineUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    //设置开始位置
    public void SetStartPos(Vector2 pos)
    {
        transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = pos;
    }


    //设置终点位置
    public void SetEndPos(Vector2 pos)
    {
        transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition = pos;

        //开始点
        Vector3 startPos = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        //终点
        Vector3 endPos = pos;
        //中点
        Vector3 midPos = Vector3.zero;
        midPos.y = (startPos.y + endPos.y) * 0.5f;
        midPos.x = startPos.x;
        //计算开始点跟终点的方向
        Vector3 dir = (endPos - startPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //弧度转角度

        //设置终点角度
        transform.GetChild(transform.childCount - 1).eulerAngles = new Vector3(0, 0, angle - 90);

        for(int i = transform.childCount - 1; i >= 0; i--) 
        {
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = GetBezier(startPos, midPos, endPos, i / (float)transform.childCount);

            if (i != transform.childCount - 1)
            {
                dir = (transform.GetChild(i + 1).GetComponent<RectTransform>().anchoredPosition - transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition).normalized;
                angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                transform.GetChild(i).eulerAngles = new Vector3(0, 0, angle - 90);
            }
        }

    }

    //贝塞尔曲线
    public Vector3 GetBezier(Vector3 start, Vector3 mid, Vector3 end, float t)
    {
        return (1.0f - t) * (1.0f - t) * start + 2.0f * t * (1.0f - t) * mid + t * t * end;
    }
}
