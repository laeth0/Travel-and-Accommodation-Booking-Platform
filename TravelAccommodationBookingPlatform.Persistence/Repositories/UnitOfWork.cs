using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Persistence.Repositories;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork, IDisposable
{

    private bool _disposed = false;
    private readonly AppDbContext _dbContext;
    private readonly Lazy<IAppUserRepository> _appUserRepository;
    private readonly Lazy<IBookingRepository> _bookingRepository;
    private readonly Lazy<ICityRepository> _cityRepository;
    private readonly Lazy<ICountryRepository> _countryRepository;
    private readonly Lazy<IDiscountRepository> _discountRepository;
    private readonly Lazy<IHotelRepository> _hotelRepository;
    private readonly Lazy<IImageRepository> _imageRepository;
    private readonly Lazy<IPaymentRepository> _paymentRepository;
    private readonly Lazy<IReviewRepository> _reviewRepository;
    private readonly Lazy<IRoomRepository> _roomRepository;
    private readonly Lazy<ITokenRepository> _tokenRepository;

    public IAppUserRepository AppUserRepository => _appUserRepository.Value;
    public IBookingRepository BookingRepository => _bookingRepository.Value;
    public ICityRepository CityRepository => _cityRepository.Value;
    public ICountryRepository CountryRepository => _countryRepository.Value;
    public IDiscountRepository DiscountRepository => _discountRepository.Value;
    public IHotelRepository HotelRepository => _hotelRepository.Value;
    public IImageRepository ImageRepository => _imageRepository.Value;
    public IPaymentRepository PaymentRepository => _paymentRepository.Value;
    public IReviewRepository ReviewRepository => _reviewRepository.Value;
    public IRoomRepository RoomRepository => _roomRepository.Value;
    public ITokenRepository TokenRepository => _tokenRepository.Value;


    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _appUserRepository = new Lazy<IAppUserRepository>(() => new AppUserRepository(_dbContext));
        _bookingRepository = new Lazy<IBookingRepository>(() => new BookingRepository(_dbContext));
        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_dbContext));
        _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(_dbContext));
        _discountRepository = new Lazy<IDiscountRepository>(() => new DiscountRepository(_dbContext));
        _hotelRepository = new Lazy<IHotelRepository>(() => new HotelRepository(_dbContext));
        _imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(_dbContext));
        _paymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(_dbContext));
        _reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(_dbContext));
        _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(_dbContext));
        _tokenRepository = new Lazy<ITokenRepository>(() => new TokenRepository(_dbContext));
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        //It will Begin the transaction on the underlying connection
        return await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
    }



    //If all the Transactions are completed successfully then we need to call this Commit() 
    //method to Save the changes permanently in the database
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        //Commits the underlying store transaction
        if (_dbContext.Database.CurrentTransaction is null) return;

        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }



    //If at least one of the Transaction is Failed then we need to call this Rollback() 
    //method to Rollback the database changes to its previous state
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction is null) return;

        //Rolls back the underlying store transaction
        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);

        //_DbContext.Database.CurrentTransaction.Dispose(); //The Dispose method is already called on the transaction object when you call RollbackTransactionAsync, so you don't need to call it again.
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _dbContext.Database.CreateExecutionStrategy();
    }




    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
         await _dbContext.SaveChangesAsync(cancellationToken);



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
            _dbContext?.Dispose();
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
