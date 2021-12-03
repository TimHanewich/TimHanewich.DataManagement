using System;
using System.Collections.Generic;

namespace TimHanewich.DataSetManagement
{
    public class DataRecord
            {
                public List<DataAttribute> Attributes { get; set; }

                public DataAttribute GetDataAttribute(string AttributeName)
                {
                    DataAttribute ToReturn = null;
                    foreach (DataAttribute da in Attributes)
                    {
                        if (da.Name == AttributeName)
                        {
                            ToReturn = da;
                        }
                    }

                    if (ToReturn == null)
                    {
                        throw new Exception("Unable to find attribute with name '" + AttributeName + "'.");
                    }
                    return ToReturn;
                }

                public DataRecord()
                {
                    Attributes = new List<DataAttribute>();
                }

                /// <summary>
                /// Add a new attribute to this data record.
                /// </summary>
                /// <param name="attribute_name"></param>
                /// <param name="attribute_value"></param>
                /// <param name="apply_normalization">Provide the parent Data Set that contains the data normalizaiton log that will use.</param>
                public void AddAttribute(string attribute_name, string attribute_value)
                {
                    DataAttribute da = new DataAttribute();
                    da.Name = attribute_name;
                    da.Value = attribute_value;

                    Attributes.Add(da);
                }
            }
}