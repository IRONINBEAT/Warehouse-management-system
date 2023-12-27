using System.ComponentModel;

namespace WMS.domain.enumerate;

public enum ProductType
{
    [Description("Техника")]
    Devices,
    [Description("Одежда")]
    Clothes,
    [Description("Обувь")]
    Shoes,
    [Description("Товары для дома")]
    HouseholdGoods,
    [Description("Строительные материалы")]
    BuildingMaterials,
    [Description("Спортивный инвентарь")]
    SportsEquipment,
    [Description("Искусство и хобби")]
    ArtHobbies,
    [Description("Инструменты")]
    Tools,
    [Description("Канцтовары")]
    OfficeSupplies,
    [Description("Книги и образование")]
    BooksEducation,
    [Description("Товары для кухни")]
    KitchenSupplies,
    [Description("Мебель")]
    Furniture,
    [Description("Тара")]
    Container
}