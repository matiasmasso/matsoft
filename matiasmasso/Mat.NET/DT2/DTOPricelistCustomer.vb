Public Class DTOPricelistCustomer
    Inherits DTOBaseGuid

    Property Fch As Date
    Property FchEnd As Date
    Property Concepte As String
    Property Cur As DTOCur
    Property Customer As DTOCustomer
    Property Items As List(Of DTOPricelistItemCustomer)

    Public Sub New()
        MyBase.New()
        _Fch = Today
        _Concepte = "(nova tarifa de preus de venda)"
        _Cur = DTOCur.Eur
        _Items = New List(Of DTOPricelistItemCustomer)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function FullNom(value As DTOPricelistCustomer, Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.current.lang
        Dim retval As String = String.Format("{0} {1} {2} {3:dd/MM/yy}", oLang.tradueix("Tarifa", "Tarifa", "Price list"), value.Concepte, oLang.tradueix("del", "del", "from"), value.Fch)
        Return retval
    End Function

    Shared Function ExcelSheet(value As DTOPricelistCustomer) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet
        retval.AddRowWithCells("marca", "categoría", "producto", "PVP")
        AddExcelRows(retval, value.Items)
        Return retval
    End Function

    Shared Function ExcelSheet(values As List(Of DTOPricelistCustomer)) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet
        retval.AddRowWithCells("marca", "categoría", "producto", "tarifa", "tarifa B", "PVP")
        For Each value As DTOPricelistCustomer In values
            AddExcelRows(retval, value.Items)
        Next
        Return retval
    End Function

    Shared Sub AddExcelRows(ByRef oSheet As MatHelperStd.ExcelHelper.Sheet, values As List(Of DTOPricelistItemCustomer))
        For Each Item As DTOPricelistItemCustomer In values
            Dim sNoms() As String = DTOProduct.GetNom(Item.Sku).Split("/")
            Dim oRow As New MatHelperStd.ExcelHelper.Row(oSheet)
            oRow.AddCell(Item.Sku.Guid.ToString())
            oRow.AddCell(sNoms(0))
            If sNoms.Count > 1 Then
                oRow.AddCell(sNoms(1))
                If sNoms.Count > 2 Then
                    oRow.AddCell(sNoms(2))
                Else
                    oRow.AddCell()

                End If
            Else
                oRow.AddCell()
                oRow.AddCell()

            End If
            oRow.AddCell(Item.Retail.val)
            oSheet.Rows.Add(oRow)
        Next
    End Sub

End Class

Public Class DTOPricelistItemCustomer
    Property Parent As DTOPricelistCustomer
    Property Sku As DTOProductSku
    Property Retail As DTOAmt

    Property BrandNom As String
    Property CategoryNom As String
    Property ProductNom As String

    Public Sub New(oPriceList As DTOPricelistCustomer)
        MyBase.New()
        _Parent = oPriceList
    End Sub

    Public Function Clon(oParent As DTOPricelistCustomer) As DTOPricelistItemCustomer
        Dim retval As New DTOPricelistItemCustomer(oParent)
        With retval
            .Sku = _Sku
            .Retail = _Retail
            .BrandNom = _BrandNom
            .CategoryNom = _CategoryNom
            .ProductNom = _ProductNom
        End With
        Return retval
    End Function
End Class
