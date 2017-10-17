using pressboard.cashregisterexam.model;
using pressboard.cashregisterexam.model.Interfaces.Services;
using System.Web.Mvc;

namespace pressboard.cashregisterexam.web.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderService orderService;
        private IDiscountService discountService;
        /// <summary>
        /// Constructor With dependency injection of OrderService and DiscountService
        /// </summary>
        /// <param name="orderService">OrderService Object</param>
        /// <param name="discountService">Discount Service Object</param>
        public OrdersController(IOrderService orderService, IDiscountService discountService)
        {
            this.orderService = orderService;
            this.discountService = discountService;
        }

        /// <summary>
        /// Get for Create a New Order
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new Order());
        }

        /// <summary>
        /// Posting the new order
        /// </summary>
        /// <param name="order">New Order to be posted</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
                orderService.createOrder(order.Key, order);
                return RedirectToAction("ShowOrderCreated", order);
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Call for Order Item
        /// Master-detail approach
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateOrderItem()
        {
            var orderItem = new OrderItem();
            return PartialView("~/Views/Shared/EditorTemplates/_OrderItem.cshtml", orderItem);
        }

        /// <summary>
        /// Showing the new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ActionResult ShowOrderCreated(Order order)
        {
            ViewBag.OnSale = false;
            order = orderService.getOrder(order.Key);
            return View(order);
        }

        /// <summary>
        /// Call for apply Coupon Discount
        /// </summary>
        /// <param name="key">Redis key</param>
        /// <returns></returns>
        public ActionResult ApplyCoupon(string key)
        {
            var order = discountService.ApplyCouponDiscount(key, 50, 5);
            return RedirectToAction("ShowOrderCreated", order);
        }

        /// <summary>
        /// Call for apply Bulk discount
        /// </summary>
        /// <param name="key">Redis key</param>
        /// <returns></returns>
        public ActionResult ApplyBulk(string key)
        {
            var order = orderService.getOrder(key);
            order = discountService.ApplyBulkDiscount(order);
            return RedirectToAction("ShowOrderCreated", order);
        }

    }
}
