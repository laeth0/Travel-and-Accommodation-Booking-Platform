﻿using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class ReviewRepository : Repository<Review>, IReviewRepository
{
    private bool _disposed = false;
    public ReviewRepository(AppDbContext dbContext) : base(dbContext)
    { }


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

    ~ReviewRepository()
    {
        Dispose(false);
    }

}
