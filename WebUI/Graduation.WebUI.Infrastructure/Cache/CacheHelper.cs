using graduation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduation.WebUI.Infrastructure.Cache
{
    public class CacheHelper
    {
        ICache cache;
        CategoryData _categoryData;
        AuthorData _authorData;
        SettingData _settingData;
        RolePageData _rolePageData;
        ContentData _contentData;
        public CacheHelper(ICache cache, CategoryData categoryData,
            AuthorData _authorData, SettingData _settingData, RolePageData rolePageData, ContentData contentData)
        {
            this.cache = cache;
            _categoryData = categoryData;
            this._authorData = _authorData;
            this._settingData = _settingData;
            _rolePageData = rolePageData;
            _contentData = contentData;
        }
        private string Categories_CacheKey = "Categories CacheKey";
        public bool CategoriesClear() { return Clear(Categories_CacheKey); }
        public List<graduation.Model.Category> Categories
        {
            get
            {
                var fromCache = Get<List<graduation.Model.Category>>(Categories_CacheKey);
                if (fromCache == null)
                {
                    var datas = _categoryData.GetBy(x => !x.IsDelete);
                    if (datas != null && datas.Count() > 0)
                    {
                        Set(Categories_CacheKey, datas);
                        fromCache = datas;
                    }
                }
                return fromCache;
            }
        }
    

        private string Author_CacheKey = "Author_CacheKey";
        public bool AuthorsClear() { return Clear(Author_CacheKey); }
        public List<graduation.Model.Author> Authors
        {
            get
            {
                var fromCache = Get<List<graduation.Model.Author>>(Author_CacheKey);
                if (fromCache == null)
                {
                    var datas = _authorData.GetBy(x => !x.IsDeleted);
                    if (datas != null && datas.Count() > 0)
                    {
                        Set(Author_CacheKey, datas);
                        fromCache = datas;
                    }
                }
                return fromCache;
            }
        }



        private string Setting_CacheKey = "Setting_CacheKey";
        public bool SettingClear() { return Clear(Setting_CacheKey); }
        public graduation.Model.Setting Setting
        {
            get
            {
                var fromCache = Get<graduation.Model.Setting>(Setting_CacheKey);
                if (fromCache == null)
                {
                    var datas = _settingData.GetAll().FirstOrDefault();
                    if (datas != null )
                    {
                        Set(Setting_CacheKey, datas);
                        fromCache = datas;
                    }
                }
                return fromCache;
            }
        }



        private string RolePage_CacheKey = "RolePage_CacheKey";
        public bool RolePageClear() { return Clear(RolePage_CacheKey); }
        public List<graduation.Model.RolePage> RolePages(int roleId)
        {
            
                var fromCache = Get<List<graduation.Model.RolePage>>(RolePage_CacheKey);
                if (fromCache == null)
                {
                    var datas = _rolePageData.GetAll();
                    if (datas != null)
                    {
                        Set(RolePage_CacheKey, datas);
                        fromCache = datas;
                    }
                }
                if(fromCache != null)
            {
                return fromCache.Where(x => x.RoleId == roleId).ToList();
            }
                return new List<graduation.Model.RolePage>();
            }


        private string NewContents_CacheKey = "NewContents_CacheKey";
        public bool NewContentsClear() { return Clear(NewContents_CacheKey); }
        public List<graduation.Model.Content> NewContents
        {
            get
            {
                var fromCache = Get<List<graduation.Model.Content>>(NewContents_CacheKey);
                if (fromCache == null)
                {
                    var datas = _contentData.GetBlogNewContents(25);
                    if (datas != null && datas.Count() > 0)
                    {
                        Set(NewContents_CacheKey, datas);
                        fromCache = datas;
                    }
                }
                return fromCache;
            }
        }

        public bool Clear(string name)
        {
            cache.Remove(name);
            return true;
        }
        public T Get<T>(string cacheKey) where T : class
        {
            object cookies;
            if (!cache.TryGetValue(cacheKey, out cookies))
            
                return null;
            

            return cookies as T;
        }
        public void Set(string cacheKey, object value)
        {
            cache.Set(cacheKey, value, 180);
        }
    }
}
