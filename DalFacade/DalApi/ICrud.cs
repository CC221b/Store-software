using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        public int Add(T t);
        public void Delete(int id);
        public void Update(T t);
        public T Get(int ID);
        public IEnumerator<T> GetAll();
       

    }
}
