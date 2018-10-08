using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using ServiceLayer;
using Zadanie4.Model;
using Zadanie4.View;

namespace Zadanie4.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private ObservableCollection<ProductReview> reviews;
        private ObservableCollection<Product> products;
        private ProductReview currentReview;
        private Product currentProduct;
        private DataLayer dataLayer;

        // właściwości do pobrania danych
        private int newProductID;
        public int NewProductID
        {
            get => newProductID;
            set
            {
                newProductID = value;
                RaisePropertyChanged();
            }
        }

        private string newReviewerName;
        public string NewReviewerName
        {
            get => newReviewerName;
            set
            {
                newReviewerName = value;
                RaisePropertyChanged();
            }
        }

        private string newEmailAddress;
        public string NewEmailAddress
        {
            get => newEmailAddress;
            set
            {
                newEmailAddress = value;
                RaisePropertyChanged();
            }
        }

        private int newRating;
        public int NewRating
        {
            get => newRating;
            set
            {
                newRating = value;
                RaisePropertyChanged();
            }
        }

        private string newComments;
        public string NewComments
        {
            get => newComments;
            set
            {
                newComments = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProductReview> ProductReviews
        {
            get => reviews;
            set { reviews = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<Product> Products
        {
            get => products;
            set { products = value; RaisePropertyChanged(); }
        }

        public ProductReview CurrentProductReview
        {
            get => currentReview;
            set
            {
                currentReview = value;
                RaisePropertyChanged();
                // ustawienie currentProduct
                CurrentProduct = products.First(p => p.ProductID == currentReview.ProductID);
            }
        }

        public Product CurrentProduct
        {
            get => currentProduct;
            set { currentProduct = value; RaisePropertyChanged(); }
        }

        public DataLayer DataLayer
        {
            get => dataLayer;
            set
            {
                dataLayer = value;
                ProductReviews = new ObservableCollection<ProductReview>(value.ProductReviews);
                Products = new ObservableCollection<Product>(value.Products);
            }
        }

        public DelegateCommand GetDataCommand { get; private set; }

        public DelegateCommand AddProductReviewCommand { get; private set; }

        public DelegateCommand DeleteProductReviewCommand { get; private set; }

        public DelegateCommand UpdateProductReviewCommand { get; private set; }

        public MainViewModel()
        {

            dataLayer = new DataLayer();
            GetDataCommand = new DelegateCommand(() => DataLayer = new DataLayer());
            AddProductReviewCommand = new DelegateCommand(AddProductReview);
            DeleteProductReviewCommand = new DelegateCommand(DeleteProductReview);
            UpdateProductReviewCommand = new DelegateCommand(EditProductReview);
        }

        private void EditProductReview()
        {
            Task.Run(() => { DataRepository.UpdateProductReview(currentReview); });
        }

        private void DeleteProductReview()
        {
            Task.Run(() => { DataRepository.DeleteReviewById(currentReview.ProductReviewID); });
            reviews.Remove(currentReview);
        }

        private void AddProductReview()
        {
            if (newReviewerName.Length <= 50 && newEmailAddress.Length <= 50 && newComments.Length <= 3850 &&
                newRating >= 1 && newRating <= 5 && DataRepository.IsProductIdValid(newProductID))
            {
                ProductReview pr = new ProductReview()
                {
                    ProductID = newProductID,
                    ReviewerName = newReviewerName,
                    ReviewDate = DateTime.Now,
                    EmailAddress = newEmailAddress,
                    Rating = newRating,
                    Comments = newComments,
                    ModifiedDate = DateTime.Now
                };
                reviews.Add(pr);
                Task.Run(() => { DataRepository.CreateReview(pr); });
            }
            else MessageBox.Show("Podane dane nie mogą zostać przekazane do bazy danych", "Błąd");
        }
    }
}
