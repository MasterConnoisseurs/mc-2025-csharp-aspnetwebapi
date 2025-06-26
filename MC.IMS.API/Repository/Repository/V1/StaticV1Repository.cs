using MC.IMS.API.Models;
using MC.IMS.API.Models.Result.Static;
using MC.IMS.API.Repository.Constants;
using MC.IMS.API.Repository.Interface;
using MC.IMS.API.Repository.Interface.V1;

namespace MC.IMS.API.Repository.Repository.V1;

public class StaticV1Repository(IDbFactoryWrapperRepository databaseContext) : IStaticV1Repository
{
    public async Task<TransactionResult<IEnumerable<AddressRegion>>> GetAddressRegion(long? id)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<AddressRegion>
            (V1ProcedureReference.Static.GetAddressRegion, new { id });
    }
    public async Task<TransactionResult<IEnumerable<AddressRegionDivision>>> GetAddressRegionDivision(long? id, long? regionId)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<AddressRegionDivision>
            (V1ProcedureReference.Static.GetAddressRegionDivision, new { id, regionId });
    }
    public async Task<TransactionResult<IEnumerable<AddressCountry>>> GetAddressCountry(long? id, long? regionDivisionId)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<AddressCountry>
            (V1ProcedureReference.Static.GetAddressCountry, new { id, regionDivisionId });
    }
    public async Task<TransactionResult<IEnumerable<AddressCountryDivision>>> GetAddressCountryDivision(long? id, long? countryId)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<AddressCountryDivision>
            (V1ProcedureReference.Static.GetAddressCountryDivision, new { id, countryId });
    }
    public async Task<TransactionResult<IEnumerable<AddressState>>> GetAddressState(long? id, long? countryDivisionId)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<AddressState>
            (V1ProcedureReference.Static.GetAddressState, new { id, countryDivisionId });
    }
    public async Task<TransactionResult<IEnumerable<AddressCity>>> GetAddressCity(long? id, long? stateId)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<AddressCity>
            (V1ProcedureReference.Static.GetAddressCity, new { id, stateId });
    }
    public async Task<TransactionResult<IEnumerable<AddressTown>>> GetAddressTown(long? id, long? cityId)
    {
        return await databaseContext.ExecuteMultipleResultStoredProcedureAsync<AddressTown>
            (V1ProcedureReference.Static.GetAddressTown, new { id, cityId });
    }
}