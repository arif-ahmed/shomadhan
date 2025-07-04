using System.Text.Json.Serialization;

namespace Shomadhan.Domain.Enums;

public enum AccountInfoType
{
    Cash,
    Bank,
    Mobile,
    Other
}

public enum TransactionFlowType
{
    Income = 1,
    Expense = 2,
}

public enum TransactionFor
{
    All = 0,
    Sale = 1,
    Purchase = 2,
    Office = 3,
    Other = 4
}

public enum TransactionWith
{
    All = 0,
    Customer = 1,
    Supplier = 2,
    Employee = 3,
    Dealer = 4,
    Partner = 5,
    Other = 6
}

public enum TransactionMedium
{
    Cash = 1,
    Card = 2,
    Cheque = 3,
    Mobile = 4,
    Bank = 5,
    Other = 6
}

public enum ReportTimeType
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}

public enum SaleChannel
{
    All,
    InHouse,
    CashOnDelivery,
    Courier,
    Condition,
    Other
}

public enum SaleFrom
{
    All,
    BizBook365,
    Facebook,
    Website,
    PhoneCall,
    MobileApp,
    Referral,
    Other,
}

public enum OrderState
{
    //All, //0
    Pending = 1,
    Created, //2
    ReadyToDeparture, //3
    OnTheWay, // 4
    Delivered, //5
    Completed, //6
    Cancel // 7
}

public enum SmsReceiverType
{
    Unknown = 0,
    Customer,
    Dealer,
    User,
    Supplier
}

public enum SmsReasonType
{
    Unknown = 0,
    Sale,
    Purchase,
    Transaction
}

public enum BizSmsHook
{
    OrderPending = 1,
    OrderCreated,
    OrderReadyToDepurture
}


public enum StockTransferState
{
    Pending = 0,
    Approved = 1
}
