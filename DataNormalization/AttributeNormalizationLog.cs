using System;

namespace TimHanewich.DataSetManagement.DataNormalization
{
    public class AttributeNormalizationLog
    {
        public string AttributeName { get; set; }
        public float RawMinimumValue { get; set; }
        public float RawMaximumValue { get; set; }
        public float AppliedFloor { get; set; }
        public float AppliedCeiling { get; set; }
    }
}