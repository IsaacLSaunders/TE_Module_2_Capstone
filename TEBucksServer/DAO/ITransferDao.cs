using System.Collections.Generic;
using System.Data.SqlClient;
using TEBucksServer.DTO;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface ITransferDao
    {
        public Transfer CreateTransfer(NewTransferDto incoming);
        public Transfer EditTransferStatus(TransferStatusUpdateDto status, int id);
        public List<Transfer> GetTransferByUserFromId(int userId);
        public List<Transfer> GetTransferByUserToId(int userId);
        public Transfer GetTransferByTransferId(int transferId);
        public List<Transfer> GetTransfersByUserName(string userName);
        public TempTransfer MapRowToTransfer(SqlDataReader reader);

    }
}
