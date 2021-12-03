using System;

namespace TimHanewich.DataSetManagement.DataNormalization
{
    public static class DataNormalizationHelper
    {
        public static AttributeNormalizationLog NormalizeAttribute(this DataSet ds, string attribute_name, float floor = -1f, float ceiling = 1f)
        {
            AttributeNormalizationLog ToReturn = new AttributeNormalizationLog();
            ToReturn.AttributeName = attribute_name;

            //Get the raw data min and max
            float RawMin = float.MaxValue;
            float RawMax = float.MinValue;
            foreach (DataRecord dr in ds.Records)
            {
                DataAttribute da = null;
                try
                {
                    da = dr.GetDataAttribute(attribute_name);
                }
                catch
                {

                }

                if (da != null)
                {
                    if (da.Value != null)
                    {
                        float val;
                        try
                        {
                            val = Convert.ToSingle(da.Value);
                        }
                        catch
                        {
                            throw new Exception("Unable to convert value '" + da.Value + "' of attribute '" + attribute_name + "' to a float value.");
                        }
                        RawMin = Math.Min(RawMin, val);
                        RawMax = Math.Max(RawMax, val);
                    }
                }
            }
            if (RawMin == float.MaxValue && RawMax == float.MinValue)
            {
                throw new Exception("Unable to find max and min value for attribute type '" + attribute_name + "'.");
            }
            ToReturn.RawMaximumValue = RawMax;
            ToReturn.RawMinimumValue = RawMin;

            //Make the conversions
            foreach (DataRecord dr in ds.Records)
            {
                DataAttribute da = null;
                try
                {
                    da = dr.GetDataAttribute(attribute_name);
                }
                catch
                {

                }

                if (da != null)
                {
                    if (da.Value != null)
                    {
                        float val = 0;
                        bool Continue = true;
                        try
                        {
                            val = Convert.ToSingle(da.Value);
                        }
                        catch
                        {
                            Continue = false;
                        }

                        if (Continue)
                        {
                            //First get as a percentage
                            val = (val - RawMin) / (RawMax - RawMin);

                            //Then apply the asked for scaling
                            val = floor + ((ceiling - floor) * val);

                            //Set the value
                            da.Value = val.ToString();
                        }
                    }
                }
            }
            ToReturn.AppliedFloor = floor;
            ToReturn.AppliedCeiling = ceiling;


            return ToReturn;
        }

        public static float DeNormalize(this float val, AttributeNormalizationLog normalization_log)
        {
            float ToReturn = val;

            //Turn it into a percentage
            ToReturn = (ToReturn - normalization_log.AppliedFloor) / (normalization_log.AppliedCeiling - normalization_log.AppliedFloor);

            //Turn it back into a value
            ToReturn = normalization_log.RawMinimumValue + ((normalization_log.RawMaximumValue - normalization_log.RawMinimumValue) * ToReturn);

            return ToReturn;
        }
    }
}