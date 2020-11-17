using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName ="TMP Date Validator", menuName ="TMPExtensions/Date Validator", order = 1)]
public class TMPDateInputValidator : TMP_InputValidator
{
    /// <summary>
    /// Override Validate method to implement your own validation
    /// </summary>
    /// <param name="text">This is a reference pointer to the actual text in the input field; changes made to this text argument will also result in changes made to text shown in the input field</param>
    /// <param name="pos">This is a reference pointer to the input field's text insertion index position (your blinking caret cursor); changing this value will also change the index of the input field's insertion position</param>
    /// <param name="ch">This is the character being typed into the input field</param>
    /// <returns>Return the character you'd allow into </returns>

    private const string DATE_FORMAT = "{0}-{1}-{2}";

    private int day = -1;
    private int month = -1;
    private int year = -1;
    public void Reset()
    {
        Debug.Log("Reset validator");

        day = -1;
        month = -1;
        year = -1;
    }

    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (char.IsNumber(ch))
        {
            if (pos < 3)
            {
                if (day < 0)
                {
                    if (ch.Equals('0'))
                    {
                        pos++;
                    }
                    day = int.Parse(ch.ToString());
                }
                else
                {
                    if (day == 0)
                        day += int.Parse(ch.ToString());
                    else
                    {
                        int check = (day * 10) + int.Parse(ch.ToString());
                        if (check <= 31)
                            day = check;
                        else
                        {
                            pos++;
                            Validate(ref text, ref pos, ch);
                            return '\0';
                        }
                        pos++;
                    }
                }
            }
            else if (pos < 6)
            {
                if (month < 0)
                {
                    if (ch.Equals('0'))
                    {
                        pos++;
                    }
                    month = int.Parse(ch.ToString());
                }
                else
                {
                    if (month == 0)
                        month += int.Parse(ch.ToString());
                    else
                    {
                        int check = (month * 10) + int.Parse(ch.ToString());
                        if (check <= 12)
                            month = check;
                        else
                        {
                            pos++;
                            Validate(ref text, ref pos, ch);
                            return '\0';
                        }
                        pos++;
                    }
                }
            }
            else if (pos < 10)
            {
                if (year < 0)
                {
                    if (ch.Equals('0'))
                    {
                        pos++;
                    }
                    year = int.Parse(ch.ToString());
                }
                else
                {
                    if (year == 0)
                        year += int.Parse(ch.ToString());
                    else
                    {
                        year = (year * 10) + int.Parse(ch.ToString());
                    }
                }
            }

            text = string.Format(DATE_FORMAT, day < 0 ? "DD" : day.ToString("D2"), month < 0 ? "MM" : month.ToString("D2"), year < 0 ? "YYYY" : year.ToString("D4"));
            pos++;
            return ch;

        }

        text = string.Format(DATE_FORMAT, day < 0 ? "DD" : day.ToString("D2"),month < 0 ? "MM" : month.ToString("D2"), year < 0 ? "YYYY" : year.ToString("D4"));

        return '\0';
    }

    public char ValidateOld(ref string text, ref int pos, char ch)
    {
        if (char.IsNumber(ch))
        {

            int day = 0;
            int month = 0;
            int year = 0;

            string[] split = text.Split('-');
            Debug.Log("Text: " + text);
            Debug.Log("Pos: " + pos);
            if (split.Length == 1)
            {
                day = int.Parse(ch + "");
                pos++;
            }
            else {
                if (pos < 3)
                {
                    int value = 0;
                    if (int.TryParse(split[0], out value))
                    {
                        int check = (value * 10) + int.Parse(ch + "");
                        if (check <= 31)
                            day = check;
                        else
                        {
                            return '\0';
                        }
                    }
                }
                else if(pos < 6)
                {
                    int value = 0;
                    day = int.Parse(split[0]);
                    if (int.TryParse(split[1], out value))
                    {
                        Debug.Log("Split " + split[1] + " / value: " + value);
                        if (value > 0)
                        {
                            int check = (value * 10) + int.Parse(ch + "");
                            if (check <= 12)
                                month = check;
                            else
                            {
                                return '\0';
                            }
                            pos++;
                        }
                    }
                    else
                    {
                        month = int.Parse(ch + "");
                    }
                }
                else if(pos < 10)
                {
                    int value = 0;
                    day = int.Parse(split[0]);
                    month = int.Parse(split[1]);
                    if (int.TryParse(split[2], out value))
                    {
                        Debug.Log("Split " + split[1] + " / value: " + value);
                        if (value > 0)
                        {
                            year = (value * 10) + int.Parse(ch + "");
                        }
                    }
                    else
                    {
                        year = int.Parse(ch + "");
                    }
                }
                else
                {
                    return '\0';
                }
            }


            text = string.Format(DATE_FORMAT, day == 0 ? ".." : day.ToString(), month == 0 ? ".." : month.ToString(), year == 0 ? "...." : year.ToString("D4"));
            pos++;
            return ch;
        }
        return '\0';

    }
}
