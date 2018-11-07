
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Aerial
{
    public static class FormsHelpers
    {
        public static void DataBindEnum<T>(this ComboBox combobox, T defaultValue)
        {
            var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new
                {
                    Value = x,
                    Description =
                        (Attribute.GetCustomAttribute(x.GetType().GetField(x.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description
                        ?? x.ToString()
                })
                .OrderBy(x => x.Value)
                .ToList();
            combobox.DataSource = list;
            combobox.DisplayMember = "Description";
            combobox.ValueMember = "Value";
            combobox.SelectedValue = defaultValue;
        }
    }
}
