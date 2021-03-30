using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.ImagenesSlider
{
    public class ImagenesSliderModel
    {

        public long PK { get; set; }
        public string IMAGEN { get; set; }
        public bool BORRADO{ get; set; }
        private database db;
        public ImagenesSliderModel() {
            db = new database();
        }
        public List<ImagenesSliderModel> getList() {
            List<ImagenesSliderModel> imagenes = new List<ImagenesSliderModel>();
            ImagenesSliderModel aux;
            try
            {
                string sql = "SELECT * FROM IMAGENES_SLIDER WHERE BORRADO=0";
                db.PreparedSQL(sql);
                ResultSet res = db.getTable();
                while (res.Next())
                {
                    aux = new ImagenesSliderModel();
                    aux.PK = res.GetLong("PK");
                    aux.IMAGEN = res.Get("IMAGEN");
                    imagenes.Add(aux);
                }
            }
            catch (Exception e) {
                LogModel.registra("Error al obtener imagenes ImagenesSliderModel->getList", e.ToString());
            }

            return imagenes;
        }

    }
}
