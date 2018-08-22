using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingService.Account
{
    public interface IManageAccount : IAccount
    {
        DateTime OpeningDate { get; }

        string OwnerId { get; }

        decimal MonthlyFee { get; set; }

        void Open();

        void Close();

        void Freeze();

        void UnFreeze();

        void ChangeOwner(string newOwnerId);
    }
}
