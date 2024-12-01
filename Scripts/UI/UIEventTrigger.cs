using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class UIEventTrigger : MonoBehaviour,IPointerClickHandler
{
    public Action<GameObject, PointerEventData> onClick;

    public static UIEventTrigger Get(GameObject obj)
    {
        UIEventTrigger trigger = obj.GetComponent<UIEventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<UIEventTrigger>();
        }
        return trigger;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick(gameObject, eventData);
        }
    }
}

