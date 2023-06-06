using chrep.core.park.Dtos;
using chrep.core.park.InputVm;
using chrep.core.park.Models;

namespace chrep.core.park.Interfaces
{
    public interface IDemandeService:IDataHelper<Demande>
    {
        Task<Demande> InsertDemande(DemandeVm demandeVm);
        Task<List<DemandeDtos>> getDemandeByUserId(int  userId);
        Task<DemandeDetailDtos> getDemandeDetailById(int Id);
        Task<Demande> updateDemande(DemandeVm demandeVm);
        
    }
}
