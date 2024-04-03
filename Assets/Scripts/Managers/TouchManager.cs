using UnityEngine;

/// <summary>
/// 
/// TouchManager is a MonoBehaviour that listens to the OnMovesFinished and OnGoalsCompleted events from MovesManager and GoalManager.
/// 
/// </summary>
public class TouchManager : MonoBehaviour
{
    private const string cellCollider = "CellCollider";

    [SerializeField] private new Camera camera;
    [SerializeField] private GameGrid board;

    private void Update()
    {
        #if UNITY_EDITOR
            GetTouchEditor();
        #else
            GetTouchMobile();
        #endif
    }
    private void GetTouchEditor()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ExecuteTouch(Input.mousePosition);
        }
    }

    private void GetTouchMobile()
    {
        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                ExecuteTouch(touch.position);
                break;
        }
    }

    private void ExecuteTouch(Vector3 pos)
    {
        var hit = Physics2D.OverlapPoint(camera.ScreenToWorldPoint(pos)) as BoxCollider2D;
        if (hit != null && hit.CompareTag(cellCollider))
        {
            hit.GetComponent<Cell>().CellTapped();
        }
    }
    private void DisableTouch()
    {
        this.enabled = false;
    }

    private void OnEnable()
    {
        MovesManager.Instance.OnMovesFinished += DisableTouch;
        GoalManager.Instance.OnGoalsCompleted += DisableTouch;
    }

    private void OnDisable()
    {
        MovesManager.Instance.OnMovesFinished -= DisableTouch;
        GoalManager.Instance.OnGoalsCompleted -= DisableTouch;
    }
}
