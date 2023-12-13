using WMS.domain.entity;

namespace WMS.domain.repository;

public interface IWarehouse
{
    void ChangeLocation(Warehouse warehouse);
    void ChangeResponsibilityPerson(User newRespPerson);
}