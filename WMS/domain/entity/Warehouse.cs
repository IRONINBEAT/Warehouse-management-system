using System.Collections.Generic;
using WMS.domain.structure;

namespace WMS.domain.entity;

public class Warehouse
{
    public void ChangeResponsibilityPerson(User newRespPerson)
    {
        this._responsibilityPerson = newRespPerson;
    }
    
    public Address address;
    public List<User> users;
    private User _responsibilityPerson;
    public double capacity;
}