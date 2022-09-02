using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    private List<List<GameObject>> _grid;

    // [SerializeField]
    // private List<List<GameObject>> rows;
    // Start is called before the first frame update
    async void Start()
    {
        _grid = new List<List<GameObject>>();
        GameObject gridElement = (GameObject)Resources.Load("LetterContainer");
        for (int i = 0; i < 6; ++i)
        {
            _grid.Add(new List<GameObject>());
            for (int j = 0; j < 5; ++j)
            {
                GameObject newGridElement = Instantiate(gridElement, transform);
                _grid[i].Add(newGridElement);
                newGridElement.GetComponentInChildren<Text>().text = "";
            }
        }

        // UpdateElement(3, 1, "F", Color.red);
    }

    public void UpdateElement(int row, int col, string letter, Color color)
    {
        _grid[row][col].GetComponentInChildren<Text>().text = letter;
        _grid[row][col].GetComponent<Image>().color = color;
    }

    public void UpdateElementText(int row, int col, string letter)
    {
        _grid[row][col].GetComponentInChildren<Text>().text = letter;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
