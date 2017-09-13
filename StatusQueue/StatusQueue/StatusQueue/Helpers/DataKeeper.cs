using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusQueue.Helpers
{
    static public class DataKeeper
    {
        private const string selectedPost = "SelectedPost";

        public static void SaveSelectedPost(string id)
        {
            CrossSettings.Current.AddOrUpdateValue(selectedPost, id);
        }

        public static string LoadSelectedPost() => CrossSettings.Current.GetValueOrDefault(selectedPost,string.Empty);
    }
}
