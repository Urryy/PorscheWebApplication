using PorscheMarket.Domain.Enum;
using PorscheMarket.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.Domain.Response
{
    public class BaseResponse<T>:IBaseResponse<T>
    {
        //Feedback from the server.
        public string Description{ get; set; }
        //Status Code operation.
        public StatusCode StatusCode { get; set; }
        //Feedback data from the server.
        public T Data { get; set; }
        
    }
}
