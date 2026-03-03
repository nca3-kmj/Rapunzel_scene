using UnityEngine;

public class LightMoving : MonoBehaviour
{
    public float speed = 0.4f; // 이동 속도 (매우 낮게 설정)
    public float limit = 0.2f;   // 위아래 이동 제한 범위 (매우 좁게 설정)

    private float startY;         // 처음 시작 높이
    private float currentOffset = 0f; // 현재 이동한 양
    private int direction = 1;    // 1이면 위, -1이면 아래

    //랜덤딜레이
    public float maxDelay = 3.0f; // 최대 대기 시간 (0~3초 사이 랜덤)
    private float currentDelay;   // 이 개체에 할당된 랜덤 딜레이 값

    void Start()
    {
        startY = transform.position.y;

        // [추가] 시작 시 0에서 maxDelay 사이의 랜덤한 딜레이 값을 부여합니다.
        currentDelay = Random.Range(0f, maxDelay);
    }

    void Update()
    {

        // [추가] 부여된 딜레이가 아직 남아있다면 연산을 수행하지 않고 기다립니다.
        if (currentDelay > 0)
        {
            currentDelay -= Time.deltaTime; // 프레임마다 시간을 깎음
            return; // 아래의 이동 연산 코드를 실행하지 않고 여기서 종료
        }

        // 1. 매 프레임마다 조금씩 이동할 양 계산
        float moveAmount = speed * Time.deltaTime * direction;

        // 2. 현재 오프셋 업데이트
        currentOffset += moveAmount;

        // 3. if문을 사용한 방향 반전 로직
        // 최고 높이에 도달하면 아래로(-1) 방향 전환
        if (currentOffset >= limit)
        {
            direction = -1;
            currentOffset = limit; // 위치 고정 (범위 이탈 방지)
        }
        // 최저 높이에 도달하면 위로(1) 방향 전환
        else if (currentOffset <= -limit)
        {
            direction = 1;
            currentOffset = -limit; // 위치 고정
        }

        // 4. 실제 좌표에 적용
        transform.position = new Vector3(transform.position.x, startY + currentOffset, transform.position.z);
    }
}
    