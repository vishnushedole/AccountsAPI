namespace AccountsWebAPI.Model
{
    public class Account
    {
        public Account(int accid, int custid, int branchid, decimal acc_balance, int freewithdrawl, int freedeposit, int accounttype,  int isactive, int ischeckbook)
        {
            accId = accid;
            custId = custid;
            branchId = branchid;
            balance = acc_balance;
            noOfFreeWithdrawl = freewithdrawl;
            noOfFreeDeposit = freedeposit;
            isCheckBook = ischeckbook;
            accountType = accounttype;
            
            isActive = isactive;
        }
        private int accId;
        private int custId;
        private int branchId;
        private int noOfFreeWithdrawl;
        private int noOfFreeDeposit;
        private int isCheckBook;
        private int accountType;
        private decimal balance;
        private int isActive;

        public int AccountId { get { return accId; } set { accId = value; } }
        public int CustomerId { get { return custId; } set { custId = value; } }
        public int BranchId { get { return branchId; } set { branchId = value; } }
        public int NoOfFreeWithdrawl{ get { return noOfFreeWithdrawl; } set { noOfFreeWithdrawl = value; } }
        public int NoOfFreeDeposit { get { return noOfFreeDeposit; } set { noOfFreeDeposit= value; } }  
        public int IsCheckBook { get { return isCheckBook; } set {  isCheckBook = value; } }
        public int AccountType { get { return accountType; } set { accountType = value; } }
        public decimal Balance { get { return balance; } set { balance = value; } }
        public int IsActive { get { return isActive; } set {  isActive = value; } }


    }
}
