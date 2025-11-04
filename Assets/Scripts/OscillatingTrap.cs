using UnityEngine;

public class OscillatingTrap : MonoBehaviour
{
    [SerializeField] private Transform upPosition;
    [SerializeField] private Transform downPosition;
    [SerializeField] private float speed = 3.0f;

    private Transform targetPoint;
    private bool isActivated = false;

    void Start()
    {
        if (upPosition != null)
        {
            transform.position = upPosition.position;
            targetPoint = downPosition;
        }
    }

    void Update()
    {
        if (!isActivated || upPosition == null || downPosition == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            if (targetPoint == downPosition)
            {
                targetPoint = upPosition;
            }
            else
            {
                targetPoint = downPosition;
            }
        }
    }

    public void ActivateTrap()
    {
        isActivated = true;
    }
}