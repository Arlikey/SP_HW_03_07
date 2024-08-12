using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_03_07
{
    internal class AdditionalTask1
    {
        static void Main(string[] args)
        {
            Account[] accounts = new Account[]
        {
            new Account(1, 500),
            new Account(2, 1000),
            new Account(3, 1500)
        };

            Client[] clients = new Client[]
            {
            new Client(accounts[0]),
            new Client(accounts[1]),
            new Client(accounts[2])
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(30000);
            CancellationToken token = cts.Token;

            Thread[] threads = new Thread[clients.Length];

            for (int i = 0; i < clients.Length; i++)
            {
                int clientIndex = i;
                threads[i] = new Thread(() => PerformOperations(clients[clientIndex], token));
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("Program has been successfully completed.");
            Console.ReadLine();
        }
        static void PerformOperations(Client client, CancellationToken token)
        {
            Random rand = new Random();
            while (!token.IsCancellationRequested)
            {
                if (rand.Next(2) == 0)
                {
                    client.Replenish(rand.Next(1, 100));
                }
                else
                {
                    client.Withdraw(rand.Next(1, 100));
                }
                Thread.Sleep(rand.Next(1000, 3000));
            }
        }
    }

    public class Account
    {
        private static readonly object locker = new object();
        public int Id { get; }
        private decimal balance;

        public Account(int id, decimal money)
        {
            Id = id;
            balance = money;
        }

        public decimal Balance
        {
            get
            {
                lock (locker)
                {
                    return balance;
                }
            }
            set 
            {
                lock (locker)
                {
                    balance = value;
                }
            }
        }
    }

    public class Client
    {
        private Account account;
        public Client(Account account)
        {
            this.account = account;
        }
        public void Withdraw(decimal money)
        {
            if (account.Balance - money < 0)
            {
                Console.WriteLine($"Account ID: {account.Id} cannot withdraw money. Not enough balance. Current balance: {account.Balance}$");
                return;
            }
            account.Balance -= money;
            Console.WriteLine($"Account ID: {account.Id} withdraw {money}$. Current balance: {account.Balance}$");
        }

        public void Replenish(decimal money)
        {
            account.Balance += money;
            Console.WriteLine($"Account ID : {account.Id} has replenished {money}$. Current balance: {account.Balance}$");
        }
    }
}
