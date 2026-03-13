using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("보트가 앞으로 나아가는 속도입니다.")]
    public float moveSpeed = 0.5f;

    [Header("Animation Settings")]
    [Tooltip("상하 출렁임 속도")]
    public float bobbingSpeed = 1.0f;
    [Tooltip("상하 출렁임 높이")]
    public float bobbingAmount = 0.05f;

    [Tooltip("꿀렁임(회전) 각도")]
    public float rockAngle = 2.0f;
    [Tooltip("꿀렁임(회전) 속도")]
    public float rockSpeed = 1.0f;

    private float _startY;          // 최초 Y 높이 캐싱
    private Vector3 _startRotation; // 최초 회전값 캐싱

    void Start()
    {
        // 보트가 씬에 배치된 최초의 높이와 회전값을 기억합니다.
        _startY = transform.position.y;
        _startRotation = transform.localEulerAngles;
    }

    void Update()
    {
        // 1. 이동: 초록색 화살표(로컬 Y축, Vector3.up) 방향으로 스르륵 전진합니다.
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);

        // 2. 상하 출렁임: 시작 높이를 기준으로 위아래로 부드럽게 움직입니다.
        float newY = _startY + (Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount);
        
        // 이동한 X, Z 위치는 유지하되 높이(Y)만 출렁이게 덮어씁니다. (물 속으로 가라앉는 것 방지)
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 3. 꿀렁임 퍼포먼스: 파도를 타며 배가 살짝 기우뚱거리는 효과
        float wave1 = Mathf.Sin(Time.time * rockSpeed) * rockAngle;
        float wave2 = Mathf.Cos(Time.time * rockSpeed * 0.8f) * rockAngle;

        // 초록축(Y)이 앞인 경우, X축 회전이 앞뒤(Pitch) 들림이고, Y축 회전이 좌우(Roll) 갸우뚱일 확률이 높습니다.
        // 시작 회전값을 기반으로 작은 각도만 더해주어 배가 벌러덩 눕는 것을 방지합니다.
        transform.localRotation = Quaternion.Euler(
            _startRotation.x + wave1,
            _startRotation.y + wave2,
            _startRotation.z
        );
    }
}
