var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TravelAccommodationBookingPlatform>("travelaccommodationbookingplatform");

builder.Build().Run();
