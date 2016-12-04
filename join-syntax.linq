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

var query = TblBatteryInfo
   .Join(
	  TblProducts,
	  mbi => mbi.ProductID,
	  prod => (Int32?)(prod.Product_id),
	  (mbi, prod) =>
		 new
		 {
			 mbi = mbi,
			 prod = prod
		 }
   )
   .Join(
	  TblSubCategories,
	  temp0 => temp0.prod.SubCategory_id,
	  subc => subc.SubCategory_id,
	  (temp0, subc) =>
		 new
		 {
			 temp0 = temp0,
			 subc = subc
		 }
   )
   .Join(
	  TblBatteryInfo,
	  temp1 => temp1.temp0.mbi.BatteryInfoID,
	  bi => bi.BatteryInfoID,
	  (temp1, bi) => temp1.temp0.mbi.BebatID);

query.Dump();
/*
var query = from mbi in TblBatteryInfo
			join prod in TblProducts on mbi.ProductID equals prod.Product_id
			join subc in TblSubCategories on prod.SubCategory_id equals subc.SubCategory_id
			join bi in TblBatteryInfo on mbi.BatteryInfoID equals bi.BatteryInfoID
			select mbi.BebatID;

query.Dump();

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