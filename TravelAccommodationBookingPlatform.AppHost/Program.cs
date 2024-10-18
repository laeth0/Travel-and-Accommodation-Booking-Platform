var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TravelAccommodationBookingPlatform_Api>("travelaccommodationbookingplatform");

builder.Build().Run();
