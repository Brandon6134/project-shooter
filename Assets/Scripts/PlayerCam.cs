using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] InputActionReference camInput;
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //cursor stays at center of screen
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //read input action controlling camera
        Vector2 input = camInput.action.ReadValue<Vector2>();

        //how unity handles rotations and inputs
        yRotation += input.x;

        xRotation -= input.y;
        xRotation = Mathf.Clamp(xRotation,-90f,90f); //dont allow user to look more than 90 degrees up or down

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0); //rotate cam along both axis 
        orientation.rotation = Quaternion.Euler(0,yRotation,0); //rotate player along y axis
    }
}
