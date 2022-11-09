using PorscheMarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.Domain.Interfaces
{
    public interface IBaseResponse<T>
    {
        //Feedback from the server.
        string Description { get; set; }
        //Status Code operation.
        public StatusCode StatusCode { get; set; }
        //Feedback data from the server.
        T Data { get; set; }
    }
}
