using UnityEngine;

public class SimpleCursor : MonoBehaviour
{
    public Texture2D cursorSprite;
    public Vector2 hotSpot = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cursorSprite,hotSpot,CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
