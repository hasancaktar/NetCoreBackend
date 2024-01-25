using Dem.Application.Abstraction;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Dem.Persistance.Services;

public class ReadDataFromExcelService : IReadDataFromExcelService
{
    public Task<Dictionary<string, string>> ReadDataFromExcel(string filePath)
    {
        Dictionary<string, string> datas = new Dictionary<string, string>();

        using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook workbook = new XSSFWorkbook(file);
            ISheet sheet = workbook.GetSheetAt(0); // İlk sayfa

            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    ICell cell1 = row.GetCell(0);
                    ICell cell2 = row.GetCell(1);

                    if (cell1 != null && cell2 != null)
                    {
                        string excelId = cell1.CellType == CellType.Numeric ? cell1.NumericCellValue.ToString() : cell1.StringCellValue;
                        string excelValue = cell2.CellType == CellType.Numeric ? cell2.NumericCellValue.ToString() : cell2.StringCellValue;

                        datas.Add(excelId, excelValue);
                    }
                }
            }
        }

        return Task.FromResult(datas);
    }
}