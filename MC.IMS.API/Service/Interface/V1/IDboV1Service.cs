using MC.IMS.API.Helpers;
using MC.IMS.API.Models;
using MC.IMS.API.Models.Result.Dbo;

namespace MC.IMS.API.Service.Interface.V1;

public interface IDboV1Service
{
    Task<TransactionResult<IEnumerable<ProductSummary>>> GetProductSummary(CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<TotalBookingPerStatus>>> GetTotalBookingPerStatus(StaticDataHelper.TimePeriodOption? timePeriodOption, CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<SummaryYearToYear>>> GetSummaryYearToYear(CancellationToken cancellationToken);
}