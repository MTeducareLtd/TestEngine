using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Customer
/// </summary>
public class Customer
{
	public Customer()
	{
		
	}
    public Customer(string custname, string custlastmae, int sal)
    {
        custname = Cust_Name;
        custlastmae = Cust_LastName;
        sal = Sal;
    }

    private string _Cust_Name;
    private string _Cust_LastName;
    private int _sal;

    public int Sal
    {
        get { return _sal; }
        set { _sal = value; }
    }

    public string Cust_LastName
    {
        get { return _Cust_LastName; }
        set { _Cust_LastName = value; }
    }

    public string Cust_Name
    {
        get { return _Cust_Name; }
        set { _Cust_Name = value; }
    }

    
}