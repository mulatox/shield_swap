using UnityEngine;

public class SimpleAim : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
         mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

         mousePosition.z = transform.position.z;

         direction = mousePosition - transform.position;

         transform.up = direction;
        
         
         
    }
}
