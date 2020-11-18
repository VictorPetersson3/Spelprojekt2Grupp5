using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_flexibleGridLayout : LayoutGroup
{
    public enum eFitType
    {
        eUniform,
        eWidth,
        eHeight,
        eFixedRows,
        eFixedColumns
    }

    public eFitType fitType;

    public int myRows;
    public int myColumns;
    public Vector2 myCellSize;
    public Vector2 mySpacing;

    public bool myFitX;
    public bool myFitY;

    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == eFitType.eWidth || fitType == eFitType.eHeight || fitType == eFitType.eUniform)
        {
            myFitX = true;
            myFitY = true;
            float sqrRt = Mathf.Sqrt(transform.childCount);
            myRows = Mathf.CeilToInt(sqrRt);
            myColumns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == eFitType.eWidth || fitType == eFitType.eFixedColumns || fitType == eFitType.eUniform)
        {
            myRows = Mathf.CeilToInt(transform.childCount / (float)myColumns);
        }
        if (fitType == eFitType.eHeight || fitType == eFitType.eFixedRows || fitType == eFitType.eUniform)
        {
            myColumns = Mathf.CeilToInt(transform.childCount / (float)myRows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)myColumns) - ((mySpacing.x / (float)myColumns) * (myColumns - 1)) - (padding.left / (float)myColumns) - (padding.right / (float)myColumns);
        float cellHeight = (parentHeight / (float)myRows) - ((mySpacing.y / (float)myRows) * (myRows - 1)) - (padding.top / (float)myRows) - (padding.bottom / (float)myRows);

        myCellSize.x = myFitX ? cellWidth : myCellSize.x;
        myCellSize.y = myFitY ? cellHeight : myCellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / myColumns;
            columnCount = i % myColumns;

            var item = rectChildren[i];

            var xPos = (myCellSize.x * columnCount) + (mySpacing.x * columnCount) + padding.left;
            var yPos = (myCellSize.y * rowCount) + (mySpacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, myCellSize.x);
            SetChildAlongAxis(item, 1, yPos, myCellSize.y);
        }

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
}