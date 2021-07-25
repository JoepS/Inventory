using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class OpenMobileKeyboardScroll : MonoBehaviour
{
    private ScrollRect scrollRect;

    private LayoutElement bufferElement;

    private void Awake()
    {
        scrollRect = this.gameObject.GetComponent<ScrollRect>();

    GameObject layoutElement = new GameObject("Buffer");
        layoutElement.transform.SetParent(scrollRect.content);
        bufferElement = layoutElement.AddComponent<LayoutElement>();
        bufferElement.minHeight = 0;
        StartCoroutine(UpdateScrollView());
    }

    private IEnumerator UpdateScrollView()
    {
        while (true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        bufferElement.minHeight = GetKeyboardSize();
        if (bufferElement.minHeight > 0)
        {
            yield return null;
            ScrollToInput();
        }
#elif UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Q))
            {
                bufferElement.minHeight = 1071;
                yield return null;
                ScrollToInput();
         
            }

#endif
            yield return null;
        }
    }

    private void ScrollToInput()
    {
        //scrollRect.verticalScrollbar.value = 0;
        SnapTo();
    }

    public void SnapTo()
    {
        Canvas.ForceUpdateCanvases();

        Vector2 position = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position)
            - (Vector2)scrollRect.transform.InverseTransformPoint(EventSystem.current.currentSelectedGameObject.transform.position);
        position.x = 0;
        position.y -= EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().rect.size.y;
        scrollRect.content.anchoredPosition = position;
    }

    private int GetKeyboardSize()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        //https://forum.unity.com/threads/keyboard-height.291038/?_ga=2.182811878.1594911924.1605602569-296759937.1605086476
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                return Screen.height - Rct.Call<int>("height");
            }
        }
#else
        return 0;
#endif
    }

}
