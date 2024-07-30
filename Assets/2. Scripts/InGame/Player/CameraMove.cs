using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degrees to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    [SerializeField]
    private float _mouseSensitivity = 300f;
    private Quaternion _previousRotation;
    private Vector2 _input;

    private const float _threshold = 0.01f;

    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        CameraRotation();
        CinemachineCameraTarget.transform.rotation = Quaternion.Lerp(_previousRotation, CinemachineCameraTarget.transform.rotation, Time.deltaTime * 10f);
        _previousRotation = CinemachineCameraTarget.transform.rotation;
    }

    private void CameraRotation()
    {
        GetLookInput();

        if (_input.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            _cinemachineTargetYaw += _input.x * Time.deltaTime * _mouseSensitivity;
            _cinemachineTargetPitch -= _input.y * Time.deltaTime * _mouseSensitivity;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private void GetLookInput()
    {
        _input.x = Input.GetAxis("Mouse X");
        _input.y = Input.GetAxis("Mouse Y");
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
