<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data.SqlServerCe;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;
using System.Collections.Generic;

public class Handler : IHttpHandler {


    string con = @"server=LENOVO-PC\SA;uid=sa;pwd=123;database=1mutong2";
    DataSet ds = new DataSet();
    SqlCeDataAdapter da = new SqlCeDataAdapter();
    JavaScriptSerializer jsS = new JavaScriptSerializer();
    List<object> lists = new List<object>();
    string result = "";
    
    public void ProcessRequest (HttpContext context) {


        string command = context.Request["cmd"];


        switch (command)
        {
            case "pie":
                GetPie(context);
                break;
            case "bar":
                GetBars(context);
                break;
            case "line":
                GetLine(context);
                break;
        };

    }


    public void GetPie(HttpContext context)
        {
            string sql = @"select fname as name,jiweig as count from JobFeedList where jid=1 ";
            ds = GetData(sql);
           
            lists = new List<object>(); 
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var obj = new { name = dr["name"], value = dr["count"] };
                lists.Add(obj); 
            } 

            jsS = new JavaScriptSerializer(); 
            result = jsS.Serialize(lists); 
            context.Response.Write(result);
        
        }



    public void GetBars(HttpContext context)
        {
           string sql = @"  select CONVERT(NVARCHAR(10),createdate,120) as date,   count(*) as count from lists  
group by CONVERT(NVARCHAR(10),createdate,120) ";
            ds = GetData(sql);
            lists = new List<object>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var obj = new { name = dr["date"], value = dr["count"] };
                lists.Add(obj);

            } 
        
            jsS = new JavaScriptSerializer();
            result = jsS.Serialize(lists);
            context.Response.Write(result);
        }

    public void GetLine(HttpContext context)
    {
        string sql = @"  select CONVERT(NVARCHAR(10),createdate,120) as date,   count(*) as count from lists  
group by CONVERT(NVARCHAR(10),createdate,120) ";
        ds = GetData(sql);
        lists = new List<object>();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            var obj = new { name = dr["date"], value = dr["count"] };
            lists.Add(obj);

        }

        jsS = new JavaScriptSerializer();
        result = jsS.Serialize(lists);
        
        
        context.Response.Write(result);
    }

    public DataSet GetData(string sql)
    { 
        ds = new DataSet();
        da = new SqlCeDataAdapter(sql, con);
        da.Fill(ds);
        return ds;
    
    }
    
    
    
    
    
    
    
    
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}