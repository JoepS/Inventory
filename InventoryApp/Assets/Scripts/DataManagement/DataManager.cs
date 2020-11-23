using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{

    public class DataManager : MonoBehaviour
    {
        public static DataManager instance = null;

        private JsonSerializer serializer;

        private List<JsonObject> loadedObjects;

        private int productListId = -1;
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

            ProductList productList = serializer.LoadData<ProductList>(ProductList.location);
            if (productList == null)
                productList = new ProductList();
            loadedObjects.Add(productList);
            productListId = loadedObjects.IndexOf(productList);
            Debug.Log(productList);
            Ready = true;
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

        public ProductList GetProductList()
        {
            if(productListId >= 0)
                return (ProductList)loadedObjects[productListId];
            return null;
        }

        public void AddProduct(Product product)
        {
            if(productListId >= 0)
            {
                product.id = GetProductList().products.Count + 1;
                GetProductList().products.Add(product);
            }
        }
    }

}
