using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CsvParser
{

    public string csvPath;
    private string [][] csvArray;

    //传入CSV文件的绝对路径
    public CsvParser(string Path)
    {
        csvPath = Path;
    }

    //载入CSV文件，存入数组csvArray
    public void Load()
    { 
        ArrayList csvFile = ResourcesOperator.LoadTextStream(csvPath);
        string[] lineArray = new string[csvFile.Count];
        csvArray = new string[csvFile.Count][];

        int i = 0;
        foreach (string s in csvFile)
        {
            csvArray[i] = s.Split(',');
            i++;
        }
    }

    //以行编号和列编号为条件，取出字段Field
    string GetDataByRowAndCol(int nRow, int nCol)
    {
        if (csvArray.Length <= 0 || nRow >= csvArray.Length)
            return "";
        if (nCol >= csvArray[0].Length)
            return "";

        return csvArray[nRow][nCol];
    }

    //以行编号和列名为条件，取出字段Field
    public string GetDataByIdAndName(int nId, string strName)
    {
        if (csvArray.Length <= 0) return "";

        int nRow = csvArray.Length;
        int nCol = csvArray[0].Length;
        for (int i = 1; i < nRow; ++i)
        {
            string strId = string.Format("{0}", nId);
            if (csvArray[i][0] == strId)
            {
                for (int j = 0; j < nCol; ++j)
                {
                    if (csvArray[0][j] == strName)
                    {
                        return csvArray[i][j];
                    }
                }
            }
        }

        return "";
    }

    //以行号和列名，取出字段Field
    public string GetField(int rowId, string strName)
    {
        if (csvArray.Length <= 0) return "";
        
        int nCol = csvArray[0].Length;
        int getColId = -1;

        for (int i = 0; i < nCol; ++i)
        {
            if (csvArray[0][i] == strName) getColId = i;
        }
        
        if (getColId == -1) return "";

        return csvArray[rowId][getColId];

    }

    //以列名和值为条件，取出一个字段Field
    public string GetDataByNameAndValue(string colName, string value, string strName)
    {
        if (csvArray.Length <= 0) return "";

        int nRow = csvArray.Length;
        int nCol = csvArray[0].Length;
        int getColId = -1;

        for (int i = 0; i < nCol; ++i)
        {
            if (csvArray[0][i] == strName) getColId = i;
        }
        

        if (getColId == -1) return "";

        for (int i = 1; i < nCol; ++i)
        {
            if (csvArray[0][i] == colName)
            {
                for (int j = 1; j < nRow; ++j)
                {
                    if (csvArray[j][i] == value)
                    {
                        return csvArray[j][getColId];
                    }
                    
                }
            }
        }
        return "";
    }

    //以列名和值为条件，取出一个数据集
    public string[][] GetRowsByNameAndValue(string colName, string value)
    {
        ArrayList rowsList = new ArrayList();
        int nRow = csvArray.Length;
        int nCol = csvArray[0].Length;
        int queryColId = -1;

        for (int i = 0; i < nCol; ++i)
        {
            if (csvArray[0][i] == colName) queryColId = i;
        }

        if (queryColId == -1) throw new ArgumentOutOfRangeException("列名不存在"); ;

        for(int i = 1; i < nRow; ++i)
        {
            if(csvArray[i][queryColId] == value)
            {
                rowsList.Add(csvArray[i]);
            }
        }

        string[][] rowsArray = new string[rowsList.Count][];

        int j = 0;
        foreach (string[] s in rowsList)
        {
            rowsArray[j] = s;
            j++;
        }
        return rowsArray;
    }

    //以两个列名和值为条件，取出一个数据
}
