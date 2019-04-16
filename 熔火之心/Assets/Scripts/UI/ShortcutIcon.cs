using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShortcutIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerDownHandler, IPointerUpHandler
{
    private Image img;
    private Shortcut originGrid;//初始快捷栏
    private Transform canvas;

    private void Awake()
    {
        img = gameObject.GetComponent<Image>();
        originGrid = transform.parent.GetComponent<Shortcut>();
        canvas = GameObject.Find("Canvas").GetComponent<Transform>();
    }

    /// <summary>
    /// 开始拖拽时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        img.raycastTarget = false;
        MagnifyItem();
        //放至画布最表层
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// 正在拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        DragItem(eventData);
    }

    /// <summary>
    /// 拖拽结束时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        img.raycastTarget = true;
        ShortcutCast(eventData);
        RestoreItemSize();
    }

    /// <summary>
    /// 鼠标点下时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        ShrinkItem();
    }

    /// <summary>
    /// 鼠标抬起时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        RestoreItemSize();
    }

    /// <summary>
    /// 缩小物品
    /// </summary>
    public void ShrinkItem()
    {
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    /// <summary>
    /// 放大物品
    /// </summary>
    private void MagnifyItem()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    /// <summary>
    /// 还原物品原本大小
    /// </summary>
    public void RestoreItemSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// 拖拽物品
    /// </summary>
    private void DragItem(PointerEventData eventData)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector3 mouseGlobalPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out mouseGlobalPos))
        {
            transform.position = mouseGlobalPos;
        }
    }

    /// <summary>
    /// 复位
    /// </summary>
    private void RestorePosition()
    {
        transform.SetParent(originGrid.transform);
        transform.SetAsFirstSibling();
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 检测快捷栏
    /// </summary>
    private void ShortcutCast(PointerEventData eventData)
    {
        //是快捷栏，使得该快捷栏的信息变更
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //为空的快捷栏
            if (eventData.pointerCurrentRaycast.gameObject.tag == Tags.shortcut)
            {
                Shortcut currentGrid = eventData.pointerCurrentRaycast.gameObject.GetComponent<Shortcut>();
                if (currentGrid != originGrid)
                {
                    //新格子复制信息
                    currentGrid.SetShortcutInfo(originGrid.Info);
                    //清空原格子
                    originGrid.Clear();
                }
            }
            //快捷栏里有东西
            else if (eventData.pointerCurrentRaycast.gameObject.tag == Tags.shortcutIcon)
            {
                Shortcut currentGrid = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Shortcut>();
                //交换信息
                TowerInfo currentInfo = currentGrid.Info;
                currentGrid.SetShortcutInfo(originGrid.Info);
                originGrid.SetShortcutInfo(currentInfo);
                print("快捷栏图标交换成功");
            }
        }
        //复位
        RestorePosition();
    }
}
