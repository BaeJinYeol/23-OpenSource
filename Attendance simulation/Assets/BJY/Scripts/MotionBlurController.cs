using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MotionBlurController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    MotionBlur motionBlur;
    public float maxSpeed = 12f;
    private Rigidbody rb;
    private Vector3 previousPosition;
    void Start()
    {
        // Post Processing 효과 초기화
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponent<Rigidbody>();
        previousPosition = rb.position;

    }

    void FixedUpdate()
    {
        // 현재 위치와 이전 위치의 차이 계산
        Vector3 velocity = (rb.position - previousPosition) / Time.fixedDeltaTime;
        // 속도에 따라 모션 블러 강도 조절
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 360, velocity.magnitude / maxSpeed);
        // 이전 위치 업데이트
        previousPosition = rb.position;
    }
}