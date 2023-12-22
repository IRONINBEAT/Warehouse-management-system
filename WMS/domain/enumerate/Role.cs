using System.ComponentModel;

namespace WMS.domain.enumerate;

public enum Role
{
    [Description("Директор")]
    MANAGER,
    [Description("Старший кладовщик")]
    SENIORSTOREKEEPER,
    [Description("Младший кладовщик")]
    JUNIORSTOREKEEPER,
    [Description("Админ")]
    ADMIN
}