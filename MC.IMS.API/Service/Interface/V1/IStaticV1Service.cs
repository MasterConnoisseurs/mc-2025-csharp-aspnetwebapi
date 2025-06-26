using MC.IMS.API.Models;
using MC.IMS.API.Models.Result.Static;

namespace MC.IMS.API.Service.Interface.V1;

public interface IStaticV1Service
{
    Task<TransactionResult<IEnumerable<AddressRegion>>> GetAddressRegion(long? id, CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<AddressRegionDivision>>> GetAddressRegionDivision(long? id, long? regionId, CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<AddressCountry>>> GetAddressCountry(long? id, long? regionDivisionId, CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<AddressCountryDivision>>> GetAddressCountryDivision(long? id, long? countryId, CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<AddressState>>> GetAddressState(long? id, long? countryDivisionId, CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<AddressCity>>> GetAddressCity(long? id, long? stateId, CancellationToken cancellationToken);
    Task<TransactionResult<IEnumerable<AddressTown>>> GetAddressTown(long? id, long? cityId, CancellationToken cancellationToken);
}