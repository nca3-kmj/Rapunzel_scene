using UnityEngine;

public class LightMoving : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("이동 속도입니다.")]
    public float speed = 0.03f;
    
    [Tooltip("기본 높낮이 한계값입니다. 시작할 때 랜덤(0.01 ~ 0.04)으로 재설정됩니다.")]
    public float limit = 0.02f;   

    [Header("Random Delay Settings")]
    [Tooltip("시작 시 최대 대기 시간(초)입니다. 0부터 이 값 사이의 랜덤한 시간동안 멈춰있다가 움직입니다.")]
    public float maxDelay = 3.0f;

    private float startY;         // 초기 Y 높이
    private float currentOffset = 0f; // 현재 이동 오프셋
    private int direction = 1;    // 1이면 위, -1이면 아래

    private float currentDelay;   // 각각 부여받은 남은 대기 시간

    void Start()
    {
        // 게임 시작 시 원래 높이를 기억합니다.
        startY = transform.position.y;

        // [추가] 각 객체마다 0초 ~ maxDelay(3초) 사이의 랜덤 딜레이를 부여합니다.
        currentDelay = Random.Range(0f, maxDelay);

        // [추가] 사용자의 요청대로 움직이는 높낮이 한계(Limit)를 0.02 ~ 0.1 사이로 랜덤하게 지정합니다.
        // 이렇게 하면 수많은 등불이 각자 다른 높이까지 올라갔다가 내려와 훨씬 자연스럽습니다.
        limit = Random.Range(0.02f, 0.1f);
        
        // [추가] 움직이는 속도 역시 0.03 ~ 0.05 사이로 랜덤하게 지정하여 개체마다 속도를 다르게 합니다.
        speed = Random.Range(0.03f, 0.04f);
    }

    void Update()
    {
        // 랜덤으로 부여받은 딜레이 시간이 남아있다면 그 시간 동안 대기합니다.
        if (currentDelay > 0)
        {
            currentDelay -= Time.deltaTime; 
            return; // 딜레이가 안 끝났으면 아래 코드를 무시하고 대기
        }

        // 1. 프레임당 이동할 거리를 구합니다. (방향 포함)
        float moveAmount = speed * Time.deltaTime * direction;

        // 2. 현재 오프셋에 이동량을 더합니다.
        currentOffset += moveAmount;

        // 3. 한계점(Limit) 도달 시 방향을 바꿉니다.
        // 최고점 도달 시 아래(-1)로 방향 전환
        if (currentOffset >= limit)
        {
            direction = -1;
            currentOffset = limit; 
        }
        // 최저점 도달 시 위(1)로 방향 전환
        else if (currentOffset <= -limit)
        {
            direction = 1;
            currentOffset = -limit; 
        }

        // 4. 새로운 위치를 적용합니다.
        transform.position = new Vector3(transform.position.x, startY + currentOffset, transform.position.z);
    }
}