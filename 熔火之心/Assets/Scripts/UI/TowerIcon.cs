using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 塔图标类，实现拖拽放置与快捷栏中
/// </summary>
public class TowerIcon : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Transform canvas;
    private Image img;
    private TowerInfo info = null;
    private bool isDrag = false;//是否正在拖拽

    public TowerInfo Info { get => info; set => info = value; }

    private void Awake()
    {
        img = gameObject.GetComponent<Image>();
        canvas = GameObject.Find("Canvas").GetComponent<Transform>();
    }

    /// <summary>
    /// 复制一个
    /// </summary>
    public void CopyItem()
    {
        Instantiate(gameObject, transform.parent);
    }

    /// <summary>
    /// 缩小物品
    /// </summary>
    public void ShrinkItem()
    {
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);       
    }

    /// <summary>
    /// 开始拖拽时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        img.raycastTarget = false;
        //设为最表层
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// 拖动时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        DragItem(eventData);
    }

    /// <summary>
    /// 结束拖拽时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        //检测是否为快捷栏
        ShortcutCast(eventData);
    }

    /// <summary>
    /// 若没有拖拽，松开鼠标后销毁
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDrag)
            Destroy(gameObject);
    }

    /// <summary>
    /// 点击图标后 复制一个图标
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        CopyItem();
        ShrinkItem();
    }

    /// <summary>
    /// 检测快捷栏
    /// </summary>
    private void ShortcutCast(PointerEventData eventData)
    {
        //是快捷栏，使得该快捷栏的信息变更
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.tag == Tags.shortcut)
            {
                Shortcut shortCut = eventData.pointerCurrentRaycast.gameObject.GetComponent<Shortcut>();
                shortCut.SetShortcutInfo(info);
            }
            else if (eventData.pointerCurrentRaycast.gameObject.tag == Tags.shortcutIcon)
            {
                Shortcut shortCut = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Shortcut>();
                shortCut.SetShortcutInfo(info);
            }
        }
        //之后销毁物体
        Destroy(gameObject);
    }

    /// <summary>
    /// 拖拽物品
    /// </summary>
    public void DragItem(PointerEventData eventData)
    {
        //让物品位置等于鼠标位置
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector3 mouseGlobPos;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle : 将屏幕坐标转化为rectTransform平面下的坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out mouseGlobPos))
        {
            rectTransform.position = mouseGlobPos;
        }
    }
}
