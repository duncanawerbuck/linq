<Query Kind="Statements">
  <Connection>
    <ID>49956faa-1cbc-48e7-97e5-f75e145611cb</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <Database>WEEE</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Output>DataGrids</Output>
</Query>

var query = from mbi in TblMonthlyBatteryInputs
			join prod in TblProducts on mbi.Product_ID equals prod.Product_id
			join subc in TblSubCategories on prod.SubCategory_id equals subc.SubCategory_id
			join bi in TblBatteryInfo on mbi.BatteryInfoID equals bi.BatteryInfoID
			select new { MonthlyBatteryInput = mbi, Product = prod, SubCategory = subc, BatteryInfo = bi };

var reportViewModel = from q in query
					  select new
					  {
						  ProducerRegistrationNumber = q.MonthlyBatteryInput.RegistrationNo,
						  MonthId = q.MonthlyBatteryInput.TblMonthEnd.DateIndex,
						  BatteryInfoID = q.MonthlyBatteryInput.BatteryInfoID,
						  BatteryRef = q.BatteryInfo.BebatID,
						  IecCode = q.BatteryInfo.IEC,
						  ProductName = q.Product.Name,
						  ProductDescription = q.Product.Description,
						  SubCategoryName = q.SubCategory.Name,
						  Quantity = q.MonthlyBatteryInput.Quantity,
						  Weight = q.MonthlyBatteryInput.Weight,
						  ProductId = q.MonthlyBatteryInput.Product_ID,
					  };

reportViewModel.Dump();

/*
SELECT mbi.BatteryInfoID
	,bi.BebatID as 'BatteryRef'
	,bi.IEC as 'IecCode'
	,prod.NAME as 'ProductName'
	,prod.[Description] as 'ProductDescription'
	,subc.NAME as 'SubCategoryName'
	,mbi.Quantity
	,mbi.[weight]
	,mbi.Product_ID
FROM tblMonthlyBatteryInput mbi
JOIN tblproduct prod ON prod.Product_id = mbi.Product_ID
JOIN TblSubCategory subc ON subc.SubCategory_id = prod.SubCategory_id
JOIN tblBatteryInfo bi ON bi.BatteryInfoID = mbi.BatteryInfoID
WHERE RegistrationNo = 1
	AND DateIndex = 119
*/