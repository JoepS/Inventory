using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class BaseScreen : MonoBehaviour
{
    private Canvas canvas;

    public Event Opened;
    public Event Closed;

    void Awake()
    {
        canvas = this.GetComponent<Canvas>();
    }

    void Open()
    {
        canvas.enabled = true;
        Opened.Use();
    }

    void Close()
    {
        canvas.enabled = false;
        Closed.Use();
    }

}
