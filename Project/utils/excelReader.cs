using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

public class ExcelReader
{
    public List<Dictionary<string, string>> GetData(string excelFilePath, string sheetName)
    {
        ISheet sheet = GetSheetByName(excelFilePath, sheetName);
        return ReadSheet(sheet);
    }

    public List<Dictionary<string, string>> GetData(string excelFilePath, int sheetNumber)
    {
        ISheet sheet = GetSheetByIndex(excelFilePath, sheetNumber);
        return ReadSheet(sheet);
    }

    private ISheet GetSheetByName(string excelFilePath, string sheetName)
    {
        IWorkbook workbook = WorkbookFactory.Create(excelFilePath);
        return workbook.GetSheet(sheetName);
    }

    private ISheet GetSheetByIndex(string excelFilePath, int sheetNumber)
    {
        IWorkbook workbook = WorkbookFactory.Create(excelFilePath);
        return workbook.GetSheetAt(sheetNumber);
    }

    private List<Dictionary<string, string>> ReadSheet(ISheet sheet)
    {
        List<Dictionary<string, string>> excelRows = new List<Dictionary<string, string>>();
        int headerRowNumber = GetHeaderRowNumber(sheet);
        if (headerRowNumber != -1)
        {
            IRow headerRow = sheet.GetRow(headerRowNumber);
            int totalColumn = headerRow.LastCellNum;

            for (int rowIndex = headerRowNumber + 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                IRow row = sheet.GetRow(rowIndex);
                if (row == null) continue;

                Dictionary<string, string> columnMapData = new Dictionary<string, string>();
                for (int columnIndex = 0; columnIndex < totalColumn; columnIndex++)
                {
                    ICell cell = row.GetCell(columnIndex, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    string header = headerRow.GetCell(columnIndex).StringCellValue;
                    string value = GetCellValue(cell);
                    columnMapData[header] = value;
                }
                excelRows.Add(columnMapData);
            }
        }
        return excelRows;
    }

    private int GetHeaderRowNumber(ISheet sheet)
    {
        for (int rowIndex = sheet.FirstRowNum; rowIndex <= sheet.LastRowNum; rowIndex++)
        {
            IRow row = sheet.GetRow(rowIndex);
            if (row != null)
            {
                for (int columnIndex = 0; columnIndex < row.LastCellNum; columnIndex++)
                {
                    ICell cell = row.GetCell(columnIndex, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    if (cell.CellType == CellType.String || cell.CellType == CellType.Numeric || cell.CellType == CellType.Boolean || cell.CellType == CellType.Error)
                    {
                        return rowIndex;
                    }
                }
            }
        }
        return -1;
    }

    private string GetCellValue(ICell cell)
    {
        return cell.CellType switch
        {
            CellType.String => cell.StringCellValue,
            CellType.Numeric => cell.NumericCellValue.ToString(),
            CellType.Boolean => cell.BooleanCellValue.ToString(),
            CellType.Formula => cell.CellFormula,
            CellType.Blank => "",
            CellType.Error => $"Error:{cell.ErrorCellValue}",
            _ => "Unknown Cell Type",
        };
    }
}
