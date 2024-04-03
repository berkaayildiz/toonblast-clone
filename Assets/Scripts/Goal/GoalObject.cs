using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The GoalObject class represents a goal in a level. It holds a reference to a LevelGoal object and tracks the current count towards that goal.
/// It provides methods to prepare the goal object, decrease the goal count, and check if the goal is completed.
/// When the goal count reaches zero, it marks the goal as completed and plays a completion effect.
/// </summary>
public class GoalObject : MonoBehaviour
{
    [SerializeField] private Image goalImage;
    [SerializeField] private Image completedMarkImage;
    [SerializeField] private TextMeshProUGUI goalCountText;
    [SerializeField] private ParticleSystem completionEffect;

    private int goalCount;
    private LevelGoal levelGoal;

    public LevelGoal LevelGoal => levelGoal;

    public void Prepare(LevelGoal goal)
    {
        levelGoal = goal;
        var goalSprite = ItemImageLibrary.Instance.GetSpriteForItemType(levelGoal.ItemType);
        goalImage.sprite = goalSprite;

        goalCount = levelGoal.Count;
        UpdateGoalCountText();
    }

    public void DecreaseCount()
    {
        goalCount--;
        UpdateGoalState();
    }

    private void UpdateGoalState()
    {
        if (goalCount <= 0)
        {
            MarkGoalAsCompleted();
            return;
        }

        UpdateGoalCountText();
        PlayDecreaseEffect();
    }

    private void UpdateGoalCountText()
    {
        goalCountText.text = goalCount.ToString();
    }

    private void MarkGoalAsCompleted()
    {
        goalCount = 0;
        goalCountText.gameObject.SetActive(false);
        completedMarkImage.gameObject.SetActive(true);
    }

    private void PlayDecreaseEffect()
    {
        completionEffect.Play();
    }

    public bool IsCompleted()
    {
        return goalCount <= 0;
    }
}
