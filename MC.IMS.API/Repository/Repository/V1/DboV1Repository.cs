using MC.IMS.API.Helpers;
using MC.IMS.API.Models;
using MC.IMS.API.Models.Result.Dbo;
using MC.IMS.API.Repository.Constants;
using MC.IMS.API.Repository.Interface;
using MC.IMS.API.Repository.Interface.V1;

namespace MC.IMS.API.Repository.Repository.V1;

public class DboV1Repository(IDbFactoryWrapperRepository databaseContext) : IDboV1Repository
{
    public async Task<TransactionResult<IEnumerable<ProductSummary>>> GetProductSummary()
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<ProductSummary>
            (V1ProcedureReference.Dbo.Get.GetProductSummary, null);
    }
    public async Task<TransactionResult<IEnumerable<TotalBookingPerStatus>>> GetTotalBookingPerStatus(StaticDataHelper.TimePeriodOption? timePeriodOption)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<TotalBookingPerStatus>
            (V1ProcedureReference.Dbo.Get.GetTotalBookingPerStatus, new { timePeriodOption });
    }
    public async Task<TransactionResult<IEnumerable<SummaryYearToYear>>> GetSummaryYearToYear()
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<SummaryYearToYear>
            (V1ProcedureReference.Dbo.Get.GetSummaryYearToYear, null);
    }
}