using System.ComponentModel;

namespace WMS.domain.enumerate;

public enum ProductStatus
{
    [Description("На складе")]
    IN_STOCK,
    [Description("На пути к клиенту")]
    ON_THE_WAY_TO_CLIENT
}