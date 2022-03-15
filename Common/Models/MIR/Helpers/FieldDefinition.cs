using Common.Utils;

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
        public bool HasCodeId => !CodeId.IsNullOrEmpty();
        public string Delimitator { get; set; }
        public bool IsDelimitatorDriven => !Delimitator.IsNullOrEmpty();
        public bool IsOptionalContained { get; set; }
        public bool IsComplexField => IsDelimitatorDriven || IsOptionalContained;
        public string NestedDelimitator { get; set; }
        public bool IsNestedDelimitatorDriven => !NestedDelimitator.IsNullOrEmpty();
        public int CropIndex { get; set; }
    }
}
