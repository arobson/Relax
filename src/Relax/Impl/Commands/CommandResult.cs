﻿using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class CommandResult
    {
        public string Json { get; set; }

        public JObject JsonObject
        {
            get
            {
                if(!string.IsNullOrEmpty(Json))
                {
                    return JObject.Parse(Json);
                }
                return new JObject();
            }
        }

        public TResult GetResultAs<TResult>()
        {
            if (string.IsNullOrEmpty(Json))
                return default(TResult);

            return Json.FromJson<TResult>();
        }

        public CommandResult(string json)
        {
            Json = FilterOutDesignDocuments(json);
        }

        public virtual string FilterOutDesignDocuments(string json)
        {
            if(json == null)
            {
                return "";    
            }

            try
            {
                var jsonDoc = JObject.Parse(json);
                if(jsonDoc["rows"] != null)
                    jsonDoc["rows"]
                        .Children()
                        .Where(x => x["doc"]["_id"].ToString().StartsWith(@"""_design"))
                        .ForEach(x => x.Remove());
                return jsonDoc.ToString();
            }
            catch (Exception e)
            {
                return json;
            }
        }
    }
}