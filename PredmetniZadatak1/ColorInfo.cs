using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredmetniZadatak1
{
    public class ColorInfo
    {
        public ColorInfo(PropertyInfo info)
        {
            Name = info.Name;
            Color = (Color)info.GetValue(null);
        }

        public string Name { get; }
        public Color Color { get; }

    }
}
