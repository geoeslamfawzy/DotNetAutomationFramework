using Newtonsoft.Json.Linq;
using ServoFramework.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServoFramework.Helpers
{
    public class JsonHelper 
    {
        UIActions ui = new UIActions();
        public JsonHelper()
        {
            
        }
        public string ExtractData(string tokenName)
        {
            JToken jsonObject = ui.ReadDataFile("loginData.Json");
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] ExtractDataArray(string tokenName)
        {
            List<String> productList = ui.ReadDataFile("loginData.Json").SelectTokens(tokenName).Values<string>().ToList();
            return productList.ToArray();
        }

        public static JsonHelper getDataParser()
        {
            return new JsonHelper();
        }
    }
}
