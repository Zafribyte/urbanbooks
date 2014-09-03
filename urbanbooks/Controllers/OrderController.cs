using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using urbanbooks.Models;
using System.Web.ModelBinding;

namespace urbanbooks.Controllers
{
    //[Authorize(Roles = "admin, employee")]
    public class OrderController : Controller
    {
        BusinessLogicHandler myHandler;
        Order order;
        OrderItem item;
        public ActionResult Index()
        {
            
            myHandler = new BusinessLogicHandler();
            List<Order> orderslist = myHandler.GetOrdersList();
            return View(orderslist);
        }
        public ActionResult Details(int orderNo)
        {
            //myHandler = new BusinessLogicHandler();
            //IEnumerable<OrderItem> orderItem = myHandler.GetOrderItemsList(orderNo);
            //IEnumerable<Order> order = myHandler.GetOrdersList();
            //IEnumerable<Book> bookOrder = myHandler.GetBooks();
            //IEnumerable<Technology> techOrder = myHandler.GetTechnology();

            //OrderViewModel viewOrder = new OrderViewModel();
            //viewOrder.books = bookOrder;
            //viewOrder.tech = techOrder;
            //viewOrder.orderItems = orderItem;
            //viewOrder.orders = order;

            //int count = 0;
            //Session["OrderNo"] = orderNo;                     
            ////List<OrderItem> itemList = myHandler.GetOrderItemsList(orderNo);
            return View();

        }


        public async Task<ActionResult> AddOrderItems(OrderItem item)
        {
            List<Order> myOrderList = new List<Order>();
            //await order = myHandler.GetOrdersList().Single(ord => ord.DataModified == DateTime.Now);
            TryUpdateModel(item);
            myHandler = new BusinessLogicHandler();
            myHandler.AddOrderItem(item);
            
            return Json(new { success = true });
        }
        
        

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                item = new OrderItem();
                order = new Order();
                myHandler.UpdateOrder(order);
                myHandler.UpdateOrderItem(item);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
