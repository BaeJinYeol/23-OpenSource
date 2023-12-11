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
    private float maxSpeed;

    private GameObject speedline_UI;
    private RawImage rawimage;
    private GameObject speedline_VideoPlayer;
    private VideoPlayer vp;

    void Start()
    {
        //최대속도 초기화
        maxSpeed = 16f;

        //모션 블러 관련 초기화
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponent<Rigidbody>();
        previousPosition = rb.position;

        //SpeedLine 관련 초기화
        speedline_UI = GameObject.Find("SpeedLine_UI");
        rawimage = speedline_UI.GetComponent<RawImage>();
        speedline_VideoPlayer = GameObject.Find("SpeedLine_VideoPlayer");
        vp = speedline_VideoPlayer.GetComponent<VideoPlayer>();
    }
    void FixedUpdate()
    {
        //속도 구하기
        Vector3 velocity = (rb.position - previousPosition) / Time.fixedDeltaTime;
        float moveSpeedRatio = velocity.magnitude / maxSpeed;
        Debug.Log(velocity.magnitude);
        //모션 블러 및 SpeedLine 적용
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 360, moveSpeedRatio);
        vp.playbackSpeed = Mathf.Lerp(0.5f, 2f, moveSpeedRatio);
        rawimage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, moveSpeedRatio));
        
        //position 재설정
        previousPosition = rb.position;
    }
}