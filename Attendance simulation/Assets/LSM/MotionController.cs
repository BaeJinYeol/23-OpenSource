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
        //�ӵ� ���� ��� �ʱ�ȭ
        maxSpeed = 16f;

        //��� �� ���� �ʱ�ȭ
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponent<Rigidbody>();
        previousPosition = rb.position;

        //SpeedLine ���� �ʱ�ȭ
        GameObject speedline_UI = GameObject.Find("SpeedLine_UI");
        rawimage = speedline_UI.GetComponent<RawImage>();
        GameObject speedline_VideoPlayer = GameObject.Find("SpeedLine_VideoPlayer");
        vp = speedline_VideoPlayer.GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        //�ӵ� �� ������Ʈ
        moveSpeedRatio = velocity.magnitude / maxSpeed;

        //��� �� �� SpeedLine ����
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 360, moveSpeedRatio);
        vp.playbackSpeed = Mathf.Lerp(0.5f, 2f, moveSpeedRatio);
        rawimage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, moveSpeedRatio));
        
        //position �缳��
        previousPosition = rb.position;
    }
    
    void FixedUpdate()
    {
        //�ӵ� ���ϱ�
        velocity = (rb.position - previousPosition) / Time.fixedDeltaTime;
        Debug.Log(velocity.magnitude);
    }
}