using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 0.1f;       // 보트의 전진 속도
    public Vector3 moveDirection = Vector3.forward; // 이동 방향 (기본: Z축 앞쪽)
    [Header("Bobbing Settings (위아래 출렁임)")]
    public float bobbingSpeed = 1.5f;    // 출렁이는 속도
    public float bobbingAmount = 0.05f;   // 출렁이는 높이
    [Header("Rocking Settings (좌우/앞뒤 흔들림)")]
    public float rockSpeed = 1.0f;       // 흔들리는 속도
    public float rockAngle = 3.0f;       // 흔들리는 각도
    private float defaultY; // 보트의 초기 Y축(높이) 값
    void Start()
    {
        // 보트의 처음 물 위 높이를 저장해둡니다.
        defaultY = transform.position.y;
    }
    void Update()
    {
        // 1. 앞으로 이동
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
        // 2. 위아래 출렁임 (Mathf.Sin 사용)
        float newY = defaultY + (Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        // 3. 앞뒤/좌우 흔들림 (Rotation)
        // Cos과 Sin을 섞어 쓰면 패턴이 단순해지지 않고 더 자연스럽습니다.
        float pitch = Mathf.Sin(Time.time * rockSpeed) * rockAngle; // 앞뒤 흔들림
        float roll = Mathf.Cos(Time.time * rockSpeed * 0.8f) * rockAngle; // 좌우 흔들림
        // 흔들림 각도를 보트의 회전값에 적용합니다.
        transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y, roll);
    }
}
