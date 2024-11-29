using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using OrderApp.Data;


namespace OrderApp.ViewModels
{
    internal class OrdersViewModel : INotifyPropertyChanged
    {
        private string _orderNumber;
        private string _customerName;
        private DateTime _orderDate = DateTime.Now;
        private decimal _orderAmount;

        public string OrderNumber
        {
            get => _orderNumber;
            set
            {
                _orderNumber = value;
                OnPropertyChanged(nameof(OrderNumber));
            }
        }

        public string CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set
            {
                _orderDate = value;
                OnPropertyChanged(nameof(OrderDate));
            }
        }

        public decimal OrderAmount
        {
            get => _orderAmount;
            set
            {
                _orderAmount = value;
                OnPropertyChanged(nameof(OrderAmount));
            }
        }

        public ObservableCollection<Order> Orders { get; set; }

        public ICommand AddOrderCommand { get;}

        public OrdersViewModel() 
        { 
            using (var context = new AppDbContext())
            {
                Orders = new ObservableCollection<Order>(context.Orders.ToList());
            }

            AddOrderCommand = new RelayCommand(AddOrder, CanAddOrder);
        }

        private void AddOrder()
        {
            var newOrder = new Order
            {
                OrderNumber = OrderNumber,
                CustomerName = CustomerName,
                OrderDate = OrderDate,
                OrderAmount = OrderAmount
            };

            using (var context = new AppDbContext())
            {
                context.Orders.Add(newOrder);
                context.SaveChanges();
            }

            Orders.Add(newOrder);

            OrderNumber = string.Empty;
            CustomerName = string.Empty;
            OrderDate = DateTime.Now;
            OrderAmount = 0;
        }

        private bool CanAddOrder()
        {
            return !string.IsNullOrEmpty(OrderNumber);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
