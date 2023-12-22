using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using WMS.domain.entity;
using WMS.domain.repository;
using ZXing;
using ZXing.Presentation;

namespace WMS.domain.use_case;

public class QRCodeUseCase : IQrCode
{
    private string filePath = @"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\qr_codes\";
    public void Generate(Product product)
    {
        BarcodeWriter barcodeWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions
            {
                Width = 300,
                Height = 300
            }
        };
        barcodeWriter.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");

        string codingInfo = $"{char.ToUpper(product.Name[0])}-{product.Id}";
            
        BitmapSource barcodeBitmap = barcodeWriter.Write($"Товар на полке: {codingInfo}");
            
        SaveAsPng(barcodeBitmap, $"{filePath}{codingInfo}.png");
    }

    public BitmapImage Load(Product product)
    {
        BitmapImage bitmapImage = new BitmapImage(new Uri($"{filePath}{char.ToUpper(product.Name[0])}-{product.Id}.png"));
        return bitmapImage;
    }


    private static void SaveAsPng(BitmapSource bitmapSource, string filePath)
    {
        // Конвертация BitmapSource в Bitmap
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

        // Сохранение в файл
        using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        {
            encoder.Save(fileStream);
        }
    }
}