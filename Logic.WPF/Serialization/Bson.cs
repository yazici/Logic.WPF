﻿using Logic.Core;
using Logic.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Serialization
{
    public class Bson : IBinarySerializer
    {
        public byte[] Serialize<T>(T obj) where T : class
        {
            try
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    using (var writer = new BsonWriter(ms))
                    {
                        var serializer = new JsonSerializer()
                        {
                            TypeNameHandling = TypeNameHandling.Objects,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                        };
                        serializer.Serialize(writer, obj);
                    }
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Log.LogError("{0}{1}{2}",
                    ex.Message,
                    Environment.NewLine,
                    ex.StackTrace);
            }
            return null;
        }

        public T Deserialize<T>(byte[] bson) where T : class
        {
            try
            {
                using (var ms = new System.IO.MemoryStream(bson))
                {
                    using (BsonReader reader = new BsonReader(ms))
                    {
                        var serializer = new JsonSerializer()
                        {
                            TypeNameHandling = TypeNameHandling.Objects,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                            ContractResolver = new LogicContractResolver()
                        };
                        var page = serializer.Deserialize<T>(reader);
                        return page;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError("{0}{1}{2}",
                    ex.Message,
                    Environment.NewLine,
                    ex.StackTrace);
            }
            return null;
        }
    }
}
