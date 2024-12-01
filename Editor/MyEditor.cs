using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;

public class MyEditor : MonoBehaviour
{
    [MenuItem("我的工具/excel转成txt")]

    public static void ExportExcelToTxt()
    {
        string assetPath = Application.dataPath + "/_Excel";
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace("\\", "/");
            using(FileStream fs = File.Open(files[i],FileMode.Open,FileAccess.Read)) 
            {
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                DataSet dataSet = excelDataReader.AsDataSet();
                DataTable table = dataSet.Tables[0];
                readTableToTxt(files[i], table);
            }
        }
        AssetDatabase.Refresh();
    }

    private static void readTableToTxt(string filePath,DataTable table)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using(FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                for(int row =0;row<table.Rows.Count;row++) 
                { 
                    DataRow dataRow = table.Rows[row];
                    string str = "";
                    for(int col = 0; col < table.Columns.Count;col ++)
                    {
                        string val = dataRow[col].ToString();
                        str = str + val + "\t";
                    }
                    sw.Write(str);
                    if(row!=table.Rows.Count-1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
