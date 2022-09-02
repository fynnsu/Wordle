using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public InputField inputField;

    [SerializeField]
    public GameObject winningScreen;

    [SerializeField]
    public GameObject losingScreen;

    [SerializeField]
    public Text score;

    [SerializeField]
    GridManager gridManager;

    private bool done = false;

    private Game curGame;

    private TouchScreenKeyboard keyboard;
    private string lastInput = "";
    void Start()
    {
        inputField.Select();
        // inputField.ActivateInputField();
        // Open Ios keyboard
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "", 5);
        TouchScreenKeyboard.hideInput = true;

        string answer = Words.getAnswerWord();
        Debug.Log($"Answer is {answer}");

        curGame = new Game(answer);
    }

    public void HandleGuess(string guess)
    {
        Debug.Log($"Guess is {guess}");
        List<Color> colors = curGame.Guess(guess);
        if (colors.Count == 0)
        {
            return;
        }

        int curRow = curGame.getGuessNum() - 1;

        for (int i = 0; i < colors.Count; ++i)
        {
            gridManager.UpdateElement(curRow, i, guess[i].ToString().ToUpper(), colors[i]);
        }


        if (curGame.getAnswerWord().Equals(guess))
        {
            Debug.Log("You win!");
            winningScreen.SetActive(true);
            score.text = $"Score: {curGame.getGuessNum()}";
            done = true;
        }
        else
        {
            if (curRow == 5)
            {
                losingScreen.SetActive(true);
                Debug.Log("Looser!");
                done = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!TouchScreenKeyboard.hideInput)
            TouchScreenKeyboard.hideInput = true;
        if (inputField.text != lastInput)
        {
            if (inputField.text.Length > 5)
            {
                inputField.text = inputField.text.Substring(0, 5);
            }
            int curRow = curGame.getGuessNum();
            for (int i = 0; i < inputField.text.Length; ++i)
            {
                gridManager.UpdateElementText(curRow, i, inputField.text[i].ToString().ToUpper());
                Debug.Log($"Updating element {curRow}, {i} to {inputField.text[i]}");
            }
            for (int i = inputField.text.Length; i < 5; ++i)
            {
                gridManager.UpdateElementText(curRow, i, "");
            }
            lastInput = inputField.text;

            Debug.LogError(lastInput);
        }
        if (keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            Debug.LogError("Enter key was pressed");
            if (keyboard.text.Length == 5)
            {
                HandleGuess(lastInput.ToLower());
            }
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "", 5);
        }
    }
}
