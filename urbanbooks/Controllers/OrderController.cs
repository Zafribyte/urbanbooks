using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using urbanbooks.Models;

namespace urbanbooks.Controllers
{
    [Authorize(Roles = "admin, employee")]
    public class OrderController : Controller
    {
        BusinessLogicHandler myHandler;
        Order order;
        OrderItem item;
        public ActionResult Index()
        {
            #region Prep Utilities

            OrderModels model = new OrderModels();
            myHandler = new BusinessLogicHandler();

            #endregion

            #region Get Orders

            model.AllOrders = myHandler.GetOrdersList();
            model.Pending = myHandler.GetAllPendingOrders();
            model.Completed = myHandler.GetAllCompletedOrders();

            #endregion

            return View(model);
        }

        public ActionResult Details(int orderNo)
        {
            #region Prep Utilities

            myHandler = new BusinessLogicHandler();

            #endregion

            #region Get Order Details
            #endregion

            List<OrderItem> itemList = myHandler.GetOrderItemsList(orderNo);
            return View(itemList);
        }


        public ActionResult AddOrderItems(OrderItem item)
        {
            List<Order> myOrderList = new List<Order>();
          //  await order = myHandler.GetOrdersList().Single(ord => ord.DataModified == DateTime.Now);
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
