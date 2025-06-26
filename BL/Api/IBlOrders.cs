using BL.Models;

namespace BL.Api
{
    public interface IBlOrders

    {
        //get
        List<BlOrder> GetOrders();

        //getByDate
        //Task<int> CreateOrderAsync(BlOrder order);

        List<BlOrder> GetByDate(DateTime d);


       BlOrder GetOrdersById(int id);
        //getByDateModel
        int GetCountModelByDateAndSize(DateTime date, int model, int size);
        BlOrder GetById(int id);
        //isUpdate
        //public int isUpdate(DateTime date, int model, int size, int count);
        //update
        void add(BlOrder item);
        //delete
        void delete(int id);
        //edit
        public void edit(BlOrder? item);

    }


}
