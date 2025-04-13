using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Helpers
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ExportableToExcelAttribute : Attribute
    {
        public string HeaderName { get; set; }

        public int ColumnOrder { get; set; }

        public ExportableToExcelAttribute(string headerName, int columnOrder)
        {
            HeaderName = headerName;
            ColumnOrder = columnOrder;
        }
    }
}
