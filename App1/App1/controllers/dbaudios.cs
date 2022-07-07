using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using App1.models;
using System.Threading.Tasks;

namespace App1.controllers
{
    public class dbaudios
    {
        readonly SQLiteAsyncConnection db;

        public dbaudios(string pathdb)
        {
            //auto completado con tab
            db = new SQLiteAsyncConnection(pathdb);
            //creamos nuestra tabla en la base de datos
            db.CreateTableAsync<audioclass>().Wait();
        }





        //hacemos un public para el listado con la db
        public Task<List<audioclass>> listaaudios()
        {
            return db.Table<audioclass>().ToListAsync();
        }


        //lista de audio por id
        public Task<audioclass>audiosobt(int pid)
        {
            //where sirve para filtrar por codigo id
            return db.Table<audioclass>()
                .Where(i => i.id == pid).FirstOrDefaultAsync(); 
        }








        //hacemos un public para que inserte el audio a la db
        public Task<int>recordingaudio(audioclass audi)
        {
            //comparamos si el id existe
            if(audi.id != 0)
            {
                return db.UpdateAsync(audi);

            }
            //caso contratio si no existe lo agrega
            else
            {
                return db.InsertAsync(audi);
            }
        }




        //hacemos un siguiente public  para eliminar
        public Task<int>eliminarrecording(audioclass audi)
        {

            return db.DeleteAsync(audi);
        }

    }

}
