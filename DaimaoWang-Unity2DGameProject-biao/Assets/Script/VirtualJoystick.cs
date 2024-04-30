using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 虚拟摇杆
/// </summary>
public class VirtualJoystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("摇杆类型")]
    public JoystickType type;
    [Header("摇杆背景")]
    public RectTransform background;
    [Header("摇杆控制柄")]
    public RectTransform thumb;

    bool inDrag;//是否在拖拽中
    float maxDragDist;//最大拖拽距离

    public static Vector2 v;//方向+位移

    #region external interfaces

    /// <summary>
    /// 得到方向+位移
    /// </summary>
    public Vector2 GetDirAndDist()
    {
        return v;
    }

    /// <summary>
    /// 得到方向
    /// </summary>
    public Vector2 GetDir()
    {
        return v.normalized;
    }

    #endregion

    private void Start()
    {
        background.GetComponent<Image>().raycastTarget = false;
        thumb.GetComponent<Image>().raycastTarget = false;
        maxDragDist = background.rect.width / 2;
        if (type == JoystickType.PosCanChange
            || type == JoystickType.FollowMove)
        {
            UpdateShow(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inDrag = true;

        if (type == JoystickType.PosCanChange
           || type == JoystickType.FollowMove)
        {
            background.localPosition = Screen2UI(eventData.position, background.parent as RectTransform, eventData.pressEventCamera);
            UpdateShow(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetLocalPos = Screen2UI(eventData.position, thumb.parent as RectTransform, eventData.pressEventCamera);
        Vector2 targetDir = targetLocalPos.normalized;
        float dist = Vector2.Distance(targetLocalPos, Vector2.zero);
        //Debug.Log(targetLocalPos);
        if (dist > maxDragDist)
        {
            if (type == JoystickType.FollowMove)
            {
                float offset = dist - maxDragDist;
                background.transform.localPosition += (Vector3)targetDir * offset;
                //background.localPosition += (Vector3)targetDir * offset;
            }
            thumb.transform.localPosition = targetDir * maxDragDist;
        }
        else
        {
            thumb.localPosition = targetDir * dist;
        }

        v = targetDir * dist;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        v = Vector2.zero;
        inDrag = false;

        if (type == JoystickType.PosCanChange
        || type == JoystickType.FollowMove)
        {
            UpdateShow(false);
        }
    }

    private void Update()
    {
        if (!inDrag)
        {
            thumb.anchoredPosition = Vector2.Lerp(thumb.anchoredPosition, Vector2.zero, 0.1f);
        }
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    void UpdateShow(bool isShow)
    {
        background.gameObject.SetActive(isShow);
        thumb.gameObject.SetActive(isShow);
    }

    /// <summary>
    /// 屏幕坐标转UI坐标
    /// </summary>
    Vector2 Screen2UI(Vector2 v, RectTransform rect, Camera camera = null)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, v, camera, out pos);
        return pos;
    }
}

/// <summary>
/// 摇杆类型
/// </summary>
public enum JoystickType
{
    Normal,//固定位置
    PosCanChange,//可变位置
    FollowMove,//跟随移动
}
