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
        //�ִ�ӵ� �ʱ�ȭ
        maxSpeed = 16f;

        //��� �� ���� �ʱ�ȭ
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponent<Rigidbody>();
        previousPosition = rb.position;

        //SpeedLine ���� �ʱ�ȭ
        speedline_UI = GameObject.Find("SpeedLine_UI");
        rawimage = speedline_UI.GetComponent<RawImage>();
        speedline_VideoPlayer = GameObject.Find("SpeedLine_VideoPlayer");
        vp = speedline_VideoPlayer.GetComponent<VideoPlayer>();
    }
    void FixedUpdate()
    {
        //�ӵ� ���ϱ�
        Vector3 velocity = (rb.position - previousPosition) / Time.fixedDeltaTime;
        float moveSpeedRatio = velocity.magnitude / maxSpeed;
        Debug.Log(velocity.magnitude);
        //��� �� �� SpeedLine ����
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 360, moveSpeedRatio);
        vp.playbackSpeed = Mathf.Lerp(0.5f, 2f, moveSpeedRatio);
        rawimage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, moveSpeedRatio));
        
        //position �缳��
        previousPosition = rb.position;
    }
}