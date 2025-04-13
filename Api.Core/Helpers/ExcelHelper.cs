using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Api.Core.Helpers
{
    public static class ExcelHelper
    {
        private const string SheetName = "Sheet 1";

        public static byte[] GetExcelContent<T>(this IList<T> dataSource, string sheetName = SheetName) where T : class
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }

            var columns = GetColumns(dataSource);
            var rowsCount = dataSource.Count;
            var columnsCount = columns.Count;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                for (int i = 1; i <= columnsCount; i++)
                {
                    worksheet.Cell(1, i).Value = columns[i - 1].headerName;
                }

                for (int j = 1; j <= rowsCount; j++)
                {
                    for (int k = 1; k <= columnsCount; k++)
                    {
                        var data = dataSource[j - 1];

                        var propertyName = columns[k - 1].propertyName;

                        worksheet.Cell(1 + j, k).Value = data.GetType().GetProperty(propertyName).GetValue(data, null);
                    }
                }

                var headerRange = worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, columnsCount).Address);
                var fullRange = worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(dataSource.Count + 1, columnsCount).Address);

                headerRange.Style.Fill.SetBackgroundColor(XLColor.Gray);
                fullRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                fullRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                var workbookBytes = new byte[0];

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    workbookBytes = stream.ToArray();
                }

                return workbookBytes;
            }
        }

        private static IDictionary<int, (string propertyName, string headerName)> GetColumns<T>(IList<T> dataSource) where T : class
        {
            var dictionary = new Dictionary<int, (string, string)>();

            var properties = typeof(T).GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(ExportableToExcelAttribute)))
                .OrderBy(x => (x.GetCustomAttributes(typeof(ExportableToExcelAttribute), true).Single() as ExportableToExcelAttribute).ColumnOrder);

            var index = 0;

            foreach (var property in properties)
            {
                var excelAttribute = property.GetCustomAttributes(typeof(ExportableToExcelAttribute), true).Single() as ExportableToExcelAttribute;

                dictionary.Add(index, (property.Name, excelAttribute.HeaderName));

                index++;
            }

            return dictionary;
        }
    }
}
