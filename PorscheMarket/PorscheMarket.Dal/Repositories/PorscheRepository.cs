using Microsoft.EntityFrameworkCore;
using PorscheMarket.Dal.Interfaces;
using PorscheMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorscheMarket.Dal.Repositories
{
    public class PorscheRepository : IBaseRepository<Porsche>
    {
        //Db Context
        private readonly ApplicationDbContext _db;
        //Construktor
        public PorscheRepository(ApplicationDbContext db)
        {
            _db = db;
                     
            if (_db.Porsches.Count()==0)
            {
                Porsche p1 = new Porsche
                {
                    Name = "Panamera",
                    Model = "GTS Sport",
                    Description = "С типичной для Porsche динамикой и одновременно высокой экономичностью? Невозможно – говорили многие. Смело – утверждали другие.",
                    Price = 195000,
                    MaxSpeed = 320,
                    CreateDate = new DateTime(2022, 7, 20),
                    BodyType = Domain.Enum.BodyType.SportSedan,
                    ImgPorsche = new byte[17],
                    ImgString = "https://phonoteka.org/uploads/posts/2021-06/1624512846_13-phonoteka_org-p-porshe-panamera-oboi-krasivo-14.jpg"
                };
                Porsche p2 = new Porsche
                {
                    Name = "Macan",
                    Model = "GTS Sport",
                    Description = "С типичной для Porsche динамикой и одновременно высокой экономичностью? Невозможно – говорили многие. Смело – утверждали другие.",
                    Price = 195000,
                    MaxSpeed = 320,
                    CreateDate = new DateTime(2022, 7, 20),
                    BodyType = Domain.Enum.BodyType.Universal,
                    ImgPorsche = new byte[17],
                    ImgString = "https://get.wallhere.com/photo/1920x1200-px-blue-blue-cars-car-Porsche-Porsche-Macan-744490.jpg"
                };
                Porsche p3 = new Porsche
                {
                    Name = "Taycan",
                    Model = "GTS Sport",
                    Description = "С типичной для Porsche динамикой и одновременно высокой экономичностью? Невозможно – говорили многие. Смело – утверждали другие.",
                    Price = 195000,
                    MaxSpeed = 320,
                    CreateDate = new DateTime(2022, 7, 20),
                    BodyType = Domain.Enum.BodyType.SportSedan,
                    ImgPorsche = new byte[17],
                    ImgString= "https://images3.alphacoders.com/106/thumb-1920-1068167.jpg"
                };
                Porsche p4 = new Porsche
                {
                    Name = "911",
                    Model = "GTS Sport",
                    Description = "С типичной для Porsche динамикой и одновременно высокой экономичностью? Невозможно – говорили многие. Смело – утверждали другие.",
                    Price = 195000,
                    MaxSpeed = 320,
                    CreateDate = new DateTime(2022, 7, 20),
                    BodyType = Domain.Enum.BodyType.Coupe,
                    ImgPorsche = new byte[17],
                    ImgString= "https://pinterest.ru.com/images/2018/10/15/porsche-911-2.jpg"
                };
                Porsche p5 = new Porsche
                {
                    Name = "Cayenne",
                    Model = "GT",
                    Description = "С типичной для Porsche динамикой и одновременно высокой экономичностью? Невозможно – говорили многие. Смело – утверждали другие.",
                    Price = 125000,
                    MaxSpeed = 300,
                    CreateDate = new DateTime(2022, 7, 20),
                    BodyType = Domain.Enum.BodyType.OffRoad,
                    ImgPorsche = new byte[17],
                    ImgString = "https://get.wallhere.com/photo/car-vehicle-Porsche-Porsche-Cayenne-wheel-1920x1200-px-land-vehicle-automotive-design-automotive-exterior-automobile-make-luxury-vehicle-sport-utility-vehicle-compact-sport-utility-vehicle-695854.jpg"
                };
                Porsche p6 = new Porsche
                {
                    Name = "718",
                    Model = "Spyder",
                    Description = "С типичной для Porsche динамикой и одновременно высокой экономичностью? Невозможно – говорили многие. Смело – утверждали другие.",
                    Price = 115000,
                    MaxSpeed = 260,
                    CreateDate = new DateTime(2022, 7, 20),
                    BodyType = Domain.Enum.BodyType.Coupe,
                    ImgPorsche = new byte[17],
                    ImgString = "https://get.wallhere.com/photo/1920x1200-px-car-Porsche-Porsche-Boxster-GTS-red-cars-748578.jpg"
                };
                _db.Porsches.AddRange(p1,p2,p3,p4,p5,p6);
                _db.SaveChanges();
            }          
        }
        //Create Entity Porsche
        public async Task<bool> Create(Porsche entity)
        {
            await _db.Porsches.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true; 
        }
        //Delete Entity Porsche
        public async Task<bool> Delete(Porsche entity)
        {
            _db.Porsches.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        //Getting All Entities Porsches
        public IQueryable<Porsche> SelectAll()
        {     
            return _db.Porsches;
        }
        //Update Entity Porsche
        public async Task<bool> Update(Porsche entity)
        {
            _db.Porsches.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
