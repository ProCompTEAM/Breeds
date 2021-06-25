using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Attributes
{
    public class UnicodeAttribute : ColumnAttribute
    {
        public UnicodeAttribute(int length)
        {
            TypeName = $"nvarchar({length})";
        }
    }
}
