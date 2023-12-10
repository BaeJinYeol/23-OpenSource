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
        // Post Processing ȿ�� �ʱ�ȭ
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        rb = GetComponent<Rigidbody>();
        previousPosition = rb.position;

    }

    void FixedUpdate()
    {
        // ���� ��ġ�� ���� ��ġ�� ���� ���
        Vector3 velocity = (rb.position - previousPosition) / Time.fixedDeltaTime;
        // �ӵ��� ���� ��� �� ���� ����
        motionBlur.shutterAngle.value = Mathf.Lerp(0, 360, velocity.magnitude / maxSpeed);
        // ���� ��ġ ������Ʈ
        previousPosition = rb.position;
    }
}