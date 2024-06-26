﻿using PortfolioManager.Base.Enums;

namespace PortfolioManager.Base.Entities;

public class ErrorLogEntity : BaseEntity
{
    public int? UserId { get; set; }

    public string UserIpAdress { get; set; }

    public ErrorTypes ErrorType { get; set; }

    public string ErrorMessage { get; set; }
}
