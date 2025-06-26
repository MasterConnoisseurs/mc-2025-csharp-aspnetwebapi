using MC.IMS.API.Helpers;
using MC.IMS.API.Models;
using MC.IMS.API.Models.Result.Dbo;

namespace MC.IMS.API.Repository.Interface.V1;

public interface IDboV1Repository
{
    Task<TransactionResult<IEnumerable<ProductSummary>>> GetProductSummary();
    Task<TransactionResult<IEnumerable<TotalBookingPerStatus>>> GetTotalBookingPerStatus(StaticDataHelper.TimePeriodOption? timePeriodOption);
    Task<TransactionResult<IEnumerable<SummaryYearToYear>>> GetSummaryYearToYear();
}