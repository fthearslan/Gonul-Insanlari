using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class VideoManager : IVideoService
    {
        IVideoDAL _video;

        public VideoManager(IVideoDAL video)
        {
            _video = video;
        }

        public void Add(Video entity)
        {
            _video.Insert(entity);
        }

        public void Delete(Video entity)
        {
            _video.Delete(entity);
        }

        public Video GetById(int id)
        {
            return _video.Get(x => x.VideoID == id);
        }

        public List<Video> List()
        {
            return _video.List();
        }

        public List<Video> ListFilter()
        {
            return _video.ListFilter(x => x.Status == true);
        }

        public void Update(Video entity)
        {
            _video.Update(entity);
        }
    }
}
