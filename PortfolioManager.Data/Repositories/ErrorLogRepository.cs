﻿using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class ErrorLogRepository(ApplicationDbContext applicationDbContext, IUserContext userContext)
    : BaseRepository<ErrorLogEntity>(applicationDbContext, userContext), IErrorLogRepository
{
}
