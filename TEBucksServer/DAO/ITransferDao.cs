using System.Collections.Generic;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface ITransferDao
    {
        public List<Transfer> GetAllTransfers();
        public Transfer GetTransferById(int transferId);
        public List<Transfer> GetTransfersByPersonId(int personId);
        public List<Transfer> GetAllTransfersByTypeAndPersonId(int personId, string transferType);
        public List<Transfer> GetAllTransfersByStatusAndPersonId(int personId, string tranferStatus);
        public Transfer CreateTransfer(Transfer incoming);
        public Transfer EditTransferStatus(Transfer incoming);
    }
}
