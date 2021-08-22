using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using day4.Model;
using day4.Repository;
using System.Threading.Tasks;

namespace day4.Service
{
    public class day4Service
    {

        day4Repository memberRepository = new day4Repository();
        public async Task<List<Person>> GetAsync()
        {
            day4Repository repo = new day4Repository();
            return await repo.GetAsync();
        }

        public async Task<List<MShip>> GetByIdAsync(int id)
        {
            day4Repository repo = new day4Repository();
            return await repo.GetByIdAsync(id);
        }

            //public async Task<bool> Postsync(int Id, [FromBody] int cijena)
         public async Task<bool> Postsync(int Id, int cijena)
         {
            day4Repository repo = new day4Repository();
            return await repo.PostAsync(Id, cijena);
        } 

        public async Task<bool> PutAsync(int OIB, string first_name, string last_name)
        {
            day4Repository repo = new day4Repository();
            return await repo.PutAsync(OIB, first_name, last_name);
        }

        public async Task<bool> DeleteAsync(int OIB)
        {
            day4Repository repo = new day4Repository();
            return await repo.DeleteAsync(OIB);
        }


    }
}
