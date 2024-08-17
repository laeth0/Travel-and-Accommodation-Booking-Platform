

using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;
using Booking.Infrastrature.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Booking.Infrastrature.Persistence;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private bool _disposed = false;  // To detect redundant calls
    private readonly ApplicationDbContext _DbContext;
    private readonly Lazy<IAmenityRepository> _amenityRepository;
    private readonly Lazy<ICityRepository> _cityRepository;
    private readonly Lazy<ICountryRepository> _countryRepository;
    private readonly Lazy<IDiscountRepository> _discountRepository;
    private readonly Lazy<IResidenceOwnerRepository> _residenceOwnerRepository;
    private readonly Lazy<IResidenceRepository> _residenceRepository;
    private readonly Lazy<IResidenceTypeRepository> _residenceTypeRepository;
    private readonly Lazy<IRoomAmenityRepository> _roomAmenityRepository;
    private readonly Lazy<IRoomBookingRepository> _roomBookingRepository;
    private readonly Lazy<IRoomRepository> _roomRepository;
    private readonly Lazy<IRoomTypeRepository> _roomTypeRepository;


    public IAmenityRepository AmenityRepository => _amenityRepository.Value;
    public ICityRepository CityRepository => _cityRepository.Value;
    public ICountryRepository CountryRepository => _countryRepository.Value;
    public IDiscountRepository DiscountRepository => _discountRepository.Value;
    public IResidenceOwnerRepository ResidenceOwnerRepository => _residenceOwnerRepository.Value;
    public IResidenceRepository ResidenceRepository => _residenceRepository.Value;
    public IResidenceTypeRepository ResidenceTypeRepository => _residenceTypeRepository.Value;
    public IRoomAmenityRepository RoomAmenityRepository => _roomAmenityRepository.Value;
    public IRoomBookingRepository RoomBookingRepository => _roomBookingRepository.Value;
    public IRoomRepository RoomRepository => _roomRepository.Value;
    public IRoomTypeRepository RoomTypeRepository => _roomTypeRepository.Value;


    public UnitOfWork(ApplicationDbContext context)
    {
        _DbContext = context;
        _amenityRepository = new Lazy<IAmenityRepository>(() => new AmenityRepository(_DbContext));
        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_DbContext));
        _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(_DbContext));
        _discountRepository = new Lazy<IDiscountRepository>(() => new DiscountRepository(_DbContext));
        _residenceOwnerRepository = new Lazy<IResidenceOwnerRepository>(() => new ResidenceOwnerRepository(_DbContext));
        _residenceRepository = new Lazy<IResidenceRepository>(() => new ResidenceRepository(_DbContext));
        _residenceTypeRepository = new Lazy<IResidenceTypeRepository>(() => new ResidenceTypeRepository(_DbContext));
        _roomAmenityRepository = new Lazy<IRoomAmenityRepository>(() => new RoomAmenityRepository(_DbContext));
        _roomBookingRepository = new Lazy<IRoomBookingRepository>(() => new RoomBookingRepository(_DbContext));
        _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(_DbContext));
        _roomTypeRepository = new Lazy<IRoomTypeRepository>(() => new RoomTypeRepository(_DbContext));
    }



    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        //It will Begin the transaction on the underlying connection
        return await _DbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
    }


    //If all the Transactions are completed successfully then we need to call this Commit() 
    //method to Save the changes permanently in the database
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        //Commits the underlying store transaction
        if (_DbContext.Database.CurrentTransaction is null) return;

        await _DbContext.Database.CommitTransactionAsync(cancellationToken);
    }


    //If at least one of the Transaction is Failed then we need to call this Rollback() 
    //method to Rollback the database changes to its previous state
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_DbContext.Database.CurrentTransaction is null) return;

        //Rolls back the underlying store transaction
        await _DbContext.Database.RollbackTransactionAsync(cancellationToken);

        //_DbContext.Database.CurrentTransaction.Dispose(); //The Dispose method is already called on the transaction object when you call RollbackTransactionAsync, so you don't need to call it again.
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _DbContext.ChangeTracker.DetectChanges();

        foreach (var entry in _DbContext.ChangeTracker.Entries<IAuditableEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedAtUtc = DateTime.UtcNow;
                    break;
            }

        return await _DbContext.SaveChangesAsync(cancellationToken);
    }


    // disposing : true (dispose managed + unmanaged)      
    // disposing : false (dispose unmanaged + large fields)
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        // Dispose Logic
        if (disposing)
        {
            // Dispose managed resources
            _DbContext?.Dispose();
            // Dispose other managed resources (like repositories) if needed
        }

        // Free any unmanaged resources here if needed
        // set large fields to null
        _disposed = true;  // Mark disposal complete
    }


    public void Dispose()
    {
        Dispose(true);  // Dispose both managed and unmanaged resources
        GC.SuppressFinalize(this);  // Prevent the finalizer from running
    }


    // Override the finalizer to clean up unmanaged resources in case Dispose is not called manually
    ~UnitOfWork()
    {
        Dispose(false);  // Only dispose unmanaged resources in finalizer
    }

   
}

