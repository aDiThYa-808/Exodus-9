using UnityEngine;

public class MouseMove : MonoBehaviour
{
public Transform head;
    private void Update()
    {
       transform.position = head.position;
    }
}
    
