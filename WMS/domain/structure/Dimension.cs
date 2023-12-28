using System;
using NuGet.Common;

namespace WMS.domain.structure;

public struct Dimension
{
    private double _width;
    private double _height;
    private double _length;

    public Dimension(double width, double height, double length)
    {
        Width = width;
        Height = height;
        Length = length;
    }

    public double Width
    {
        get { return _width; }
        set
        {
            if (value <= 0 || double.IsNaN(value))
            {
                throw new Exception("Ширина не может быть неположительной, либо не являться числом");
            }
            _width = value;
        }
    }

    public double Height
    {
        get { return _height; }
        set
        {
            if (value <= 0 || double.IsNaN(value))
            {
                throw new Exception("Высота не может быть неположительной, либо не являться числом");
            }
            _height = value;
        }
    }

    public double Length
    {
        get { return _length; }
        set
        {
            if (value <= 0 || double.IsNaN(value))
            {
                throw new Exception("Длина не может быть неположительной, либо не являться числом");
            }
            _length = value;
        }
    }

    public string ToString => $"({Width}x{Height}x{Length}) м\u00b3";
}
    
