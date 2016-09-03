using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Area : MonoBehaviour {

    public const string SQUARE = "SQUARE";
    public const string CIRCLE = "CIRCLE";
    public const string TRIANGLE = "TRIANGLE";

    //square
    public Text SquareSide;
    public Text SquareArea;

    //triangle
    public Text TriangleBase;
    public Text TriangleHeight;
    public Text TriangleArea;

    //circle
    public Text CircleRadius;
    public Text CircleArea;

    //largest
    public Text LargestAreaName;
    public Text LargestAreaValue;

    private Dictionary<string,double> areas = new Dictionary<string, double>();

	// Use this for initialization
	void Start () {
        ComputeArea();
	}

    public void ComputeArea()
    {
        double squareSide = double.Parse(this.SquareSide.text);
        areas.Add(SQUARE, computeAreaSquare(squareSide));

        double triangleBase = double.Parse(this.TriangleBase.text);
        double triangleHeight = double.Parse(this.TriangleHeight.text);
        areas.Add(TRIANGLE, computeAreaTriangle(triangleBase, triangleHeight));

        double circleRadius = double.Parse(this.CircleRadius.text);
        areas.Add(CIRCLE, computeAreaCircle(circleRadius));

        double largestAreaTemp = 0;
        string largestAreaName = "";
        foreach (KeyValuePair<string,double> entry in areas)
        {
            if (entry.Value > largestAreaTemp)
            {
                largestAreaTemp = entry.Value;
                largestAreaName = entry.Key;
            }
        }

        //apply process
        SquareArea.text = "" + areas[SQUARE];
        TriangleArea.text = "" + areas[TRIANGLE];
        CircleArea.text = "" + areas[CIRCLE];
        LargestAreaName.text = largestAreaName + ":";
        LargestAreaValue.text = "" + largestAreaTemp;
    }

    private double computeAreaSquare(double squareSide)
    {
        return squareSide * squareSide;
    }

    private double computeAreaTriangle(double triangleBase, double triangleHeight)
    {
        return (triangleBase * triangleHeight) / 2;
    }

    private double computeAreaCircle(double circleRadius)
    {
        return 3.14 * (circleRadius * circleRadius); 
    }
	
}
