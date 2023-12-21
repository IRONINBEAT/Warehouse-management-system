using WMS.domain.entity;
using WMS.domain.enumerate;

namespace WMS.domain.repository;

public interface ICustomer
{
    FillingCustomerInfoErrors Add(Customer customer);
}