using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    string expression = "";

    void Start()
    {
        displayText.text = "0";
    }

    // Called by number & operator buttons
    public void AddInput(string value)
    {
        expression += value;
        displayText.text = expression;
    }

    // Clear button
    public void Clear()
    {
        expression = "";
        displayText.text = "0";
    }

    // Equals button
    public void Calculate()
    {
        float result = Solve(expression);
        displayText.text = result.ToString();
        expression = result.ToString();
    }

    // =================================
    // SIMPLE MANUAL DMAS CALCULATION
    // =================================
    float Solve(string exp)
    {
        List<string> parts = new List<string>();
        string number = "";

        // Step 1: Split numbers and operators
        for (int i = 0; i < exp.Length; i++)
        {
            char c = exp[i];

            if (char.IsDigit(c) || c == '.')
            {
                number += c;
            }
            else
            {
                parts.Add(number);
                parts.Add(c.ToString());
                number = "";
            }
        }
        parts.Add(number);

        // Step 2: Solve * and /
        for (int i = 0; i < parts.Count; i++)
        {
            if (parts[i] == "*" || parts[i] == "/")
            {
                float a = float.Parse(parts[i - 1]);
                float b = float.Parse(parts[i + 1]);

                float value = (parts[i] == "*") ? a * b : a / b;

                parts[i - 1] = value.ToString();
                parts.RemoveAt(i);     // remove operator
                parts.RemoveAt(i);     // remove second number
                i--;
            }
        }

        // Step 3: Solve + and -
        float result = float.Parse(parts[0]);

        for (int i = 1; i < parts.Count; i += 2)
        {
            float next = float.Parse(parts[i + 1]);

            if (parts[i] == "+")
                result += next;
            else
                result -= next;
        }

        return result;
    }
}
