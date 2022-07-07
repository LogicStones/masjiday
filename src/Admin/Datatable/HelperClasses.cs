using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;

namespace Admin.Datatable
{
	public class Message
	{
		public static string ErrorMessage = "Something went wrong. Please try again.";
		public bool Success = true;
		public string Action { get; set; }
		public bool Info { get; set; }
		public bool Warning { get; set; }
		public string MessageDetail { get; set; }
		public int ID { get; set; }

		public static string SaveMessage = "Record has been saved";
		public static string UpdateMessage = "Record has been updated";
	}
	public class Paging
	{
		public int Draw { get; set; }
		public int DisplayStart { get; set; }
		public int DisplayLength { get; set; }
		public int SortColumn { get; set; }
		public string SortOrder { get; set; }
		public string Search { get; set; }
		public string SearchJson { get; set; }
	}
	public class CallBackData
	{
		public Message msg = new Message();
		public JqueryDataTable Data = new JqueryDataTable();
	}
	public partial class CommonFilters
	{
		public string Search { get; set; }
		//public DateTime? SubscriptionDate { get; set; }
		//public DateTime? ExpiryDate { get; set; }
		//public Guid CompanyID { get; set; }
	}
	public class JqueryDataTable
	{
		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public dynamic data { get; set; }
	}
	public class ReturnData
	{
		public Message msg = new Message();
		public dynamic Data { get; set; }

	}
	public class SystemUser
	{
		public int Total { get; set; }
		public Int64 RowNo { get; set; }
		public int Id { get; set; }
		public bool IsActive { get; set; }
		public string FullName { get; set; }
		public string Company { get; set; }
		public string Email { get; set; }
	}
	public static class Extension
	{
		public static DataTable ToDataTable<T>(List<T> items)
		{
			DataTable dataTable = new DataTable(typeof(T).Name);

			//Get all the properties
			PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo prop in Props)
			{
				//Defining type of data column gives proper data table 
				var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
				//Setting column names as Property names
				dataTable.Columns.Add(prop.Name, type);
			}
			foreach (T item in items)
			{
				var values = new object[Props.Length];
				for (int i = 0; i < Props.Length; i++)
				{
					//inserting property values to datatable rows
					values[i] = Props[i].GetValue(item, null);
				}
				dataTable.Rows.Add(values);
			}
			//put a breakpoint here and check datatable
			return dataTable;
		}

		public static CallBackData ToDataTable<T>(this List<T> list, Paging paging)
		{
			CallBackData callBackData = new CallBackData();
			callBackData.Data.data = list;
			callBackData.Data.draw = paging.Draw;
			callBackData.Data.recordsTotal = callBackData.Data.data.Count;
			callBackData.Data.recordsFiltered = (callBackData.Data.data.Count != 0 ? ((int)callBackData.Data.data[0].GetType().GetProperty("Total").GetValue(callBackData.Data.data[0], null)) : 0);

			return callBackData;
		}
		public static T Deserialize<T>(this string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
		public static Paging FetchPaging(this HttpRequestBase request)
		{
			Paging paging = new Paging();
			try
			{
				paging.Draw = Convert.ToInt32(request.QueryString["sEcho"]);
				paging.SearchJson = Convert.ToString(request.QueryString["SearchJson"]);
				paging.DisplayLength = Convert.ToInt32(request.QueryString["iDisplayLength"]);
				paging.DisplayStart = Convert.ToInt32(request.QueryString["iDisplayStart"]);
				paging.SortColumn = Convert.ToInt32(request.QueryString["iSortCol_0"]);
				paging.Search = Convert.ToString(request.QueryString["sSearch"]);
				paging.SortOrder = Convert.ToString(request.QueryString["sSortDir_0"]);
				if (paging.DisplayLength == -1)
				{
					paging.DisplayLength = 10000000;
				}

			}
			catch { }

			return paging;
		}

		public static List<T> ConvertDataTable<T>(DataTable dt)
		{
			List<T> data = new List<T>();
			foreach (DataRow row in dt.Rows)
			{
				T item = GetItem<T>(row);
				data.Add(item);
			}
			return data;
		}
		public static T GetItem<T>(DataRow dr)
		{
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();

			foreach (DataColumn column in dr.Table.Columns)
			{
				foreach (PropertyInfo pro in temp.GetProperties())
				{
					if (pro.Name == column.ColumnName)
						pro.SetValue(obj, pro.PropertyType.Name == "String" ? Convert.ToString(dr[column.ColumnName]) : dr[column.ColumnName], null);
					else
						continue;
				}
			}
			return obj;
		}
	}
}