using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public CharacterController _controller;
    public DynamicJoystick joystick;
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f; 
    [Range(0.0f, 1f)]
    public float smoothAnim = 0.12f;
    [HideInInspector] public Animator anim;
    //movement
    float _verticalVelocity;
    float moveHorizontal;
    float moveVertical;
    Vector3 velocityTemp;

    //  speed
   private float targetSpeed = 10f;
    [HideInInspector] public float speed;
    //rotation
    private Vector3 _direction;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
  
    private bool _hasAnimator;
   // [HideInInspector] public Vector3 currentRotation;
    //dash
    bool canDash;
    float dashingTime = 1;
    float dashingPower = 100;
    float dashingCooldown = 3;

    public Transform upperBody;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
/*    private void LateUpdate()
    {
        UpdateBodyTop();
    }

    private void UpdateBodyTop()
    {
        upperBody.transform.forward = transform.forward;
    }*/

    public void Movement()
    {
        if (Time.timeScale == 0) return;
        targetSpeed = PlayerController.Instance.heroStats.CurrentSpeed;
        _verticalVelocity = -15f;
#if UNITY_EDITOR
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(moveHorizontal, _verticalVelocity, moveVertical);

#else
        moveHorizontal = joystick.Horizontal;
        moveVertical = joystick.Vertical;
        _direction = new Vector3(moveHorizontal, _verticalVelocity, moveVertical);
#endif

        if (_direction != Vector3.zero + (Vector3.up * _verticalVelocity))
        {
            _targetRotation = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
           // currentRotation = new Vector3(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;


        if (_direction == Vector3.zero + (Vector3.up * _verticalVelocity))
        {
            // if (smoothAnim > 0) smoothAnim -= 0.5f;
            if (speed > 0f) speed -= smoothAnim;
           else
            PlayerController.Instance.stateManager.TransitionToState(PlayerState.Idle);
        }
        else
        {
            speed += smoothAnim;
         
        }
        speed = Mathf.Clamp(speed, 0f, targetSpeed);


        velocityTemp = targetDirection.normalized * (speed * Time.deltaTime ) +
                                new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
     
        _controller.Move(velocityTemp);
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash(targetDirection.normalized));
        }


        anim.SetFloat("Speed", speed);
        PlayerController.Instance.animator.SetTrigger("BackToMove");

    }
    public void Idle()
    {
        if (!_controller.enabled) return;
        _verticalVelocity = -15f;
#if UNITY_EDITOR
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(moveHorizontal, _verticalVelocity, moveVertical);

#else
        moveHorizontal = joystick.Horizontal;
        moveVertical = joystick.Vertical;
        _direction = new Vector3(moveHorizontal, _verticalVelocity, moveVertical);
#endif

        if (_direction != Vector3.zero + (Vector3.up * _verticalVelocity))
        {
          
                PlayerController.Instance.stateManager.TransitionToState(PlayerState.Move);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;


        if (_direction == Vector3.zero + (Vector3.up * _verticalVelocity))
        {

            if (speed > 0f) speed = 0;
        }
        else
        {
            speed += 0.5f;

        }
        speed = Mathf.Clamp(speed, 0f, targetSpeed);
       
        velocityTemp = targetDirection.normalized * (speed * Time.deltaTime) +
                                new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;

        _controller.Move(velocityTemp);
        
        
    }
    private IEnumerator Dash(Vector3 dir)
    {
        canDash = false;
        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + dashingTime)
        {
            _controller.Move(dir * dashingPower * Time.deltaTime * transform.localScale.x);
            yield return null; // this will make Unity stop here and continue next frame
        }
        yield return new WaitForSeconds(dashingCooldown);
        Physics.IgnoreLayerCollision(7, 9, false);
        canDash = true;
    }
}
