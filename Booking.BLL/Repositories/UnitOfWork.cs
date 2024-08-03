

using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Booking.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _DbContext;

        //The following varibale will hold the Transaction Instance
        private IDbContextTransaction? _objTran = null;

        private readonly Lazy<ICityRepository> _cityRepository;
        private readonly Lazy<IFlightRepository> _flightRepository;
        private readonly Lazy<IResidenceRepository> _residenceRepository;
        private readonly Lazy<IReviewRepository> _reviewRepository;
        private readonly Lazy<IRoomRepository> _roomRepository;

        public ICityRepository CityRepository => _cityRepository.Value;
        public IFlightRepository FlightRepository => _flightRepository.Value;
        public IResidenceRepository ResidenceRepository => _residenceRepository.Value;
        public IReviewRepository ReviewRepository => _reviewRepository.Value;
        public IRoomRepository RoomRepository => _roomRepository.Value;

        public UnitOfWork(ApplicationDbContext context)
        {
            _DbContext = context;
            _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_DbContext));
            _flightRepository = new Lazy<IFlightRepository>(() => new FlightRepository(_DbContext));
            _residenceRepository = new Lazy<IResidenceRepository>(() => new ResidenceRepository(_DbContext));
            _reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(_DbContext));
            _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(_DbContext));
        }


        //The CreateTransaction() method will create a database Transaction so that we can do database operations
        //by applying do everything and do nothing principle
        public async Task BeginTransactionAsync()
        {
            //It will Begin the transaction on the underlying connection
            _objTran = await _DbContext.Database.BeginTransactionAsync();
        }

        //If all the Transactions are completed successfully then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public async Task CommitAsync()
        {
            //Commits the underlying store transaction
            await _objTran?.CommitAsync();
        }

        //If at least one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public async Task RollbackAsync()
        {
            //Rolls back the underlying store transaction
            await _objTran?.RollbackAsync();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            _objTran?.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync() => await _DbContext.SaveChangesAsync();


        public void Dispose()
        {
            _objTran?.Dispose();
            _DbContext.Dispose();
        }

    }
}
