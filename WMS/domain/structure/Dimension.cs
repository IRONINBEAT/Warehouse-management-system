using NuGet.Common;

namespace WMS.domain.structure;

public struct Dimension
{
    public Dimension(double width, double height, double length)
    {
        Width = width;
        Height = height;
        Length = length;
    }
    public double Width { get; set; }
    public double Height{ get; set; }
    public double Length{ get; set; }



    public string ToString => $"({Width}x{Height}x{Length}) м\u00b3";
}