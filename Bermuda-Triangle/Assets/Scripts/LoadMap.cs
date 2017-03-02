using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

/**
 * Created by Daniel Resio
 * Attach to map picture and make sure map is named the same as the json file in the Resources/Maps folder
 * This will decode json files so that coliders can be created
 * Note, block sizes in unity are 1 unit^2
 **/
public class LoadMap : MonoBehaviour {

    #region constants for the border numbers
    private const int leftBorder = 69;
    private const int topBorder = 84;
    private const int rightBorder = 67;
    private const int bottomBorder = 52;
    private const int topLeftBorder = 86;
    private const int topRightBorder = 87;
    private const int bottomRightBorder = 103;
    private const int bottomLeftBorder = 102;
    private const int bottomRightCurveBorder = 51;
    private const int bottomLeftCurveBorder = 53;
    private const int topRightCurveBorder = 83;
    private const int topLeftCurveBorder = 85;
    #endregion

    //this is the squareroot of 2
    private const float angleConst = 0.41421356237f / 2f;

    public GameObject border;
    public GameObject borderEdge;
    public GameObject borderCurve;
    MapData map = new MapData();

    void Start()
    {
        string fileName = gameObject.name;
        //parses map into the map object
        parseMap(fileName);
        createBorder();

    }
	
    /// <summary>
    /// This takes the data from the map, and it converts it into the map object
    /// currently, it only places in the land array. may add other parts later
    /// </summary>
    /// <param name="fileName"></param>
    void parseMap(string fileName)
    {
        string nameLand = "Maps/" + fileName + "_land";
        TextAsset landData = Resources.Load(nameLand) as TextAsset;
        string fs = landData.text;
        string[] fLines = Regex.Split(fs, "\n");
        //assumes file is rectangular
        #region finds the size of the array and stores it in Length and width
        int length, width;
        length = fLines.Length;
        string tempLine = fLines[0];
        string[] temp = Regex.Split(tempLine, ",");
        width = temp.Length;
        #endregion
        int[,] tempArray = new int[length, width];
        for (int i = 0; i < fLines.Length; i++)
        {
            string valueLine = fLines[i];
            string[] values = Regex.Split(valueLine, ","); 
            for(int j = 0; j < values.Length; j++)
            {
                tempArray[i, j] = Int32.Parse(values[j]);
            }
        }

        map.land = tempArray;
    }

    void createBorder()
    {
        
        //handles border checking
        for (int x = 0; x < map.land.GetLength(0); x++)
        {
            for (int y = 0; y < map.land.GetLength(1); y++)
            {
                #region large if statements placing boundries.  Look at your own risk
                if(map.land[x,y] == bottomBorder || map.land[x, y] == topBorder)
                {
                    Instantiate(border, calculateCoords(x, y), Quaternion.Euler(new Vector3(0, 0, 0))); 
                }
                else if (map.land[x, y] == leftBorder || map.land[x, y] == rightBorder)
                {
                    Instantiate(border, calculateCoords(x, y), Quaternion.Euler(new Vector3(0, 0, 90)));
                }
                else if (map.land[x, y] == topLeftBorder)
                {
                    Instantiate(borderEdge, calculateCoords(x +.5f, y), Quaternion.Euler(new Vector3(0, 0, 45)));
                }
                else if(map.land[x, y] == bottomRightBorder)
                {
                    Instantiate(borderEdge, calculateCoords(x-.5f, y), Quaternion.Euler(new Vector3(0, 0, 45)));
                }
                else if(map.land[x, y] == topRightBorder)
                {
                    Instantiate(borderEdge, calculateCoords(x, y -.5f), Quaternion.Euler(new Vector3(0, 0, -45)));
                }
                else if(map.land[x, y] == bottomLeftBorder)
                {
                    Instantiate(borderEdge, calculateCoords(x , y+.5f), Quaternion.Euler(new Vector3(0, 0, -45)));
                }
                else if (map.land[x, y] == topLeftCurveBorder)
                {
                    Instantiate(borderCurve, calculateCoords(x -.25f, y -.25f), Quaternion.Euler(new Vector3(0, 0, 45)));
                }
                else if (map.land[x, y] == topRightCurveBorder)
                {
                    Instantiate(borderCurve, calculateCoords(x - .25f, y + .25f), Quaternion.Euler(new Vector3(0, 0, -45)));
                }
                else if (map.land[x, y] == bottomRightCurveBorder)
                {
                    Instantiate(borderCurve, calculateCoords(x + .25f, y + .25f), Quaternion.Euler(new Vector3(0, 0, 45)));
                }
                else if (map.land[x, y] == bottomLeftCurveBorder)
                {
                    Instantiate(borderCurve, calculateCoords(x + .25f, y - .25f), Quaternion.Euler(new Vector3(0, 0, -45)));
                }
                #endregion
            }
        }
    }

    Vector2 calculateCoords(float initialx, float initialy)
    {
        initialx *= -1;
        initialy *= 1;
        initialy += .5f;
        initialx -= .5f;
        return new Vector2(initialy,initialx);
    }

}

/// <summary>
/// Stores data from map with the files that coencide with the map name in the resources file
/// </summary>
class MapData
{
    /// <summary>
    /// change this to deal with our maps
    /// </summary>
    public int[,] land;

}