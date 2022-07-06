using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace nms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;
        private readonly IAgreementService _agreementService;
        private readonly IShoppingAgreementService _shoppingAgreementService;
        private readonly IShoppingDetailService _shoppingDetailService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ApplicationDbContext _context;

        

        public PayController(IShoppingService shoppingService, IAgreementService agreementService, IShoppingAgreementService shoppingAgreementService,
            IShoppingDetailService shoppingDetailService, IShoppingCartService shoppingCartService, ApplicationDbContext context)
        {
            _shoppingService = shoppingService;
            _agreementService = agreementService;
            _shoppingAgreementService = shoppingAgreementService;
            _shoppingDetailService = shoppingDetailService;
            _shoppingCartService = shoppingCartService;
            _context = context;
        }

        /// <summary>
        /// Pay 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(typeof(GenericResponseModel<PayDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponseModel<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponseModel<object>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Pay(string userId,string CCNo,string CCYear,string CCMonth,string CCV,IEnumerable<string> agreementIds)
        {
            var meta = new MetaData();
            var response = new GenericResponseModel<PayDto>();
            var payDetails = new PayDto();
            var user = _context.Set<User>().SingleOrDefault(x => String.Equals(x.Id, userId));

            if(user == null)
            {
                meta.Error = "User id is invalid";
                meta.Success = false;
                response.MetaData = meta;

                return NotFound(response);
            }

            var cartData = _shoppingCartService.GetByUserId(user.Id);
            
            if(cartData.Count() == 0 || cartData == null)
            {
                meta.Error = "Cart is empty";
                meta.Success = false;
                response.MetaData = meta;
                return NotFound(response);
            }

            var validatePay = PaySimulate(CCNo, CCYear, CCMonth, CCV);

            if (!validatePay)
            {
                meta.Error = "An error occured while paying. Please check credit card details.";
                meta.Success = false;
                response.MetaData = meta;
                return NotFound(response);

                return BadRequest(response);
            }

            var shopping =  _shoppingService.Add(new Shopping
            {
                UserId = user.Id,
                CreatedTime = DateTime.Now
            });

            if (shopping == null)
            {
                meta.Error = "An error occured in the database";
                meta.Success = false;
                response.MetaData = meta;
                
                return NotFound(response);
            }

            payDetails.ShoppingCreateResult = "Shopping added on the database";
            
            if(agreementIds.Count() == 0)
            {
                meta.Error = "There is no agreement has been accepted";
                meta.Success = false;
                response.MetaData = meta;
                
                return NotFound(response);
            }

            foreach(var agreementId in agreementIds)
            {
                await Task.Run(() => _shoppingAgreementService.Add(new ShoppingAgreement
                {
                    AgreementId = agreementId,
                    ShoppingId = shopping.Id.ToString()
                }));
            }

            payDetails.AggrementCreateResult = "Agreements added on the database";

            foreach (var cart in cartData)
            {
                await Task.Run(() => _shoppingDetailService.Add(new ShoppingDetail
                {
                    ShoppingId = shopping.Id.ToString(),
                    CourseId = cart.CourseId,
                }));
            }

            payDetails.ShoppingDetailResult = "Shopping details added on the database";
            
            foreach(var shoppingCart in cartData)
            {
                await Task.Run(() => _shoppingCartService.Remove(shoppingCart.Id));
            }

            meta.Success = true;
            response.MetaData = meta;
            response.Data = payDetails;
            
            
            return Ok(response);
        }

        private bool PaySimulate(string cCNo, string cCYear, string cCMonth, string cCV)
        {
            return true;
        }
    }
}
