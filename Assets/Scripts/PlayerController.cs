using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;
    [SerializeField]
    private float crouchSpeed;

    [SerializeField]
    private float jumpForce;


    private bool isGround = true;
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isWalk = false;


    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    [SerializeField]
    private float lookSensibility;
    [SerializeField]
    private float cameraRotationLimit;
    
    private float currentCameraRotationX;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    private GunController theGunController;
    private CapsuleCollider capsuleCollider;
    private CrossHair theCrossHair;
    private StatusController theStatusController;

    private Vector3 lastPos;
    private void Start()
    {
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
        theCrossHair = FindObjectOfType<CrossHair>(); 

        applySpeed = walkSpeed;
        myRigid = GetComponent<Rigidbody>();
        theGunController = FindObjectOfType<GunController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        theStatusController = FindObjectOfType<StatusController>();
    }
    private void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        MoveCheck();
        Move();
        CameraRotation();
        CharacterRotation();
        
    }

    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        theCrossHair.CrouchingAnimation(isCrouch);
        //theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
        StartCoroutine("CrouchCoroutine");
    }

    IEnumerator CrouchCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;
        while (_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
            {
                break;
            }

            yield return null; // 한프레임 쉬기
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        theCrossHair.JumppingAnimation(!isGround);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && theStatusController.GetCurrentSP() > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isCrouch)
        {
            Crouch();
        }
        theStatusController.DecreaseStamina(100);
        myRigid.velocity = transform.up * jumpForce;
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * applySpeed;

        transform.position += velocity * Time.deltaTime;
        //myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
 
    }

    private void MoveCheck()
    {
        if (!isRun && !isCrouch && isGround)
        {
            //Debug.Log(Vector3.Distance(lastPos, transform.position));
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
            {
                isWalk = true;
            }
            else
            {
                isWalk = false;

            }
            theCrossHair.WalkingAnimation(isWalk);

            lastPos = transform.position;
        }
        

    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSP() <= 0)
        {
            RunningCancle();
        }
    }

    private void Running()
    {
        if (isCrouch)
          Crouch();
        theStatusController.DecreaseStamina(10);
        theGunController.CancleFineSight();
        theCrossHair.RunningAnimation(isRun);
        isRun = true;
        applySpeed = runSpeed;

    }

    private void RunningCancle()
    {
        isRun = false;
        theCrossHair.RunningAnimation(isRun);
        applySpeed = walkSpeed;

    }

    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensibility;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Mouse X")) >= 0.1f)
        {
            float yRotation = Input.GetAxisRaw("Mouse X");
            Vector3 characterRotation = new Vector3(0f, yRotation, 0f) * lookSensibility;
            myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotation));
        }
        
    }






}
