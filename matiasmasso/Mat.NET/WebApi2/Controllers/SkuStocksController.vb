Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SkuStocksController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SkuStocks/test")>
    Public Function Test() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.mamaRoo)
            BEBL.ProductSku.Load(oSku)
            Dim oCatalog As New DTOCatalog
            Dim oBrand As New DTOCatalog.Brand(oSku.category.brand.Guid, oSku.category.brand.nom.Esp)
            oCatalog.Add(oBrand)
            Dim oCategory As New DTOCatalog.Category(oSku.category.Guid, oSku.category.nom.Esp)
            oBrand.categories.Add(oCategory)
            Dim pSku As New DTOCatalog.Sku(oSku.Guid, oSku.nom.Esp)
            pSku.stock = 23
            pSku.id = 21212
            oCategory.skus.Add(pSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, oCatalog)

            'retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els stocks")

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els stocks")
        End Try
        Return retval
    End Function

    'used on mailing for Csv
    <HttpGet>
    <Route("api/SkuStocks/ForWeb/{user}")>
    Public Function ForWeb(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try

            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                oUser.Emp = MyBase.GetEmp(oUser.Emp.Id) 'to get Mgz
                Dim exs As New List(Of Exception)
                Dim value As DTOProductCatalog = BEBL.SkuStocks.ForWeb(oUser, exs)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els stocks")
                End If
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els stocks")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/SkuStocks/ForWeb2/{user}")>
    Public Function ForWeb2(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try

            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                oUser.Emp = MyBase.GetEmp(oUser.Emp.Id) 'to get Mgz
                Dim exs As New List(Of Exception)
                Dim value As DTOProductCatalog = BEBL.SkuStocks.ForWeb(oUser, exs)
                Dim oCatalog = DTOCatalog.Factory(value.brands, oUser.lang)

                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, oCatalog)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els stocks")
                End If
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els stocks")
        End Try
        Return retval
    End Function

    'web stocks x proveidor
    <HttpGet>
    <Route("api/SkuStocks/Excel/{user}")>
    Public Function Excel(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try

            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                oUser.Emp = MyBase.GetEmp(oUser.Emp.Id) 'to get Mgz
                Dim exs As New List(Of Exception)
                Dim oCatalog As DTOProductCatalog = BEBL.SkuStocks.ForWeb(oUser, exs)
                If exs.Count = 0 Then
                    Dim oSheet As MatHelper.Excel.Sheet = DTOProductCatalog.ExcelStocks(oCatalog, oUser.Rol)
                    Dim filename = "M+O Stocks.xlsx"
                    retval = MyBase.HttpExcelResponseMessage(oSheet, filename)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els stocks")
                End If
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els stocks")
        End Try
        Return retval
    End Function


    'web stocks x Britax
    <HttpGet>
    <Route("api/SkuStocks/StockMovements/{user}/{year}")>
    Public Function StockMovements(user As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = GetEmp(DTOEmp.Ids.MatiasMasso)
            Dim oUser = BEBL.User.Find(user)
            Dim oSheet = BEBL.SkuStocks.StockMovementsExcelSheet(oEmp.Mgz, oUser, year)
            Dim filename = String.Format("M+O Stock movement {0}.xlsx", year)
            retval = MyBase.HttpExcelResponseMessage(oSheet, filename)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error on reading stock movement")
        End Try
        Return retval
    End Function
End Class
