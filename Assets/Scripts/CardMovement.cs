using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform cardParent;

    public void OnBeginDrag(PointerEventData eventData) // ドラッグ開始時
    {
        // カードオブジェクトの親要素を変更
        cardParent = transform.parent;
        transform.SetParent(cardParent.parent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)  // ドラッグした時
    {
        // カードの場所をマウスポインタ―の場所にする
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)   // カードを離したとき
    {
        // カードオブジェクトの親要素を変更
        transform.SetParent(cardParent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
