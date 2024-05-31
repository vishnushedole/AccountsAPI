using System.Runtime.InteropServices;

namespace AccountsWebAPI.Model
{
    public class Transactions
    {

        public Transactions(int tid,int stid,int dtid,decimal transfer_amount,DateTime datetime) {
            transactionId = tid;
            sourceTransactionId = stid;
            destinationTransactionId = dtid;
            amount = transfer_amount;
            dateTime = datetime;
        }
        private int transactionId;
        private int sourceTransactionId;
        private int destinationTransactionId;
        private decimal amount;
        private DateTime dateTime;

      public int TransactionId {  get { return transactionId; } set { transactionId = value; } }
        public int SourceTransactionId { get { return sourceTransactionId; } set { sourceTransactionId = value; } }
        public int DestinationTransactionId { get { return destinationTransactionId; } set {  destinationTransactionId = value; } }

        public decimal Amount {  get { return amount; } set {  amount = value; } }

        public DateTime DateTime { get { return dateTime; } set { dateTime = value; } }
    }
}
