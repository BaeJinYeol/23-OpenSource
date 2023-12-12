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
        //�ӵ� ���� ��� �ʱ�ȭ
        maxSpeed = 17f;

        //��� �� ���� �ʱ�ȭ
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponentInChildren<Rigidbody>();
        previousPosition = rb.position;

        //SpeedLine ���� �ʱ�ȭ
        rawimage = GameObject.Find("SpeedLine_UI").GetComponent<RawImage>();
    }
    private void Update()
    {
        //�ӵ� ���ϱ�
        velocity = (rb.position - previousPosition) / Time.deltaTime;
        Debug.Log(velocity.magnitude);
        
        //�ӵ� �� ������Ʈ
        moveSpeedRatio = velocity.magnitude / maxSpeed;

        //��� �� �� SpeedLine ����
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 100, moveSpeedRatio);
        if (GameManager.Instance.isRide)
            rawimage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 0.5f, moveSpeedRatio));
        else
            rawimage.color = new Color(1f, 1f, 1f, 0f);
        //position �缳��
        previousPosition = rb.position;
    }
}