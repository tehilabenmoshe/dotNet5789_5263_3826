﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
public interface IBoProduct
{
    public IEnumerable<ProductForList> getProductForList();
    public Product GetProductbyIdForManager(int ID); //

    public IEnumerable<ProductItem> GetProductItemListForCustomer();
    public ProductItem GetProductByIDAndCartForCostumer(int ID, Cart cart); //
    public void AddProduct(Product product); //
    public void DeledeProduct(int ID);
    public void UpdateDetailProduct(BO.Product? p);
    internal DO.Product ProductFromBOToDO(BO.Product p); //casting from bo to do

    public IEnumerable<ProductForList> FilterProductList(Predicate<ProductForList> tmp); //filter the list by category
    public IEnumerable<ProductItem> FilterProducItemtList(Predicate<ProductItem> tmp); //filter the list of the ProductItem by category
}
