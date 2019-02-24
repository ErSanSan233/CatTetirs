using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shape : MonoBehaviour
{
    public float speed = 1.0f;
    float lastMoveDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsInGrid())
        {
            //Sound Here
            //SoundManager.Instance.PlayOneShot(SoundManager.Instance.gameOver);
            Invoke("OpenGameOverScene", .2f);
        }

    }

    void OpenGameOverScene()
    {
        Destroy(gameObject);    
        SceneManager.LoadScene ("GameOver");

    }
            


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            transform.position += new Vector3(-1, 0, 0);
            Debug.Log(transform.position);
            if (!IsInGrid())
            {
                transform.position += new Vector3(1, 0, 0);
            }
            else
            {
                UpadateGameBoard();
            }
        }

        if (Input.GetKey("w"))
        {
            transform.Rotate(0, 0, 90);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
            if (!IsInGrid())
            {
                transform.Rotate(0, 0, -90);
            }
            else
            {
                UpadateGameBoard();
            }

        }

        if (Input.GetKey("e"))
        {
            transform.Rotate(0, 0, -90);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
            if (!IsInGrid())
            {
                transform.Rotate(0, 0, 90);

            }
            else
            {
                UpadateGameBoard();
            }

        }

        if (Input.GetKey("s") || Time.time - lastMoveDown >= 1)
        {
            transform.position += new Vector3(0, -1, 0);
        
            if (!IsInGrid())
            {
                transform.position += new Vector3(0, 1, 0);

                bool rowDeleted = GameBoard.DeleteAllFullRows();
                if (rowDeleted)
                {

                    GameBoard.DeleteAllFullRows();
                    IncreaseTextUIScore();
                }

                enabled = false;

                FindObjectOfType<ShapeSpawner>().SpawnShape();
            }
            else
            {
                UpadateGameBoard();
            }

            lastMoveDown = Time.time;
        }


        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(1, 0, 0);
            Debug.Log(transform.position);
            if (!IsInGrid())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            else
            {
                UpadateGameBoard();
            }
        }

    }

    public Vector2 RoundVector(Vector2 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    public bool IsInGrid()
    {
        foreach(Transform childBlock in transform)
        {
            Vector2 vect = RoundVector(childBlock.position);

            if (!IsInBorder(vect))
            {
                return false;
            }
            if (GameBoard.gameBoard[(int) vect.x, (int)vect.y ] != null &&
            GameBoard.gameBoard[(int)vect.x, (int)vect.y].parent != transform)
            {
                return false;
            }

        }
        return true;
    }


    public static bool IsInBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x <= 10 &&
                (int)pos.y >= 0);
    }


    public void UpadateGameBoard()
    {
        for(int y = 0; y <20; ++y)
        {

            for( int x = 0; x<11; ++x)
            {
                if (GameBoard.gameBoard[x,y]!=null &&
                GameBoard.gameBoard[x, y].parent == transform)
                {
                    GameBoard.gameBoard[x, y] = null;
                }
            }
        }

        foreach(Transform childBlock in transform)
        {
            Vector2 vect = RoundVector(childBlock.position);
                GameBoard.gameBoard[(int)vect.x, (int)vect.y] = childBlock;
                Debug.Log("Cube At : " + vect.x + " " + vect.y);
        }

    }

    // Increases the score the text UI 
    void IncreaseTextUIScore()
    {
        // Find the matching text UI component
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        // Get the string stored in it and convert to an int
        int score = int.Parse(textUIComp.text);

        // Increment it
        score++;

        // Save new score in Text UI
        textUIComp.text = score.ToString();
    }
}
