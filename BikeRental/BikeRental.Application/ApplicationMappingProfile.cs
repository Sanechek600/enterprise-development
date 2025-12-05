using AutoMapper;
using BikeRental.Domain.Models;
using BikeRental.Application.Contracts.BikeModels;
using BikeRental.Application.Contracts.Bikes;
using BikeRental.Application.Contracts.RentalRecords;
using BikeRental.Application.Contracts.Renters;

namespace BikeRental.Application;

/// <summary>
/// Профиль AutoMapper для маппинга сущностей домена и DTO
/// </summary>
public class ApplicationMappingProfile : Profile
{
    /// <summary>
    /// Создаёт профиль маппинга для сущностей и DTO
    /// </summary>
    public ApplicationMappingProfile()
    {
        CreateMap<BikeModel, BikeModelDto>();
        CreateMap<BikeModelCreateUpdateDto, BikeModel>();

        CreateMap<Bike, BikeDto>();
        CreateMap<BikeCreateUpdateDto, Bike>();

        CreateMap<Renter, RenterDto>();
        CreateMap<RenterCreateUpdateDto, Renter>();

        CreateMap<RentalRecord, RentalRecordDto>();
        CreateMap<RentalRecordCreateUpdateDto, RentalRecord>();
    }
}