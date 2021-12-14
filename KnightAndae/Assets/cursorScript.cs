using UnityEngine;
using System.Collections;

public class cursorScript : MonoBehaviour
{
    public Texture2D up;
    public Texture2D down;
    public Texture2D left;
    public Texture2D right;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    void Start()
    {
        //Debug.Log("ASDASD");
        Cursor.SetCursor(right, hotSpot, cursorMode);
    }
    
    void OnMouseEnter()
    {
        Cursor.SetCursor(right, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void Update()
    {
        if(Input.mousePosition.y > ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y > ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
        {
            //UP
            Cursor.SetCursor(up, hotSpot, cursorMode);
        }
        else if(Input.mousePosition.y < ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y > ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
        {
            //RIGHT
            Cursor.SetCursor(right, hotSpot, cursorMode);
        }
        else if(Input.mousePosition.y > ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y < ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
        {
            //LEFT
            Cursor.SetCursor(left, hotSpot, cursorMode);
        }
        else if(Input.mousePosition.y < ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y < ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
        {
            //DOWN
            Cursor.SetCursor(down, hotSpot, cursorMode);
        }
    }
}