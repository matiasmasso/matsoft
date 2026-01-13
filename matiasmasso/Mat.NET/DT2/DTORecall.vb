Public Class DTORecall
    Inherits DTOBaseGuid

    Property Fch As Date
    Property Nom As String
    Property Clis As List(Of DTORecallCli)

    Public Enum Wellknowns
        Notset
        DualfixR44
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Wellknown(id As DTORecall.Wellknowns) As DTORecall
        Dim retval As DTORecall = Nothing
        Select Case id
            Case DTORecall.Wellknowns.DualfixR44
                retval = New DTORecall(New Guid("9C76E9AF-E0BF-41BD-9601-3C047A519770"))
        End Select
        Return retval
    End Function

    Shared Function Excel(oRecall As DTORecall) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("Recall", "Recall")
        With retval
            .AddColumn("Customer", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Sku code", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Product", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Serial number", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
        End With

        For Each cli In oRecall.Clis
            For Each product In cli.Products
                Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow()
                oRow.AddCell(cli.Customer.FullNom)
                If product.Sku Is Nothing Then
                    oRow.AddCell()
                    oRow.AddCell()
                Else
                    oRow.AddCell(product.Sku.refProveidor)
                    oRow.AddCell(product.Sku.nomProveidor)
                End If
                oRow.AddCell(product.SerialNumber)
            Next
        Next
        Return retval
    End Function

End Class

Public Class DTORecallCli
    Inherits DTOBaseGuid

    Property Recall As DTORecall
    Property ContactNom As String
    Property ContactTel As String
    Property ContactEmail As String
    Property Customer As DTOCustomer
    Property Address As String
    Property Zip As String
    Property Location As String
    Property Country As DTOCountry
    Property FchVivace As Date

    Property RegMuelle As String

    Property PurchaseOrder As DTOPurchaseOrder
    Property Delivery As DTODelivery

    Property Products As List(Of DTORecallProduct)

    Property UsrLog As DTOUsrLog

    Public Sub New()
        MyBase.New()
        _UsrLog = New DTOUsrLog
        _Products = New List(Of DTORecallProduct)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _UsrLog = New DTOUsrLog
        _Products = New List(Of DTORecallProduct)
    End Sub

    Public Function Bultos() As Integer
        Return _Products.Count
    End Function

    Shared Function RemiteLocation(value As DTORecallCli) As String
        Dim sb As New Text.StringBuilder
        If value.Zip > "" Then
            sb.AppendFormat("{0} ", value.Zip)
        End If
        If value.Location > "" Then
            sb.AppendFormat("{0} ", value.Location)
        End If
        If value.Country IsNot Nothing Then
            If value.Country.UnEquals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)) Then
                sb.AppendFormat("({0})", value.Country.LangNom.Esp)
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

End Class

Public Class DTORecallProduct
    Property Sku As DTOProductSku
    Property SerialNumber As String
End Class
