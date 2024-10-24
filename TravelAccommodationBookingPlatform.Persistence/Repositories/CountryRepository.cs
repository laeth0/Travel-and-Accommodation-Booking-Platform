﻿using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class CountryRepository : Repository<Country>, ICountryRepository
{
    private bool _disposed = false;
    public CountryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }


    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _dbContext?.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~CountryRepository()
    {
        Dispose(false);
    }
}
