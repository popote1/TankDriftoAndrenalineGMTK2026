using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private Transform[] _frontWheels;
    [SerializeField] private Transform[] _backWheels;
    [Header("Hover")]
    [SerializeField] private float _hoverHeight = 0.3f;
    [SerializeField] private float _wheelRaycastDistance = 0.4f;
    [SerializeField] private float _springForce =1;
    [SerializeField] private float _springDamper =1;
    [Header("Grip")]
    [SerializeField] private AnimationCurve _frontwheelGripFactor;
    [SerializeField] private AnimationCurve _backwheelGripFactor;
    [SerializeField] private float _wheelGripFactor =1;
    [SerializeField] private float _wheelMass = 1;

    [Header("Acceleration")] 
    [SerializeField] private float _tankTopSpeed = 10;
    [SerializeField] private float TankAccelleration = 1;
    [SerializeField] private AnimationCurve _accelerationCurve;
    [Header("Steering")] [SerializeField] private float _wheelMaxAngle = 45;
    
    [SerializeField] private float _upWardForce = 1;
    [SerializeField] private float _driftSpeedPresservation = 1;
    [SerializeField] private AnimationCurve _driftSpeedPreservationCurve;
    [Header("DriftFactor")]
    
    [SerializeField] private AnimationCurve _driftFactorSpeedCurve;
    [SerializeField] private AnimationCurve _driftFactorDotCurve;
    
    [SerializeField] private CinemachineCamera _virtualCamera;
    [SerializeField] private float _baseFow = 60f;
    [SerializeField] private float _speedFow = 70f;
    [SerializeField] private LayerMask _groundMask;
    //[SerializeField]private float _moveSpeed;
    //[SerializeField]private float _rotationSpeed;
    
    private InputAction _moveAction;
    private InputAction _respawnAction;
    private int wheelGrounded;
    [SerializeField] private bool _controlBlock;
    
    public float GetCurrentSpeed{get => Vector3.Dot(transform.forward, _rb.linearVelocity);}
    public float GetNormalizedSpeed{get => Mathf.Clamp01(Mathf.Abs(GetCurrentSpeed)/_tankTopSpeed);}

    public float GetGurentFrontGrip { get => _frontwheelGripFactor.Evaluate(GetNormalizedSpeed); }
    public float GetGurentBackGrip { get => _backwheelGripFactor.Evaluate(GetNormalizedSpeed); }
    public Vector3 GetLinearVelocity{get => _rb.linearVelocity;}
    public float GetGroundedFactor {get => (1-(float)wheelGrounded / 4);}
    public float DriftFactor;
    public float TurnDot;

    void Start() {
        _rb = GetComponent<Rigidbody>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _respawnAction =  InputSystem.actions.FindAction("Respawn");
        
        StaticEvent.OnGameStart += InstanceOnOnGameStart;
        StaticEvent.OnBlockPlayerControl+= StaticEventOnOnBlockPlayerControl;
        _respawnAction.started += DoRespawn;
    }

    private void DoRespawn(InputAction.CallbackContext ctx) {
        if (StaticEvent.RespawnPad != null)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            StaticEvent.RespawnPad.RespawnPlayer(gameObject);
            _rb.position = transform.position;
            _rb.rotation = transform.rotation;
            _virtualCamera.PreviousStateIsValid = false;
        }
    }

    private void OnDestroy() {
        StaticEvent.OnGameStart -= InstanceOnOnGameStart;
        StaticEvent.OnBlockPlayerControl-= StaticEventOnOnBlockPlayerControl;
        _respawnAction.started -= DoRespawn;
    }

    private void StaticEventOnOnBlockPlayerControl(object sender, bool e)=> _controlBlock = e;
    

    private void InstanceOnOnGameStart(object sender, EventArgs e) {
        _controlBlock = false;
    }

    // Update is called once per frame
    void Update() {
        
        //ManagerMovement();
        if( !_controlBlock) ManagerRotation();
        CalculateDriftFactor();
        ManageCameraFow();
        
    }

    private void FixedUpdate() {
        ManageHover();
        ManagerStayUpWard();
    }

    private void CalculateDriftFactor()
    {
        float dot = _driftFactorDotCurve.Evaluate(Vector3.Dot(transform.forward, _rb.linearVelocity.normalized));
        float speed = _driftFactorSpeedCurve.Evaluate(_rb.linearVelocity.magnitude / _tankTopSpeed);
        DriftFactor = dot * speed*GetGroundedFactor;

        if (_rb.linearVelocity.magnitude > 1)
        {
            TurnDot = Vector3.Dot(transform.right, _rb.linearVelocity.normalized);
        }
        else {
            TurnDot = 0;
        }
    }

    private void ManageHover() {
        Vector2 inputmovement = _moveAction.ReadValue<Vector2>();
        foreach (var wheel in _frontWheels) {
           ManagerWheel(wheel, inputmovement, true);
        }
        foreach (var wheel in _backWheels) {
            ManagerWheel(wheel, inputmovement, false);
        }
    }

    private void ManagerWheel(Transform wheel, Vector2 inputmovement, bool isFrontWheel)
    {
        wheelGrounded = 0;
        
        RaycastHit hit;
        if (Physics.Raycast(wheel.transform.position, -wheel.transform.up, out hit, _wheelRaycastDistance,
                _groundMask))
        {
            wheelGrounded++;
            Vector3 springDir = wheel.up;
            Vector3 tireWorldVel = _rb.GetPointVelocity(wheel.position);
            Vector3 accelDir = wheel.forward;
            
            float carSpeed = Vector3.Dot(transform.forward, _rb.linearVelocity);
            float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed)/_tankTopSpeed);
            
            //Hover
            float offset = _hoverHeight - hit.distance;
            float vel = Vector3.Dot(springDir, tireWorldVel);
            float force = (offset * _springForce) - (vel * _springDamper);
            _rb.AddForceAtPosition(springDir * force, wheel.position);
              
            //Grip
            Vector3 steeringDir = wheel.right;
            float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
            float gripFactor;
            if (isFrontWheel) gripFactor = _frontwheelGripFactor.Evaluate(normalizedSpeed);
            else gripFactor = _backwheelGripFactor.Evaluate(normalizedSpeed);
            
            //Debug.DrawLine(wheel.position, wheel.position+steeringDir,  Color.red);
            //Debug.DrawLine(wheel.position, wheel.position+tireWorldVel,  Color.blue);
            //Debug.DrawLine(wheel.position, wheel.position+steeringVel*steeringDir,  Color.green);
            
            float desiredVelChange = -steeringVel * gripFactor;
            float desiredAccel = desiredVelChange*Time.fixedDeltaTime;
            _rb.AddForceAtPosition(steeringDir*_wheelMass * desiredAccel, wheel.position);

            float driftPreservationFactor =
                _driftSpeedPreservationCurve.Evaluate(normalizedSpeed) * _driftSpeedPresservation;
            _rb.AddForceAtPosition(accelDir * driftPreservationFactor * Mathf.Abs(desiredAccel), wheel.position);
                
            
            //Accelration
            if( _controlBlock)return;
            if (inputmovement.y > 0) {
                float availableAccelecration = _accelerationCurve.Evaluate(normalizedSpeed) * inputmovement.y*TankAccelleration;
                _rb.AddForceAtPosition(accelDir * availableAccelecration, wheel.position);
            }

            if (inputmovement.y < 0 )
            {
                float availableAccelecration = _accelerationCurve.Evaluate(normalizedSpeed) * inputmovement.y*TankAccelleration;
                _rb.AddForceAtPosition(accelDir * availableAccelecration, wheel.position);
            }

        }
    }
    //private void ManagerMovement()
    //{
    //    Vector2 inputmovement = _moveAction.ReadValue<Vector2>();
    //    Vector3 movement = transform.forward*( inputmovement.y * _moveSpeed * Time.deltaTime);
    //    _rb.AddForce(movement);
    //}

    private void ManagerRotation() {
        Vector2 inputmovement = _moveAction.ReadValue<Vector2>();
        foreach (var wheel in _frontWheels) {
            float angle = _wheelMaxAngle *inputmovement.x;
            wheel.localEulerAngles = new Vector3(0,angle,0);
        }
        //Vector2 inputmovement = _moveAction.ReadValue<Vector2>();
        //Vector3 movement = transform.up*( inputmovement.x* _rotationSpeed * Time.deltaTime);
        //_rb.AddTorque(movement);
    }

    private void ManagerStayUpWard() {
      // float upwardPower = 1-Vector3.Dot(transform.up, Vector3.up);
      // Quaternion angleToStayUp = Quaternion.FromToRotation(transform.up, Vector3.up);
      // Vector3 ealer = angleToStayUp.eulerAngles;
      // float vel = Vector3.Dot(Vector3.up, _rb.angularVelocity);
      // float force = (upwardPower*_upWardForce) - (vel*_upwardDamper);
      // _rb.AddTorque((ealer*force)*Time.fixedDeltaTime);
      Quaternion testqua = new Quaternion();
      testqua.SetFromToRotation(transform.up,  Vector3.up);
      testqua.ToAngleAxis(out float angle , out Vector3 axis);
      _rb.AddTorque((angle*Mathf.Deg2Rad)*axis*_upWardForce*GetGroundedFactor);
        
        
    }

    private void ManageCameraFow() {
        float normalizeSpeed = _rb.linearVelocity.magnitude/20;
       _virtualCamera.Lens.FieldOfView = Mathf.Lerp(_baseFow, _speedFow, normalizeSpeed);
    }
    public void GiveBoost(float boostPower) {
        _rb.AddForce(transform.forward * boostPower,  ForceMode.Impulse);
    }
}