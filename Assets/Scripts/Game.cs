using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private List<string> guesses = new List<string>();
    private string answer;

    public Game(string answer)
    {
        this.answer = answer;
    }


    public string getAnswerWord()
    {
        return answer;
    }

    public List<Color> Guess(string guess)
    {
        if (!Words.isValidWord(guess))
        {
            return new List<Color>();
        }
        guesses.Add(guess);
        return Coloring(guess);
    }


    public int getGuessNum()
    {
        return guesses.Count;
    }


    public List<Color> Coloring(string guess)
    {
        List<Color> colors = new List<Color>();
        List<char> remainingLetters = new List<char>(answer.ToCharArray());
        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i] == guess[i])
            {
                colors.Add(Color.green);
                remainingLetters.Remove(answer[i]);
            }
            else if (remainingLetters.Contains(guess[i]))
            {
                colors.Add(Color.yellow);
                remainingLetters.Remove(guess[i]);
            }
            else
            {
                colors.Add(Color.grey);
            }
        }
        return colors;
    }


}
