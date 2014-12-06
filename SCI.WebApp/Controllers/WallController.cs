using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using SCI.BL.Entities;

namespace SCI.WebApp.Controllers
{
    public class WallController : ApiController
    {
        private static readonly List<WallEntry> _repo = GetStartingRepo();
        private static List<WallEntry> GetStartingRepo()
        {
            return Enumerable.Range(1, 5).Select<int, WallEntry>(q => GetTextWallEntry())
                .Concat(Enumerable.Range(1, 3).Select(q => GetImageWallEntry())).ToList();
        }
        private static ImageWallEntry GetImageWallEntry()
        {
            return new ImageWallEntry
            {
                Author = "WebApp",
                TimeStamp = DateTime.Now,
                Id = Guid.NewGuid(),
                Title = "SampleImage",
                Base64Content = GetImage()
            };
        }
        private static string GetImage()
        {
            var virtualPath = "~/Assets/1.png";
            var image = File.OpenRead(System.Web.Hosting.HostingEnvironment.MapPath(virtualPath));
            var bytes = new byte[image.Length];
            image.ReadAsync(bytes, 0, (int) image.Length).Wait();
            return Convert.ToBase64String(bytes);
        }
        private static TextWallEntry GetTextWallEntry()
        {
            return new TextWallEntry
            {
                Author = "WebApp",
                Content = "Some text",
                Id = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
            };
        }

        public IEnumerable<WallEntry> Get()
        {
            lock (_repo)
            {
                return _repo;
            }
        }

        public void Post(WallEntry wallEntry)
        {
            DiscardNullObject(wallEntry);
            lock (_repo)
            {
                _repo.Add(wallEntry);
            }
        }
        
        public void Put(WallEntry wallEntry)
        {
            DiscardNullObject(wallEntry);
            lock (_repo)
            {
                CheckIfExistsInRepo(wallEntry.Id);
                ReplaceIitemsInRepo(wallEntry);
            }
        }

        public void Delete(Guid id)
        {
            lock (_repo)
            {
                CheckIfExistsInRepo(id);
                _repo.RemoveAll(q => q.Id == id);
            }
        }

        private void DiscardNullObject(object obj)
        {
            if (obj == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
        private void CheckIfExistsInRepo(Guid id)
        {
            if(_repo.All(q => q.Id != id))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
        private void ReplaceIitemsInRepo(WallEntry wallEntry)
        {
            var indexes = _repo.Where(q => q.Id == wallEntry.Id).Select(q => _repo.IndexOf(q)).ToArray();
            foreach (var index in indexes)
            {
                _repo[index] = wallEntry;
            }
        }
    }
}
