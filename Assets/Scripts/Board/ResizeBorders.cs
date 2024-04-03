using UnityEngine;

/// <summary>
/// 
/// The ResizeBorders class is responsible for adjusting the size of the game board's borders.
/// 
/// </summary>
public class ResizeBorders : MonoBehaviour
{
    [SerializeField] private GameGrid gameGrid;
    private const float WIDTH_PADDING = 0.35f;
    private const float HEIGHT_PADDING = 0.45f;

    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float newWidth = gameGrid.levelInfo.grid_width + WIDTH_PADDING;
        float newHeight = gameGrid.levelInfo.grid_height + HEIGHT_PADDING;

        sr.size = new Vector2(newWidth, newHeight);
    }
}
