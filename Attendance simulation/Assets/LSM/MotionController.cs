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
    private VideoPlayer vp;

    void Start()
    {
        //속도 관련 모듈 초기화
        maxSpeed = 16f;

        //모션 블러 관련 초기화
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponent<Rigidbody>();
        previousPosition = rb.position;

        //SpeedLine 관련 초기화
        GameObject speedline_UI = GameObject.Find("SpeedLine_UI");
        rawimage = speedline_UI.GetComponent<RawImage>();
        GameObject speedline_VideoPlayer = GameObject.Find("SpeedLine_VideoPlayer");
        vp = speedline_VideoPlayer.GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        //속도 값 업데이트
        moveSpeedRatio = velocity.magnitude / maxSpeed;

        //모션 블러 및 SpeedLine 적용
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 360, moveSpeedRatio);
        vp.playbackSpeed = Mathf.Lerp(0.5f, 2f, moveSpeedRatio);
        rawimage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, moveSpeedRatio));
        
        //position 재설정
        previousPosition = rb.position;
    }
    
    void FixedUpdate()
    {
        //속도 구하기
        velocity = (rb.position - previousPosition) / Time.fixedDeltaTime;
        Debug.Log(velocity.magnitude);
    }
}