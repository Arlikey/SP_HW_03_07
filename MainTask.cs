namespace SP_HW_03_07
{
    internal class MainTask
    {
        static void Main(string[] args)
        {
            Queue<Customer> customers = new Queue<Customer>();
            for (int i = 0; i < 20; i++)
            {
                customers.Enqueue(new Customer(i));
            }

            List<CashRegister> cashRegisters = new List<CashRegister>();
            for (int i = 0; i < 3; i++)
            {
                cashRegisters.Add(new CashRegister(customers, i));
            }

            foreach (CashRegister cashRegister in cashRegisters)
            {
                cashRegister.Start();
            }

            Console.ReadLine();
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public Customer(int id)
        {
            Id = id;
        }
    }

    public class CashRegister
    {
        private Queue<Customer> customers;
        public Queue<Customer> Customers { get { return customers; } }
        private Thread thread;
        private int _registerNumber;
        private static readonly Random random = new Random();

        public CashRegister(Queue<Customer> customers, int registerNumber)
        {
            this.customers = customers;
            thread = new Thread(ProcessCustomers);
            _registerNumber = registerNumber;
        }
        public void Start() => thread.Start();
        private void ProcessCustomers(object? obj)
        {
            while (true)
            {
                Customer customer;
                lock (customers)
                {
                    if (customers.Count == 0)
                    {
                        break;
                    }
                    customer = customers.Dequeue();
                }
                Console.WriteLine($"Cash Register {_registerNumber} started working for customer {customer.Id}");
                int workTime = random.Next(1, 4) * 1000;
                Thread.Sleep(workTime);
                Console.WriteLine($"Cash Register {_registerNumber} finished work for customer {customer.Id}. Work time: {workTime/1000} seconds");
            }
        }
    }
}
