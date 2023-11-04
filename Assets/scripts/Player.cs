using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 100f;

    private CharacterController contr;
    private float xrot = 0f, sens = 100f;

    void Start(){
        contr = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){

        //Movement
        float hor = Input.GetAxis("Horizontal"), ver = Input.GetAxis("Vertical"); 
        Vector3 move = transform.right * hor + transform.forward * ver;
        contr.Move(move * speed * Time.deltaTime);


        //Mouse
        float X = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float Y = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xrot -= Y;
        xrot = Mathf.Clamp(xrot, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xrot, 0f, 0f);
        transform.Rotate(Vector3.up * X);
        

        //SPEEDUP
        if(Input.GetKeyDown(KeyCode.Space)){
            speed += 50f;
        }

        
    }
}