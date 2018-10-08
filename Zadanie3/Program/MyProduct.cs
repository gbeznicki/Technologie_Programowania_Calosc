using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class MyProduct
    {
        private int _ProductID;

        private string _Name;

        private string _ProductNumber;

        private bool _MakeFlag;

        private bool _FinishedGoodsFlag;

        private string _Color;

        private short _SafetyStockLevel;

        private short _ReorderPoint;

        private decimal _StandardCost;

        private decimal _ListPrice;

        private string _Size;

        private string _SizeUnitMeasureCode;

        private string _WeightUnitMeasureCode;

        private System.Nullable<decimal> _Weight;

        private int _DaysToManufacture;

        private string _ProductLine;

        private string _Class;

        private string _Style;

        private System.Nullable<int> _ProductSubcategoryID;

        private System.Nullable<int> _ProductModelID;

        private System.DateTime _SellStartDate;

        private System.Nullable<System.DateTime> _SellEndDate;

        private System.Nullable<System.DateTime> _DiscontinuedDate;

        private System.Guid _rowguid;

        private System.DateTime _ModifiedDate;

        public MyProduct(Product product)
        {
            this.ProductID = product.ProductID;
            this.Name = product.Name;
            this.ProductNumber = product.ProductNumber;
            this._MakeFlag = product.MakeFlag;
            this._FinishedGoodsFlag = product.FinishedGoodsFlag;
            this._Color = product.Color;
            this._SafetyStockLevel = product.SafetyStockLevel;
            this._ReorderPoint = product.ReorderPoint;
            this._StandardCost = product.StandardCost;
            this._ListPrice = product.ListPrice;
            this._Size = product.Size;
            this._SizeUnitMeasureCode = product.SizeUnitMeasureCode;
            this._WeightUnitMeasureCode = product.WeightUnitMeasureCode;
            this._Weight = product.Weight;
            this._DaysToManufacture = product.DaysToManufacture;
            this._ProductLine = product.ProductLine;
            this._Class = product.Class;
            this._Style = product.Style;
            this._ProductSubcategoryID = product.ProductSubcategoryID;
            this._ProductModelID = product.ProductModelID;
            this._SellStartDate = product.SellStartDate;
            this._SellEndDate = product.SellEndDate;
            this._DiscontinuedDate = product.DiscontinuedDate;
            this._rowguid = product.rowguid;
            this._ModifiedDate = product.ModifiedDate;
        }

        public int ProductID { get => _ProductID; set => _ProductID = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string ProductNumber { get => _ProductNumber; set => _ProductNumber = value; }
    }
}
