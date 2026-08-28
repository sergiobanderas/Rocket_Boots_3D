using UnityEngine;
using DG.Tweening;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3(0, 3, 0);
    [SerializeField] private float duration = 2f;

    void Start()
    {
        transform.DOLocalMove(
            transform.localPosition + movement,
            duration
        )
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);
    }
}
