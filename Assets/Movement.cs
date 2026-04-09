using UnityEngine;

public class Movement : MonoBehaviour
{
    public float mouseSensitivity = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity / 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity / 100;

        transform.position += new Vector3(mouseX, mouseY);
    }
}
