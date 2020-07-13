using System;
using System.Collections.Generic;

namespace TimHanewich.DataSetManagement
{
    public static class DataSetDataTransformationHelper
            {

                public static string[] ListUniqueValues(this DataSet ds, string attribute_name)
                {
                    List<string> UniqueValues = new List<string>();

                    foreach (DataRecord dr in ds.Records)
                    {
                        foreach (DataAttribute da in dr.Attributes)
                        {
                            if (da.Name == attribute_name)
                            {
                                if (UniqueValues.Contains(da.Value) == false)
                                {
                                    UniqueValues.Add(da.Value);
                                }
                            }
                        }
                    }

                    return UniqueValues.ToArray();
                }

                public static void TransformContinuousToDiscrete(this DataSet ds, string attribute_name)
                {
                    string[] UniqueValues = ds.ListUniqueValues(attribute_name);



                    foreach (DataRecord dr in ds.Records)
                    {
                        DataAttribute ToDelete = null;
                        List<DataAttribute> ToAdd = new List<DataAttribute>();
                        ToAdd.Clear();


                        foreach (DataAttribute da in dr.Attributes)
                        {
                            if (da.Name == attribute_name)
                            {
                                foreach (string s in UniqueValues)
                                {
                                    DataAttribute nda = new DataAttribute();
                                    nda.Name = attribute_name + "_is_" + s;
                                    if (da.Value == s)
                                    {
                                        nda.Value = "1";
                                    }
                                    else
                                    {
                                        nda.Value = "0";
                                    }
                                    ToAdd.Add(nda);
                                }
                                ToDelete = da;
                            }
                        }

                        foreach (DataAttribute da in ToAdd)
                        {
                            dr.Attributes.Add(da);
                        }

                        if (ToDelete != null)
                        {
                            dr.Attributes.Remove(ToDelete);
                        }

                    }

                }
            }
}