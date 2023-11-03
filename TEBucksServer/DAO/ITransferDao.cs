using System.Collections.Generic;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface ITransferDao
    {
        public Transfer GetTransferById(int transferId);
        public List<TransferDto> GetTransfersByPersonId(int userId);
        public List<Transfer> GetAllTransfersByTypeAndPersonId(int personId, string transferType);
        public List<Transfer> GetAllTransfersByStatusAndPersonId(int personId, string tranferStatus);
        public TransferDto CreateTransfer(Transfer incoming);
        public Transfer EditTransferStatus(TransferStatusUpdateDto status, int id);
    }
}
