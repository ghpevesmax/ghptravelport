using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class FieldDefinition
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int SegmentPosition { get; set; }
        public FieldDefinition[] NestedFields { get; set; }
        public bool HasNestedFields => NestedFields != null && NestedFields.Length > 0;
        public bool IsVariableLength { get; set; }
        /// <summary>
        /// Defines whether or not this field can be present or not.
        /// </summary>
        public bool IsOptionalField { get; set; }
        public string CodeId { get; set; }
        public bool HasCodeId => !string.IsNullOrEmpty(CodeId);
        public string Delimitator { get; set; }
        public bool IsDelimitatorDriven => !string.IsNullOrEmpty(Delimitator);
        public bool IsOptionalContained { get; set; }
        public bool IsComplexField => IsDelimitatorDriven || IsOptionalContained;
    }
}
