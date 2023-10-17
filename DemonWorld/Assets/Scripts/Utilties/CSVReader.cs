using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader
{
    private string[,] csvDataString;
    private TextAsset csvText;

    public CSVReader(TextAsset csvText)
    {
        this.csvText = csvText;
        ReadDataFromCSV();
    }

    public void ReadDataFromCSV()
    { 
            string[] rows = csvText.ToString().Split('\n');
            int numRows = rows.Length;
            int numColumns = rows[0].Split(',').Length;
            csvDataString = new string[numRows, numColumns];

            for (int j = 0; j < numRows; j++)
            {
                string[] values = rows[j].Split(',');
                for (int k = 0; k < numColumns; k++)
                {
                csvDataString[j, k] = values[k];
                }
            }

            csvDataString = Rotate2DArrayClockwise(csvDataString, 5);

    }
    public static T[,] Rotate2DArrayClockwise<T>(T[,] inputArray, int numRotations)
    {
        int rows = inputArray.GetLength(0);
        int columns = inputArray.GetLength(1);


        int effectiveRotations = numRotations % 4;


        T[,] rotatedArray = new T[columns, rows];


        for (int rotation = 0; rotation < effectiveRotations; rotation++)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    rotatedArray[j, rows - 1 - i] = inputArray[i, j];
                }
            }


            inputArray = rotatedArray;

            int temp = rows;
            rows = columns;
            columns = temp;
        }

        return rotatedArray;
    }

    public string[,] GetParsedCSV() => csvDataString;



}
