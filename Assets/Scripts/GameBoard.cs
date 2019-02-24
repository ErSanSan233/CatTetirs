using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    // Surround all GameObjects with GameObjectGroup
    // and move it the amount of space to make the 
    // gameboard lie at the 0 0 mark

    // Stores all the cubes on the gameboard
    public static Transform[,] gameBoard = new Transform[11, 20];



    public static bool DeleteAllFullRows()
    {
        // Cycle through all rows 
        for (int row = 0; row < 20; ++row)
        {
            // Check for a full row
            if (IsRowFull(row))
            {
                // Delete Row
                DeleteGBRow(row);

                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rowDelete);

                return true;
            }
        }
        return false;
    }

    // This test is done in a 2nd function because it 
    // answers a specific question being is a row full
    public static bool IsRowFull(int row)
    {
        // Cycle through columns and if a null is
        // found return false
        for (int col = 0; col < 11; ++col)
        {
            if (gameBoard[col, row] == null)
            {
                return false;
            }
        }

        return true;

    }

    public static void DeleteGBRow(int row)
    {
        // Cycle through row deleting in both the array
        // as well as in the scene
        for (int col = 0; col < 11; ++col)
        {
            // Destroy the cubes in the scene
            Destroy(gameBoard[col, row].gameObject);
            // Destroy the cubes in the array
            gameBoard[col, row] = null;
        }

        // Increment up a row to start moving them down
        row++;

        // Cycle through all rows
        for (int j = row; j < 20; ++j)
        {
            // Cycle through all columns
            for (int col = 0; col < 11; ++col)
            {
                // Check if there is a block in a cell
                if (gameBoard[col, j] != null)
                {
                    // Move whats above down
                    gameBoard[col, j - 1] = gameBoard[col, j];

                    // Delete the cube that was moved down
                    gameBoard[col, j] = null;

                    // Move the cube in the scene as well
                    gameBoard[col, j - 1].position += new Vector3(0, -1, 0);
                }
            }
        }

    }

}