using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.Video;

public class MotionController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private MotionBlur motionBlur;
    private Rigidbody rb;
    private Vector3 previousPosition;
    private Vector3 velocity;

    private float maxSpeed;
    private float moveSpeedRatio;
    private RawImage rawimage;
    void Start()
    {
        //속도 관련 모듈 초기화
        maxSpeed = 17f;

        //모션 블러 관련 초기화
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponentInChildren<Rigidbody>();
        previousPosition = rb.position;

        //SpeedLine 관련 초기화
        rawimage = GameObject.Find("SpeedLine_UI").GetComponent<RawImage>();
    }
    private void Update()
    {
        //속도 구하기
        velocity = (rb.position - previousPosition) / Time.deltaTime;
        Debug.Log(velocity.magnitude);
        
        //속도 값 업데이트
        moveSpeedRatio = velocity.magnitude / maxSpeed;

        //모션 블러 및 SpeedLine 적용
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 100, moveSpeedRatio);
        if (GameManager.Instance.isRide)
            rawimage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 0.5f, moveSpeedRatio));
        else
            rawimage.color = new Color(1f, 1f, 1f, 0f);
        //position 재설정
        previousPosition = rb.position;
    }
}