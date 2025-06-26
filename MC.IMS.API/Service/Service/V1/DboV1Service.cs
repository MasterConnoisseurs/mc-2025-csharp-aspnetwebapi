using MC.IMS.API.Helpers;
using MC.IMS.API.Helpers.Config;
using MC.IMS.API.Models;
using MC.IMS.API.Models.Result.Dbo;
using MC.IMS.API.Repository.Interface.V1;
using MC.IMS.API.Service.Interface.V1;

namespace MC.IMS.API.Service.Service.V1;

public class DboV1Service(IDboV1Repository dboV1Repository) : IDboV1Service
{
    public async Task<TransactionResult<IEnumerable<ProductSummary>>> GetProductSummary(CancellationToken cancellationToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(SystemDefaultsConfig.Config.ApiTimeoutInSeconds));
        return await dboV1Repository.GetProductSummary();
    }

    public async Task<TransactionResult<IEnumerable<TotalBookingPerStatus>>> GetTotalBookingPerStatus(StaticDataHelper.TimePeriodOption? timePeriodOption, CancellationToken cancellationToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(SystemDefaultsConfig.Config.ApiTimeoutInSeconds));
        return await dboV1Repository.GetTotalBookingPerStatus(timePeriodOption);
    }

    public async Task<TransactionResult<IEnumerable<SummaryYearToYear>>> GetSummaryYearToYear(CancellationToken cancellationToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(SystemDefaultsConfig.Config.ApiTimeoutInSeconds));
        return await dboV1Repository.GetSummaryYearToYear();
    }
}