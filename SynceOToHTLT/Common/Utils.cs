using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynceOToHTLT.Common
{
    public static class Utils
    {
        /// <summary>
        /// Convert a list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static DataTable ToDataTableV2<T>(this IList<T> list, DataTable defaultvalue = null)
        {
            DataTable dt;
            string text = ListToDataTableV2(list, out dt);
            if (text.Length > 0)
            {
                return defaultvalue;
            }

            return dt;
        }

        /// <summary>
        /// Convert a list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ListToDataTableV2<T>(IList<T> list, out DataTable dt)
        {
            dt = null;
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                dt = new DataTable();
                for (int i = 0; i < properties.Count; i++)
                {
                    var column = dt.Columns.Add(properties[i].Name, Nullable.GetUnderlyingType(properties[i].PropertyType) ?? properties[i].PropertyType);
                    if (Nullable.GetUnderlyingType(properties[i].PropertyType) != null)
                        column.AllowDBNull = true;
                }

                object[] array = new object[properties.Count];
                foreach (T item in list)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = properties[j].GetValue(item);
                    }

                    dt.Rows.Add(array);
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
