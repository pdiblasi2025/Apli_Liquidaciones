using Api.Core.Dtos;
using Api.Core.Dtos.ErpMilonga;
using Api.Core.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E = Api.Core.Entities;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaInvoiceService : IErpMilongaInvoiceService
    {
        private readonly IMapper _mapper; 
        private readonly IErpMilongaService _erpMilongaService;
        public ErpMilongaInvoiceService(IMapper mapper, IErpMilongaService erpMilongaService)
        {
            _mapper = mapper;
            _erpMilongaService = erpMilongaService;
        }

        public InvoiceOrderDto GenerateInvoice(E.Liquidacion liquidacion)
        {
            try
            {
                return new InvoiceOrderDto
                {
                    OrderCode = liquidacion.Id.ToString(),
                    OrderDate = liquidacion.CreateDate,
                    Remarks = liquidacion.Descripcion,
                    OrderTypeID = "01",
                    StoreID = "",
                    PaymentMethodID = liquidacion.Cliente.CondicionPago.ErpId,
                    Payment = GetPayments(liquidacion),
                    Customer = new InvoiceCustomerDto
                    {
                        CustomerCode = liquidacion.Cliente.OmsId.ToString(),
                        FirstName = liquidacion.Cliente.RazonSocial ?? liquidacion.Cliente.Nombre,
                        LastName = !string.IsNullOrEmpty(liquidacion.Cliente.RazonSocial) ? "" : liquidacion.Cliente.Nombre,
                        Telephone = liquidacion.Cliente.Telefono,
                        Email = liquidacion.Cliente.Email ?? string.Empty
                    },
                    BillingAddress = new InvoiceBillingAddressDto
                    {
                        BusinessName = liquidacion.Cliente.RazonSocialNombre,
                        TaxTypeID = liquidacion.Cliente.TipoImpuesto.TaxTypeID,
                        IdentificationType = liquidacion.Cliente.TipoDocumento.IdentificationTypeID,
                        IdentificationCode = liquidacion.Cliente.NumeroDeDocumento,
                        StateName = liquidacion.Cliente.Localidad.Provincia.Nombre,
                        CityName = liquidacion.Cliente.Localidad.Nombre,
                        StreetName = liquidacion.Cliente.Calle,
                        Door = liquidacion.Cliente.Altura.ToString(),
                        Appartment = liquidacion.Cliente.Depto,
                        ZipCode = liquidacion.Cliente.CodigoPostal,
                        Telephone = liquidacion.Cliente.Telefono,
                        Email = liquidacion.Cliente.Email ?? string.Empty
                    },
                    Tax = liquidacion.Cliente.Impuestos.Any() ? liquidacion.Cliente.Impuestos.Select(x => new InvoiceTaxDto
                    {
                        TaxCode = x.Impuesto.Codigo,
                        ExemptionPercent = x.PorcentajeExencion ?? 0,
                        ExemptFrom = x.ExencionDesde,
                        ExemptTo = x.ExencionHasta
                    }).ToList() : new List<InvoiceTaxDto>(),
                    OrderItem = GetOrderItems(liquidacion.Conceptos)
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
            

        }

        private static List<InvoicePaymentDto> GetPayments(E.Liquidacion liquidacion)
        {
            var payments = new List<InvoicePaymentDto>();

            if (liquidacion.DetalleLiquidacionPre.Any())
            {
                foreach (var orden in liquidacion.DetalleLiquidacionPre)
                {
                    var payment = new InvoicePaymentDto
                    {
                        PaymentMethodID = liquidacion.Cliente.CondicionPago.ErpId,
                        PaymentTransactionID = orden.IdMercadoPago ?? string.Empty,
                        Amount = orden.ValorSinImpuesto,
                        PaymentTerm = liquidacion.Cliente.CondicionPago.TerminoPago
                    };

                    payments.Add(payment);
                }
            }
            else
            {
                foreach (var envio in liquidacion.DetalleLiquidacionPos)
                {
                    var payment = new InvoicePaymentDto
                    {
                        PaymentMethodID = liquidacion.Cliente.CondicionPago.ErpId,
                        PaymentTransactionID = string.Empty,
                        Amount = envio.ValorSinImpuesto,
                        PaymentTerm = liquidacion.Cliente.CondicionPago.TerminoPago
                    };

                    payments.Add(payment);
                }
            }

            return payments;
        }

        private List<InvoiceOrderItemDto> GetOrderItems(IList<E.ConceptoLiquidacion> conceptos)
        {
            var orderItems = new List<InvoiceOrderItemDto>();

            foreach (var concepto in conceptos)
            {
                orderItems.Add(new InvoiceOrderItemDto
                {
                    ProductTypeID = concepto.Concepto.CodigoProducto.ProductTypeCode,
                    ProductCode = concepto.Concepto.CodigoProducto.ProductCode,
                    ProductName = concepto.Concepto.CodigoProducto.ProductName,
                    ProductDescription = concepto.Observacion,
                    OriginalUnitPrice = !concepto.Concepto.Descuento ? concepto.Monto : -concepto.Monto,
                    UnitPrice = !concepto.Concepto.Descuento ? concepto.Monto : -concepto.Monto,
                    RequestedQuantity = 1
                });
            }

            return orderItems;
        }


        public async Task<ErpInvoiceResponseDto> Sync(InvoiceOrderDto order)
        {
            return await _erpMilongaService.PostOrderAsync(order);
        }
    }
}
