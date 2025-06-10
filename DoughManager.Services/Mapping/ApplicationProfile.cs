// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Mapping.ApplicationProfile
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using AutoMapper;
using DoughManager.Services.Dtos;
using DoughManager.Services.Dtos.Auth;
using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Dtos.Production;
using DoughManager.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DoughManager.Services.Dtos.Discrepancy;
using DoughManager.Services.Dtos.RawMaterial;
using DoughManager.Services.Dtos.StockTake;

#nullable enable
namespace DoughManager.Services.Mapping;

public class ApplicationProfile : Profile
{
  public ApplicationProfile()
  {
        CreateMap<CreateAccountRequest, Account>().ForMember((Expression<Func<Account, string>>) (dest => dest.PasswordHash),  opt => opt.MapFrom((Expression<Func<CreateAccountRequest, string>>) (src => src.Password)));
        CreateMap<DoughManager.Data.EntityModels.Product, CreateProductRequest>();
        CreateMap<ProductBatch, ProductBatchDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
            .ForMember(dest => dest.ProductionProcesses,
                opt => opt.MapFrom<ProductProcessesResolver>());
            
        CreateMap<ProductBatchDto, ProductBatch>()
            .ForMember(dest => dest.ProductionProcesses,
                opt => opt.MapFrom<ProductProcessesSerializer>());
        CreateMap<CreateProductRequest, DoughManager.Data.EntityModels.Product>();
        CreateMap<ProductRawMaterial, CreateProductRawMaterialRequest>();
        CreateMap<CreateProductRawMaterialRequest, ProductRawMaterial>();
        CreateMap<RawMaterial, CreateRawMaterialRequest>();
        CreateMap<CreateRawMaterialRequest, RawMaterial>();
        CreateMap<DiscrepancyRecord, DiscrepancyRecordDto>().ReverseMap();
        CreateMap<Order, OrderDto>();
        CreateMap<StockTake, StockTakeDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Product.SellingPrice))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl));
        CreateMap<OrderDto, Order>();
        CreateMap<ProductionBatchRawMaterialDto, ProductBatchRawMaterial>();
        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        CreateMap<RawMaterialInventory, InventoryLog>()
            .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => src.Creator.FullName))
            .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => src.CreationTime))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.RawMaterial.ImageUrl))
            .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(src => src.RawMaterial.UnitOfMeasure))
            .ForMember(dest => dest.RawMaterialName, opt => opt.MapFrom(src => src.RawMaterial.Name));
        CreateMap<ProductionBatch, ProductionBatchDto>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.ProductOrders.Select(o => o.Id)))
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator.FullName))
            .ForMember(dest => dest.ProductionProcesses,
                opt => opt.MapFrom<ProductionProcessesResolver>());

            

        
        CreateMap<ProductionBatchDto, ProductionBatch>()
            .ForMember(dest => dest.ProductionProcesses,
                opt => opt.MapFrom<ProductionProcessesSerializer>());
        CreateMap<ProductBatchRawMaterial, ProductionBatchRawMaterialDto>()
            .ForMember(dest => dest.RawMaterialName, opt => opt.MapFrom(src => src.RawMaterial.Name))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.RawMaterial.ImageUrl));
        CreateMap<ProductOrdersDto, ProductOrder>();
        CreateMap<ProductOrder, ProductOrdersDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl));
        CreateMap<Dispatch, DispatchDto>();
        CreateMap<DispatchDto, Dispatch>();
        CreateMap<DispatchItem, DispatchItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl));
        CreateMap<DispatchItemDto, DispatchItem>();
  }
}
