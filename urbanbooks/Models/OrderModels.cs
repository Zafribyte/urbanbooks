using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace urbanbooks.Models
{
    public class OrderModels
    {
        BusinessLogicHandler myHandler;
        Order order;
        OrderItem orderItem;

        public void GetProductName(int productID) 
        {
            myHandler = new BusinessLogicHandler();
            IEnumerable<Book> books = myHandler.GetBooks();
            IEnumerable<Technology> tech = myHandler.GetTechnology();


                var bookTitle = from item in books
                                join bi in books on item.ProductID equals bi.ProductID
                                where item.ProductID == bi.ProductID
                                select (item.BookTitle);

                var modelName = from item in tech
                                join bi in tech on item.ProductID equals bi.ProductID
                                where item.ProductID == bi.ProductID
                                select (item.ModelName);           
        }
        //public IEnumerable<OrderItem> itemOrder(int orderNo) 
        //{
        //    myHandler = new BusinessLogicHandler();
        //    return (IEnumerable<OrderItem>) myHandler.GetOrderItemsList(orderNo);
        //}
 
    }

}