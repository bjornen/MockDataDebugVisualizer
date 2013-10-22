using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MockDataVisualizerTestConsole.Tests.TestObjects.Customer
{
    public interface ICacheKeyObject
    {
        string CacheKey { get; }
    }

    public class Email
    {
        public string Address { get; set; }
        public string Type { get; set; }

        public Email(string emailAddress, string emailType)
        {
            Address = emailAddress;
            Type = emailType;
        }

        public Email(){}
    }

    public class Household
    {
        private IServicedEntity<IEnumerable<StoreUsedStats>> _storesUsed;
        private readonly MasterCustomer _parentMasterCustomer;

        public Household(MasterCustomer parentMasterCustomer)
        {
            _parentMasterCustomer = parentMasterCustomer;
        }

        public Household(){}

        protected Household(MasterCustomer parentMasterCustomer, IServicedEntity<IEnumerable<StoreUsedStats>> storesUsed)
        {
            _parentMasterCustomer = parentMasterCustomer;
            _storesUsed = storesUsed;
        }

        public int Id { get { return (int)_parentMasterCustomer.BestBonusAccount; } }

        public IServicedEntity<IEnumerable<StoreUsedStats>> StoresUsed
        {
            get
            {
                return _storesUsed;
            }
        }

    }

    public class Phone
    {
        public PhoneType PhoneType { get; set; }
        public string SecurityCode { get; set; }
        public string PhoneNumber { get; set; }

        public Phone(string phoneNumber, PhoneType phoneType) : this(phoneNumber, phoneType, "01") { }

        public Phone(string phoneNumber, PhoneType phoneType, string securityCode)
        {
            PhoneNumber = phoneNumber;
            PhoneType = phoneType;
            SecurityCode = securityCode;
        }
    }

    public enum MyStoresCode
    {
        CustomerSelection = 1,
        MonthlyBatchjob = 2,
        DailyBatchjob = 3,
        CustomerRemoved = 4,
        Unknown = -1,
    }

    public class StoreUsedStats
    {
        public MyStoresCode MyStoresCode { get; set; }

        public StoreUsedStats(int storeId, int numberOfPurchases, MyStoresCode myStoresCode)
        {
            StoreId = storeId;
            NumberOfPurchases = numberOfPurchases;
            MyStoresCode = myStoresCode;
        }

        public StoreUsedStats(){}

        public int StoreId { get; protected set; }

        public int NumberOfPurchases { get; protected set; }

        public int Points { get; protected set; }

        public string StoreName { get; set; }

        public static StoreUsedStats Create(decimal storeId, decimal numberOfPurchases, decimal pointQuantity, MyStoresCode myStoresCode)
        {
            var store = new StoreUsedStats((int)storeId, (int)numberOfPurchases, myStoresCode);

            store.Points = (int)pointQuantity;

            return store;
        }
    }

    public enum PhoneType
    {
        Mobile = 'M',
        Home = 'T',
        Unknown
    }

    public class CustomerBonusData
    {
        public decimal AvailableAmountOfMoneyOnAccount { get; private set; }
        public decimal CreditOnAccount { get; private set; }
        public decimal BalanceOfAccount { get; set; }
        public decimal CurrentAmountOfPointsOfAccount { get; private set; }
        public decimal RecievedBonusOfCurrentYear { get; private set; }
        public decimal CurrentDiscountOnCard { get; private set; }
        public decimal PurchasesMadeOnIcaCurrentYear { get; private set; }
        public decimal PurchasesMadeOnStatoilCurrentYear { get; private set; }
        public decimal PurchasesMadeOnOtherPlacesCurrentYear { get; private set; }
        public decimal AmountOfSwedishCrownsToNextBonusCheck { get; private set; }
        public decimal AccountNumber { get; private set; }

        public IList<CustomerCardTransaction> CustomerCardTransactions { get; private set; }
    }

    public class CustomerCardTransaction
    {
        public string TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public decimal PointAmount { get; set; }
    }

    public class BonusData 
    {
        public decimal CurrentAmountOfPointsOfAccount { get; set; }
        public decimal DiscountAmountPerYear { get; set; }
        private decimal _amountOfSwedishCrowndsToNextBonusCheck;
        public decimal AmountOfSwedishCrownsToNextBonusCheck
        {
            get
            {
                return _amountOfSwedishCrowndsToNextBonusCheck;
            }
            set
            {

                int currentPointSum = Int32.Parse(value.ToString());
                int nextBomuslevel = (currentPointSum / 2500 + 1) * 2500;
                decimal amountToNextCoupon = nextBomuslevel - currentPointSum;

                if (amountToNextCoupon <= 0)
                {
                    amountToNextCoupon = 0;
                }
                _amountOfSwedishCrowndsToNextBonusCheck = amountToNextCoupon;
            }
        }
        public decimal PurchasesMadeOnIcaStatiolCurrentYear { get; set; }
        public decimal RecievedBonusOfCurrentYear { get; set; }

        public static BonusData Create(decimal currentAmountOfPointsOfAccount,
                                        decimal amountOfSwedishCrownsToNextBonusCheck,
                                        decimal purchasesMadeOnIcaStatiolCurrentYear,
                                        decimal recievedBonusOfCurrentYear,
                                        decimal discountAmountPerYear)
        {
            var bonusData = new BonusData
            {
                CurrentAmountOfPointsOfAccount = currentAmountOfPointsOfAccount,
                AmountOfSwedishCrownsToNextBonusCheck = amountOfSwedishCrownsToNextBonusCheck,
                PurchasesMadeOnIcaStatiolCurrentYear = purchasesMadeOnIcaStatiolCurrentYear,
                RecievedBonusOfCurrentYear = recievedBonusOfCurrentYear,
                DiscountAmountPerYear = discountAmountPerYear
            };

            return bonusData;
        }
    }

    public class AccountTransactions
    {
        public string TransactionType { get; protected set; }
        public DateTime TransactionDate { get; protected set; }
        public decimal Amount { get; protected set; }
        public DateTime ValueDate { get; protected set; }
        public string Description { get; protected set; }
        public int TransactionCategoryCode { get; set; }

        public AccountTransactions(string transactionType, DateTime transactionDate, DateTime valueDate, decimal amount, string description, int transactionCategoryCode)
        {
            TransactionType = transactionType;
            TransactionDate = transactionDate;
            Amount = amount;
            ValueDate = valueDate;
            Description = description;
            TransactionCategoryCode = transactionCategoryCode;
        }

        public AccountTransactions()
        {
        }

        public static AccountTransactions Create(string transactionType, DateTime transactionDate, decimal amount, DateTime valueDate, string description, int transactionCategoryCode)
        {
            var transaction = new AccountTransactions(transactionType, transactionDate, valueDate, amount, description, transactionCategoryCode);

            return transaction;
        }
    }

    public enum AccountTransactionType
    {
        _ = 1,
        Köp,
        Kreditering,
        Inbetalning,
        Inbetalning_autogiro,
        Avvikelse_autogiro,
        Justering_inbetalning,
        Justering_saldo,
        Kontant_utbetalning_av_tillgodo,
        Kontantuttag,
        Insättning_i_butik,
        Prenumeration_ICA_förlaget,
        Aviavgift,
        Förseningsavgift,
        Tillgodoränta,
        Köpmånadsränta, Kontokreditsränta,
        Rabatt, Ränterabatt,
        Drivmedelsrabatt,
        Preliminär_skatt, Vinst,
        Premie,
        Kompensation,
        Vinst_ATG,
        Avgift_ATG, Återbetalning_ATG,
        Rättelse_ATG, Vinst_Svenska_spel,
        Rättelse_Svenska_spel,
        Justering_ränta,
        Justering_avgift,
        Överfört_tillgodo,
        Avskrivning,
        Öresutjämning,
        Hittelön,
        Premie_olycksfall,
        Premie_spärrservice,
        Justering_spärrservice,
        Ränterabatt_drivmedel
    }

    public class AccountData
    {
        private MasterCustomer _parentMasterCustomer;
        public decimal Available { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public decimal Reserved { get; set; }
        public string AccountStatus { get; set; }
        public IEnumerable<AccountTransactions> Transactions { get; set; }
        public decimal AccountNumber { get; private set; }
        public bool IncompleteContent { get; set; }
        public decimal CurrentRunningBalance { get; set; }

        public static string[] AccountTypeDescription
        {
            get
            {
                return new[] {"", "Köp", "Kreditering", "Inbetalning", "Inbetalning autogiro",
							"Avvikelse autogiro", "Justering inbetalning", "Justering saldo", "Inbetalning", 
                            "Kontant utbetalning av tillgodo", "Kontantuttag", "Insättning i butik", 
                            "Prenumeration ICA förlaget", "Aviavgift", "Förseningsavgift",
							"Tillgodoränta", "Köpmånadsränta", "Kontokreditsränta", "Rabatt", "Ränterabatt",
							"Drivmedelsrabatt", "Preliminär skatt", "Vinst", "Premie", "Kompensation", 
							"Vinst ATG", "Avgift ATG", "Återbetalning ATG", "Rättelse ATG", "Vinst Svenska spel", 
                            "Rättelse Svenska spel", "Justering ränta", "Justering avgift", "Överfört tillgodo", 
                            "Avskrivning", "Öresutjämning", "Hittelön", "Premie olycksfall", "Premie spärrservice", 
                            "Justering spärrservice", "Drivmedelsrabatt", "Ränterabatt" 
                };
            }
        }

        public static AccountData Create(
            decimal available,
            decimal creditLimit,
            decimal balance,
            decimal reserved,
            string accountStatus,
            IEnumerable<AccountTransactions> transactions,
            decimal accountNumber,
            bool incompleteContent,
            decimal currentRunningBalance
            )
        {
            var accountData = new AccountData
            {
                Available = available,
                CreditLimit = creditLimit,
                Balance = balance,
                Reserved = reserved,
                AccountStatus = accountStatus,
                Transactions = transactions,
                AccountNumber = accountNumber,
                IncompleteContent = incompleteContent,
                CurrentRunningBalance = currentRunningBalance
            };

            return accountData;
        }

        public AccountData()
        {
        }

        public AccountData(MasterCustomer parentMasterCustomer)
        {
            _parentMasterCustomer = parentMasterCustomer;
        }
    }

    public class CardData
    {
        private MasterCustomer _parentMasterCustomer;
        public decimal CardNumber { get; set; }
        public bool Blocked { get; set; }
        public string BlockCardReason { get; set; }
        public bool IncompleteContent { get; set; }

        public static CardData Create(decimal cardNumber,
                                      bool blocked,
                                      string blockCardReason,
                                      bool incompleteContent
        )
        {
            var cardData = new CardData
            {
                CardNumber = cardNumber,
                Blocked = blocked,
                BlockCardReason = blockCardReason,
                IncompleteContent = incompleteContent

            };

            return cardData;
        }

        public CardData()
        {

        }
        public CardData(MasterCustomer parentMasterCustomer)
        {
            _parentMasterCustomer = parentMasterCustomer;
        }
    }

    public class MasterCustomer : ICacheKeyObject
    {
        public int Id { get; set; }
        public decimal? CardId { get; set; }
        public Guid? OnlineId { get; set; }
        public string StudentInd { get; set; }
        public DateTime StudyEndDate { get; set; }
        public bool UpdateStudyEndDate { get; set; }
        public bool StudyEndDateSpecified { get; set; }
        public string StudyFocusAreaDesc { get; set; }

        public decimal CivicRegistrationNumber { get; set; }
        public string Address { get; set; }
        public decimal BestBonusAccount { get; set; }
        public Household Household { get; protected set; }
        public string CellPhoneNumber
        {
            get { return GetPhoneNumber(PhoneType.Mobile); }
            set { SetPhoneNumber(value, PhoneType.Mobile); }
        }
        public string City { get; set; }
        public string CoAttName { get; set; }
        public string Country { get; set; }
        public string CustomerStatus { get; set; }
        public decimal CustomerSubscriptionChoice { get; set; }
        public string HomeNumber
        {
            get { return GetPhoneNumber(PhoneType.Home); }
            set { SetPhoneNumber(value, PhoneType.Home); }
        }
        public string Forename { get; set; }
        public string NameDepartment { get; set; }
        public string ProtectedAddressInd { get; set; }
        public string Surname { get; set; }
        public decimal ZipCode { get; set; }

        public List<Email> EmailAdresses { get; set; }
        public CustomerBonusData BonusData { get; set; }
        public string Fullname { get; set; }
        public List<Phone> Phones { get; private set; }

        protected IServicedEntity<BonusData> _bonus;
        public IServicedEntity<BonusData> Bonus
        {
            get
            {
                return _bonus;
            }
        }

        protected IServicedEntity<AccountData> _account;
        public IServicedEntity<AccountData> Account
        {
            get
            {
                return _account;
            }
        }


        protected IServicedEntity<CardData> _card;

        public IServicedEntity<CardData> Card
        {
            get
            {
                return _card;
            }
        }


        // To be able to initialize household from mapper.
        public virtual void InitializeHouseHold()
        {
            Household = new Household(this);
        }

        public virtual void InitializeHouseHold(Household household)
        {
            Household = household;
        }

        /// <summary>
        /// Mapping customer bonus data from WS to CustomerBonusData object
        /// </summary>
        /// <param name="customerBonusData">output from Web Service GetBonusData</param>
        /// <returns>0=OK, 1=Warning, 2=Serious error, 3=Missing</returns>
        public enum EmailType
        {
            Home = 1,
            Work = 2,
        }

        private static string NameFormatter(string fullname)
        {
            fullname = new CultureInfo("sv-SE").TextInfo.ToTitleCase(fullname.ToLower());
            return fullname.Replace(" Von ", " von ").Replace(" Af ", " af ");
        }

        public Email GetCustomerEmail(EmailType type)
        {
            Email email = null;
            string emailType = GetEmailType(type);
            if (EmailAdresses != null)
            {
                email = EmailAdresses.FirstOrDefault(x => x.Type.Equals(emailType));
            }
            return email;
        }

        public void SetCustomerEmail(string email, EmailType type)
        {
            if (email == null)
                return;

            var newEmail = new Email(email, GetEmailType(type));

            if (EmailAdresses == null)
            {
                EmailAdresses = new List<Email> { newEmail };
                return;
            }

            bool foundType = false;

            for (int i = 0; i < EmailAdresses.Count; i++)
            {
                if (EmailAdresses[i].Type.Equals(GetEmailType(type)))
                {
                    foundType = true;
                    EmailAdresses[i] = newEmail;
                }
            }

            if (!foundType)
                EmailAdresses.Add(newEmail);
        }

        private static string GetEmailType(EmailType type)
        {
            return string.Format("0{0}", (int)type);
        }

        private string GetPhoneNumber(PhoneType phoneType)
        {
            if (Phones == null)
                return string.Empty;

            var phone = Phones.Where(x => x.PhoneType == phoneType).FirstOrDefault();

            if (phone == null)
                return string.Empty;

            return phone.PhoneNumber;

        }

        private void SetPhoneNumber(string number, PhoneType phoneType)
        {
            if (number == null)
                number = string.Empty;

            var newPhone = new Phone(number, phoneType);

            if (Phones == null)
            {
                Phones = new List<Phone> { newPhone };
                return;
            }

            var foundPhone = Phones.Where(x => x.PhoneType != phoneType).ToList();
            foundPhone.Add(newPhone);
            Phones = foundPhone;

        }

        public void SetPhoneNumbers(List<Phone> phones)
        {
            if (phones == null)
                return;

            Phones = phones;
        }

        public string CacheKey
        {
            get { return Convert.ToString(CivicRegistrationNumber); }
        }

    }
}
