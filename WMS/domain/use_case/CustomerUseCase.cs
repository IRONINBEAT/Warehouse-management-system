using System;
using System.Linq;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.repository;

namespace WMS.domain.use_case;

public class CustomerUseCase : ICustomer
{
    private CustomerRepository _customerRepository;

    public CustomerUseCase(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public FillingCustomerInfoErrors Add(Customer customer)
    {

        var result = ValidateCustomerInfo(customer);
        if (result == FillingCustomerInfoErrors.SUCCEED)
            _customerRepository.Add(customer);
        return result;
    }
    
    private bool IsPhoneNumber(string input, string pattern)
    {
        if (input.Length != pattern.Length) return false;

        for( int i = 0; i < input.Length; ++i ) {
            bool ith_character_ok;
            if (Char.IsDigit(pattern, i))
                ith_character_ok = Char.IsDigit(input, i);
            else
                ith_character_ok = (input[i] == pattern[i]);

            if (!ith_character_ok) return false;
        }
        return true;
    }
    
    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) {
            return false; // suggested by @TK-421
        }
        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch {
            return false;
        }
    }

    public FillingCustomerInfoErrors ValidateCustomerInfo(Customer customer)
    {
        if (string.IsNullOrEmpty(customer.Name)) return FillingCustomerInfoErrors.NO_NAME;
        if (!IsPhoneNumber(customer.PhoneNumber, "0-000-000-00-00")) return FillingCustomerInfoErrors.INCORRECT_PHONE_NUMBER;
        if (string.IsNullOrEmpty(customer.PhoneNumber)) return FillingCustomerInfoErrors.NO_PHONE_NUMBER;
        if (!IsValidEmail(customer.Email)) return FillingCustomerInfoErrors.INCORRECT_EMAIL;
        if (string.IsNullOrEmpty(customer.Email)) return FillingCustomerInfoErrors.NO_EMAIL;
        
        if (string.IsNullOrEmpty(customer.Country)) return FillingCustomerInfoErrors.NO_COUNTRY;
        if (string.IsNullOrEmpty(customer.City)) return FillingCustomerInfoErrors.NO_CITY;
        if (string.IsNullOrEmpty(customer.Street)) return FillingCustomerInfoErrors.NO_STREET;
        if (string.IsNullOrEmpty(customer.HouseNumber)) return FillingCustomerInfoErrors.NO_HOUSE_NUMBER;
        if (string.IsNullOrEmpty(customer.PostalCode)) return FillingCustomerInfoErrors.NO_POSTAL_CODE;

        if (!customer.PostalCode.All(char.IsDigit)) return FillingCustomerInfoErrors.INCORRECT_POSTAL_CODE;

        return FillingCustomerInfoErrors.SUCCEED;
    }
}