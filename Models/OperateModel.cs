using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;


namespace WMS_API.Models
{
    public class OperateModel
    {
        DBAgentPos mPos = null;

        public OperateModel(IConfiguration configuration)
        {
            mPos = new DBAgentPos(configuration);
        }

        public dynamic AddOrder()
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
            return null;
        }

    }

    public class picking_infos
    {
        public List<info> info = new List<info>();
        //public string updatetime{set;get;}
    }

    public class info
    {
        public string number{set;get;}
        public string datetime{set;get;}
        public string orderid{set;get;}
        public string type{set;get;}
        public string applicant{set;get;}
        public string status{set;get;}

    }

    public class return_infos
    {
        public List<info> info = new List<info>();
        //public string updatetime{set;get;}
    }

    public class appliedorder
    {
        public string applicant{set;get;}
        public string machinecode{set;get;}
        public string date{set;get;}
        public string orderid{set;get;}
        public List<orderitem> orderitems = new List<orderitem>();

    }

    public class orderitem
    {
        public string number{set;get;}
        public string productionname{set;get;}
        public string productionnumber{set;get;}
        public string count{set;get;}
    }
}