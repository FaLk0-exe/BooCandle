using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooCandle
{
    public static class BooRepository
    {
        public static Boo boo = new Boo();
        public static List<Receipt> GetReceipts()
        {
                return boo.Receipt.Select(r => r).ToList();
        }

        public static bool IsReceiptExist(string name)
        {
            return boo.Receipt.Any(r => r.Name == name);
        }

        public static bool AddReceipt(string name,string description)
        {
            try
            {
                var receipt = new Receipt { Name = name, Description = description };
                boo.Receipt.Add(receipt);
                boo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteReceipt(string name)
        {
            if (!IsReceiptExist(name))
            {
                try
                {
                    var deleted = boo.Receipt.FirstOrDefault(r => r.Name == name);
                    if (deleted != null)
                    {
                        boo.Entry(deleted).State = System.Data.Entity.EntityState.Deleted;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static Receipt GetReceiptByName(string name)
        {
            return boo.Receipt.FirstOrDefault(r => r.Name == name);
        }

        public static bool EditMaterial(Material r)
        {
            try
            {
                boo.Entry(r).State = System.Data.Entity.EntityState.Modified;
                boo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EditReceipt(Receipt r)
        {
            try
            {
                boo.Entry(r).State = System.Data.Entity.EntityState.Modified;
                boo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<dynamic> GetMaterials()
        {
            return boo.Material.Select(m => new
            {
                Name = m.Name,
                MaterialType = m.MaterialType.Name
            }).ToList<dynamic>();
        }

        public static List<string> GetMaterialTypes()
        {
            return boo.MaterialType.Select(mt => mt.Name).ToList();
        }

        public static void AddMaterialType(MaterialType mt)
        {
            boo.MaterialType.Add(mt);
            boo.SaveChanges();
        }

        public static void AddMaterial(Material m)
        {
            boo.Material.Add(m);
            boo.SaveChanges();
        }

        public static MaterialType GetMaterialTypeByName(string name)
        {
            return boo.MaterialType.FirstOrDefault(mt => mt.Name == name);
        }

        public static Material GetMaterialByName(string name)
        {
            return boo.Material.FirstOrDefault(mt => mt.Name == name);
        }

        public static string[] GetOrderCodes()
        {
            return boo.Order.ToList().Where(o=>o.OrderStatus!="Завершено").Select(c => c.Id.ToString()).ToArray();
        }

        public static Order GetOrderByCode(int id)
        {
            return boo.Order.FirstOrDefault(o => o.Id == id);
        }

        public static List<string> GetOrderListById(int id)
        {
            return boo.Order.FirstOrDefault(o => o.Id == id).ListOrder.ToList().Select(l=>l.Candle.CandleCode+" "+
            (l.Candle.Price*l.Count).ToString("#.##") +"грн "+l.Count.ToString()).ToList();
        }

        public static bool ChangeOrderStatusById(int id,string status)
        {
            if(boo.Order.Any(o=>o.Id==id))
            {
                var order = boo.Order.FirstOrDefault(O => O.Id == id);
                order.OrderStatus = status;
                boo.Entry(order).State = System.Data.Entity.EntityState.Modified;
                boo.SaveChanges();
                return true;
            }
            return false;
        }

        public static void AddOrder(Order order)
        {
            boo.Order.Add(order);
            boo.SaveChanges();
        }

        public static void AddOrderList(ListOrder orderEl)
        {
            boo.ListOrder.Add(orderEl);
            boo.SaveChanges();
        }

 
        public static bool IsCandleExist()
        {
            return boo.Candle.Any();
        }

        public static int GetLastCandleId()
        {
            return boo.Candle.Last().Id;
        }

        public static void AddCandle(Candle candle)
        {
            boo.Candle.Add(candle);
            boo.SaveChanges();
        }
    }
}
