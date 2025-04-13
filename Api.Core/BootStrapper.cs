using AutoMapper;
using Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Helpers;
using Api.Core.Dtos.ErpMilonga;
using Api.Core.Enums;
using Newtonsoft.Json;

namespace Api.Core
{
    public static class BootStrapper
    {
        public static MapperConfiguration MapperConfiguration { get; private set; }

        public static void BootStrap()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Compania, Dtos.Compania>();
                cfg.CreateMap<Dtos.Compania, Compania>();

                cfg.CreateMap<Dtos.Licencia, Licencia>();
                cfg.CreateMap<Licencia, Dtos.Licencia>();

                cfg.CreateMap<Concepto, Dtos.Concepto>() ;
                cfg.CreateMap<Dtos.Concepto, Concepto>();

                cfg.CreateMap<Cliente, Dtos.Cliente>()
                    .ForMember(x => x.Provincia, opt => opt.MapFrom(y => y.Localidad.Provincia))
                    .ForMember(x => x.MetodosEnvio, opt => opt.MapFrom(y => y.GetMetodosEnvio()));
                cfg.CreateMap<Dtos.Cliente, Cliente>();

                cfg.CreateMap<MetodoDeEnvio, Dtos.MetodoDeEnvio>();

                cfg.CreateMap<Licencia, Dtos.Licencia>();

                cfg.CreateMap<Localidad, Dtos.Localidad>();

                cfg.CreateMap<Provincia, Dtos.Provincia>();

                cfg.CreateMap<CondicionPago, Dtos.CondicionPago>();

                cfg.CreateMap<ConceptoCliente, Dtos.ConceptoCliente>().ReverseMap();
                cfg.CreateMap<ConceptoLiquidacion, Dtos.ConceptoLiquidacion>().ReverseMap();

                cfg.CreateMap<Liquidacion, Dtos.Liquidacion>()
                    .ForMember(x => x.Ordenes, opt => opt.MapFrom(y => y.DetalleLiquidacionPre))
                    .ForMember(x => x.Envios, opt => opt.MapFrom(y => y.DetalleLiquidacionPos));
                cfg.CreateMap<Dtos.Liquidacion, Liquidacion>();
                cfg.CreateMap<Liquidacion, Dtos.LiquidacionReport>()
                    .ForMember(x => x.Cliente, opt => opt.MapFrom(y => y.Cliente.RazonSocialNombre))
                    .ForMember(x => x.Estado, opt => opt.MapFrom(y => EnumHelper.GetDescription(y.Estado)));

                cfg.CreateMap<DetalleLiquidacionPre, Dtos.DetalleLiquidacionPre>();
                cfg.CreateMap<DetalleLiquidacionPre, Dtos.LiquidacionPreReport>()
                    .ForMember(x => x.Estado, opt => opt.MapFrom(y => EnumHelper.GetDescription(y.Estado)))
                    .ForMember(x => x.Fecha, opt => opt.MapFrom(y => y.Fecha.ToString("dd/MM/yyyy")))
                    .ForMember(x => x.Cliente, opt => opt.MapFrom(y => y.Cliente.RazonSocialNombre));
                cfg.CreateMap<Dtos.DetalleLiquidacionPre, DetalleLiquidacionPre>();
                cfg.CreateMap<DetalleLiquidacionPos, Dtos.DetalleLiquidacionPos>();
                cfg.CreateMap<DetalleLiquidacionPos, Dtos.LiquidacionPosReport>()
                    .ForMember(x => x.Estado, opt => opt.MapFrom(y => EnumHelper.GetDescription(y.Estado)))
                    .ForMember(x => x.Fecha, opt => opt.MapFrom(y => y.Fecha.ToString("dd/MM/yyyy")))
                    .ForMember(x => x.Cliente, opt => opt.MapFrom(y => y.Cliente.RazonSocialNombre));
                cfg.CreateMap<Dtos.DetalleLiquidacionPos, DetalleLiquidacionPos>();

                cfg.CreateMap<IngresosBrutosArchivo, Dtos.IngresosBrutosArchivo>();

                cfg.CreateMap<Comprobante, Dtos.Comprobante>();

                cfg.CreateMap<CondicionPago, ErpMasterPaymentMethodDto>()
                .ForMember(x => x.PaymentMethodID, opt => opt.MapFrom(y => y.ErpId))
                .ForMember(x => x.PaymentMethodDescription, opt => opt.MapFrom(y => y.Nombre))
                .ForMember(x => x.PaymentTerm, opt => opt.MapFrom(y => y.TerminoPago)).ReverseMap();

                cfg.CreateMap<Impuesto, Dtos.Impuesto>().ReverseMap();
                cfg.CreateMap<ClienteImpuesto, Dtos.ClienteImpuesto>().ReverseMap();

                cfg.CreateMap<ErpMilongaTaxType, Dtos.TipoImpuesto>().ReverseMap();
                cfg.CreateMap<ErpMilongaIdentificationType, Dtos.TipoDocumento>().ReverseMap();

                cfg.CreateMap<ErpMilongaProductCode, Dtos.CodigoProducto>().ReverseMap();
            });
        }
    }
}
