using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataManagement
{

    public class DataManager : MonoBehaviour
    {
        public static DataManager instance = null;

        private JsonSerializer serializer;

        private List<JsonObject> loadedObjects;

        private int productListId = -1;
        private int productAmountListId = -1;
        public bool Ready
        {
            get;
            private set;
        }

        private void Awake()
        {
            if (instance != null)
                DestroyImmediate(this.gameObject);
            else
            {
                instance = this;
            }

            serializer = new JsonSerializer();
            loadedObjects = new List<JsonObject>();

            CreateProductList();
            CreateAmountList();
           
            Ready = true;
        }

        private void CreateProductList()
        {
            ProductList productList = serializer.LoadData<ProductList>(ProductList.location);
            if (productList == null)
                productList = new ProductList();
            loadedObjects.Add(productList);
            productListId = loadedObjects.IndexOf(productList);
            Debug.Log(productList);
        }

        private void CreateAmountList()
        {
            ProductAmountList productAmountList = serializer.LoadData<ProductAmountList>(ProductAmountList.location);
            if (productAmountList == null)
                productAmountList = new ProductAmountList();
            loadedObjects.Add(productAmountList);
            productAmountListId = loadedObjects.IndexOf(productAmountList);
            Debug.Log(productAmountList);
        }


        private void SaveAll()
        {
            if (loadedObjects != null)
            {
                foreach (JsonObject jsonObject in loadedObjects)
                {
                    serializer.SaveData(jsonObject);
                }
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                SaveAll();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                SaveAll();
            }
        }

        private void OnApplicationQuit()
        {
            SaveAll();
        }

        
        public JsonObjectList<T> GetList<T>() where T : JsonObject
        {
            JsonObjectList<T> jsonObjectList = (JsonObjectList<T>)loadedObjects.Where(x => typeof(JsonObjectList<T>).IsAssignableFrom(x.GetType())).First();
            return jsonObjectList;
        }


        public T AddItem<T>(T item) where T : JsonObject
        {
            JsonObjectList<T> list = GetList<T>();
            if (list != null)
            {
                item.id = list.GetUniqueID();
                list.list.Add(item);
            }
            return item;

        }

        public void EditItem<T>(T item) where T : JsonObject
        {
            JsonObjectList<T> list = GetList<T>();
            if(list != null)
            {
                list.list[list.list.FindIndex(x => x.id == item.id)] = item;
            }
        }

        public bool RemoveItem<T>(T item) where T : JsonObject
        {
            JsonObjectList<T> list = GetList<T>();
            if(list!= null)
            {
                list.list.Remove(item);
                return true;
            }
            return false;
        }
    }

}
