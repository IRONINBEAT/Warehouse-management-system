using System.ComponentModel;

namespace WMS.domain.enumerate;

public enum ProductType
{
    [Description("Электроника")]
    ELECTRONICS,
    [Description("Одежда")]
    CLOTHES,
    [Description("Обувь")]
    SHOES,
    [Description("Товары для дома")]
    HOUSEHOLD_GOODS,
    [Description("Строительные материалы")]
    BUILDING_MATERIALS,
    [Description("Спортивный инвентарь")]
    SPORTS_EQUIPMENT,
    [Description("Искусство и хобби")]
    ART_HOBBIES,
    [Description("Инструменты")]
    TOOLS,
    [Description("Канцтовары")]
    OFFICE_SUPPLIES,
    [Description("Книги и образование")]
    BOOKS_EDUCATION,
    [Description("Товары для кухни")]
    KITCHEN_SUPPLIES
}