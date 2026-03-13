using UnityEngine;
using System.Collections;

public class BoatCamera : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("따라갈 보트 오브젝트를 하이어라키(Hierarchy) 창에서 드래그해서 끌어다 놓으세요.")]
    public Transform targetBoat;

    [Header("Cinematic Delay Settings")]
    [Tooltip("게임 시작 후 카메라가 움직이기 시작할 때까지의 대기 시간(초)입니다.")]
    public float startDelaySeconds = 1.0f;
    
    [Tooltip("카메라가 보트를 따라붙는 속도입니다. 수치가 작을수록 묵직하고 뒤늦게 따라가는 생동감 느낌이 납니다.")]
    public float followSpeed = 2.0f;

    private bool _canMove = false;
    private Vector3 _initialOffset;

    void Start()
    {
        if (targetBoat != null)
        {
            // 게임 시작 시, 보트와 카메라 사이에 띄워져 있는 초기 거리(간격)를 기억합니다.
            _initialOffset = transform.position - targetBoat.position;
        }
        else
        {
            Debug.LogWarning("BoatCamera 스크립트: Target Boat가 할당되지 않아 카메라가 따라갈 수 없습니다!");
        }

        // 설정한 시간만큼 기다리는 타이머를 시작합니다.
        StartCoroutine(MovementDelayCoroutine());
    }

    IEnumerator MovementDelayCoroutine()
    {
        // 설정한 초(seconds)만큼 기다린 뒤
        yield return new WaitForSeconds(startDelaySeconds);
        // 이동 가능 상태로 바꿉니다.
        _canMove = true;
    }

    void LateUpdate()
    {
        // 보트가 씬에 없거나, 아직 대기 시간 중이라면 움직이지 않고 제자리에 머뭅니다.
        if (targetBoat == null || !_canMove) return;

        // 카메라가 이동해야 할 목표 지점 = (보트의 현재 위치) + (처음 유지했던 거리)
        // 보트가 출렁거리면 카메라도 같이 출렁거리는 것을 원치 않으시면 targetBoat.position 대신 Y값을 고정하는 방법도 있습니다.
        Vector3 targetPosition = targetBoat.position + _initialOffset;

        // 현재 위치에서 목표 위치로 부드럽게(Lerp) 따라갑니다. (생동감 있고 살짝 밀리는 듯한 '고무줄 모션' 연출)
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
