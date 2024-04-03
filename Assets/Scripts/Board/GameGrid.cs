using UnityEngine;

/// <summary>
/// 
/// The Board class represents the game board in a grid-based game.
/// It is responsible for managing the cells of the game board, including their initialization and preparation.
/// 
/// </summary>
public class GameGrid : MonoBehaviour
{
    public Transform cellsParent;
    public Transform itemsParent;
    public Transform particlesParent;
    [SerializeField] private Cell cellPrefab;

    public LevelInfo levelInfo;

    public int Rows { get; private set; }
    public int Cols { get; private set; }

    public Cell[,] Cells { get; private set; }

    private void Awake()
    {
        LoadLevelInfo();
        InitializeCells();
        PrepareCells();
    }

    private void LoadLevelInfo()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        levelInfo = LevelManager.getLevelInfo(currentLevel);

        Rows = levelInfo.grid_height;
        Cols = levelInfo.grid_width;
    }

    private void InitializeCells()
    {
        Cells = new Cell[Cols, Rows];
        ResizeBoard(Rows, Cols);
        CreateCells();
    }

    private void CreateCells()
    {
        for(int y = 0; y < Rows; y++)
            for (int x = 0; x < Cols; x++)
                Cells[x, y] = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, cellsParent);
    }

    private void PrepareCells()
    {
        for (int y = 0; y < Rows; y++)
            for(int x= 0; x < Cols; x++)
                Cells[x, y].Prepare(x, y, this);
    }


    private void ResizeBoard(int rows, int cols)
    {
        Transform currTrans = this.transform;

        float newX = (9 - cols) * 0.5f;
        float newY = (9 - rows) * 0.5f;

        this.transform.position = new Vector3(newX, newY, currTrans.position.z);
    }
}
