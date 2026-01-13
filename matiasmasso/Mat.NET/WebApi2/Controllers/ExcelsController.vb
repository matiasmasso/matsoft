Imports System.Web.Http
Public Class ExcelsController
    Inherits _BaseController

    '<HttpGet>
    '<Route("api/excel/{cod}/{guid}")>
    'Function Index(cod As Integer, guid As Guid) As MatHelper.Excel.Sheet
    '    Dim retval As MatHelper.Excel.Sheet = Nothing
    '    Select Case cod
    '        Case MatHelper.Excel.Book.UrlCods.CustomerDeliveries
    '            Dim oCustomer As New DTOCustomer(guid)
    '            retval = BEBL.Deliveries.ExcelHistoric(oCustomer)
    '    End Select
    '    Return retval
    'End Function

    <HttpGet>
    <Route("api/excel/sellout/{data}")>
    Function sellout(data As DTOSellOut) As MatHelper.Excel.Sheet
        Dim retval As MatHelper.Excel.Sheet = BEBL.SellOut.Excel(data)
        Return retval
    End Function

End Class
