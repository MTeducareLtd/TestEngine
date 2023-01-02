using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SelectCust
/// </summary>
public class SelectCust
{
	public SelectCust()
	{
		
	}

    public void selectCustList()
    {
        List<Customer> cust = new List<Customer>();
        cust.Add(new Customer("jay", "c", 1));
        cust.Add(new Customer("PC", "PC", 2));
        cust.Add(new Customer("ds", "sd", 2));
    }

}