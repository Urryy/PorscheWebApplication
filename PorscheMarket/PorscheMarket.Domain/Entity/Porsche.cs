using Microsoft.AspNetCore.Http;
using PorscheMarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.Domain.Entity
{
    public class Porsche
    {
        //Variables
        //FullNameModel: Name + Model.
        private string fullNameModel;

        //Propirties
        public int Id { get; set; }
        //Model Porsche.
        public string Name { get; set; }
        //Equipment Porsche.
        public string Model { get; set; }
        //FullNameModel: Name + Model.
        public string FullNameModel 
        {
            get{return this.fullNameModel;} 
            set{ this.fullNameModel = Name + Model;}
        }
        //Description Porsche.
        public string Description { get; set; }
        //Price Porsche.
        public int Price { get; set; }
        //Maximum speed Porsche.
        public int MaxSpeed { get; set; }
        //Date Create Porsche.
        public DateTime CreateDate { get; set; }
        //Enum BodyType Porsche(Universal,Sedan...).
        public BodyType BodyType { get; set; }
        //Image Porsche in Bytes.
        public byte[]? ImgPorsche { get; set; }
        //Image Porsche in String.
        public string ImgString { get; set; }
    }
}
