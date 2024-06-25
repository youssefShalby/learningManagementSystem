﻿

namespace learningManagementSystem.DAL.Models;

[NotMapped]
public class Address
{
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}
