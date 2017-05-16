using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VK_Analyze.Models;
using VkNet;
using System.Diagnostics;
using VK_Analyze.Controllers.supportFunction;

namespace VK_Analyze.Controllers
{
    public class WorkerController : Controller
    {

        const float COUNT_MILLISECOND = 1000;
        const int MAX_REQUEST = 2;
        const float TIME_ONE_REQUEST = COUNT_MILLISECOND / MAX_REQUEST;

        // GET: Worker
        public ActionResult PageWorker()
        {
            VkApi vk = (VkApi)Session["VkApi"];
            List<VkNet.Model.Post> posts = GetPostsUser(vk);

            Dictionary<long, long> likesUser = GetLikes(vk, posts);

            List<UserInfo_Likes> likesUserWithName = new List<UserInfo_Likes>();

            TaskFactory taskFactory = new TaskFactory();
            List<Task> tasks = new List<Task>();
            foreach (long userID in likesUser.Keys)
            {
                Task taskAdd =  taskFactory.StartNew(
                    () =>
                    {
                        VkNet.Model.User user = functions.VkAccount.GetAccountInfo(vk, userID.ToString());
                        string shortName = $"{user.FirstName} {user.LastName}";
                        likesUserWithName.Add(new UserInfo_Likes(shortName, user.Id, likesUser[userID]));
                            });
                tasks.Add(taskAdd);
            }
            Task.WaitAll(tasks.ToArray());
            likesUserWithName = (from x in likesUserWithName orderby x.Count select x).ToList();
            ViewBag.likesUser = likesUserWithName;

            return View();
        }

        private static Dictionary<long, long> GetLikes(VkApi vk, List<VkNet.Model.Post> posts)
        {
            VkNet.Model.RequestParams.LikesGetListParams paramsGetLikes = CreateDefaultParamsGetLikes();
            Dictionary<long, long> likesUser = new Dictionary<long, long>();
            List<long> likesList = new List<long>();
            Stopwatch stopWatch = new Stopwatch();

            for (int i = 0; i < posts.Count; i++)
            {
                paramsGetLikes.ItemId = posts[i].Id.Value;
                WaitOffset(stopWatch);
                likesList = vk.Likes.GetList(paramsGetLikes).ToList();
                foreach (long userID in likesList)
                {
                    AddLikesForlikesUser(likesUser, userID);
                }

            }

            return likesUser;
        }

        private static void AddLikesForlikesUser(Dictionary<long, long> likesUser, long userID)
        {
            if (!likesUser.ContainsKey(userID))
            {
                likesUser.Add(userID, 0);
            }
            likesUser[userID]++;
        }

        private static void WaitOffset(Stopwatch stopWatch)
        {
            int millisecondElapsed = Convert.ToInt32(TIME_ONE_REQUEST - stopWatch.ElapsedMilliseconds);
            if (millisecondElapsed > 0)
            {
                Thread.Sleep(millisecondElapsed);

            }
            stopWatch.Restart();
        }

        private static List<VkNet.Model.Post> GetPostsUser(VkApi vk, int id = 0)
        {
            VkNet.Model.RequestParams.WallGetParams wall = GetParamsWallPosts(id);
            List<VkNet.Model.Post> posts = null;
            posts = vk.Wall.Get(wall).WallPosts.ToList();
            return posts;
        }

        private static VkNet.Model.RequestParams.LikesGetListParams CreateDefaultParamsGetLikes()
        {
            VkNet.Model.RequestParams.LikesGetListParams paramsGetLikes = new VkNet.Model.RequestParams.LikesGetListParams();
            paramsGetLikes.Extended = true;
            paramsGetLikes.Count = int.MaxValue;
            paramsGetLikes.SkipOwn = true;
            paramsGetLikes.Type = VkNet.Enums.SafetyEnums.LikeObjectType.Post;
            return paramsGetLikes;
        }

        private static VkNet.Model.RequestParams.WallGetParams GetParamsWallPosts(int id = 0)
        {
            VkNet.Model.RequestParams.WallGetParams wall = new VkNet.Model.RequestParams.WallGetParams();
            wall.Count = 100;
            if (id != 0)
            {
                wall.OwnerId = id;
            }
            return wall;
        }

        [HttpPost]
        public ActionResult PageWorker(UserFriendsView model)
        {

            return View();
        }
    }
}