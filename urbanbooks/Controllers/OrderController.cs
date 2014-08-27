using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Details(int orderId)
        {
            order = new Order();
            order = myHandler.GetOrder(orderId);
            List<OrderItem> itemList = myHandler.GetOrderItemsList();
            ViewBag.Order = order;
            return View(itemList);
        }

        public ActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> AddOrderItems(OrderItem item)
        {
            List<Order> myOrderList = new List<Order>();
          //  await order = myHandler.GetOrdersList().Single(ord => ord.DataModified == DateTime.Now);
            TryUpdateModel(item);
            myHandler = new BusinessLogicHandler();
            myHandler.AddOrderItem(item);
            
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            myHandler = new BusinessLogicHandler();
            order = new Order();
            item = new OrderItem();
            TryUpdateModel(order);
            try
            {
                if (ModelState.IsValid)
                {
                    myHandler.AddOrder(order);
                    await AddOrderItems(item);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
