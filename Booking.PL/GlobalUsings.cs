global using Booking.BLL.Enums;
global using Booking.BLL.Interfaces;
global using Booking.BLL.IService;
global using Booking.BLL.Repositories;
global using Booking.BLL.Service;

global using Booking.DAL.Entities;
global using Booking.DAL.Validators;
global using Booking.DAL.ConfigModels;
global using Booking.DAL.Data;

global using Booking.PL.MappingProfiles;
global using Booking.PL.CustomizeResponses;
global using Booking.PL.DTO.Account;
global using Booking.PL.DTO.City;
global using Booking.PL.DTO.Residence;
global using Booking.PL.DataSeeding;
global using Booking.PL.ServiceConfiguration;

global using System.Text;
global using System.Net;
global using System.ComponentModel.DataAnnotations;

global using AutoMapper;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;

global using Microsoft.EntityFrameworkCore;