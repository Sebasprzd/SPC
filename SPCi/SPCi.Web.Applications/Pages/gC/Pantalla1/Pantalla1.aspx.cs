﻿using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

public partial class Pantalla1 : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<script>alert('Script de redirección está ejecutándose.'); setTimeout(function() { window.location.href = '/Default.aspx'; }, 5000);</script>");


    }
}

