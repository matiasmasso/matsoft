Imports System.Runtime.Serialization

<DataContract>
Public Class DTOEdiversaDesadv
    Inherits DTOBaseGuid

    Property Bgm As String 'num de document
    Property FchDoc As Date
    Property FchShip As Date
    Property Rff As String

    Property NadBy As DTOEan
    Property NadSu As DTOEan
    Property NadDp As DTOEan

    Property Proveidor As DTOProveidor
    Property Entrega As DTOContact
    Property PurchaseOrder As DTOPurchaseOrder

    Property Items As List(Of Item)

    Property Exceptions As List(Of DTOEdiversaException)

    Public Sub New(oEdiFile As DTOEdiversaFile)
        MyBase.New(oEdiFile.Guid)
        _Items = New List(Of Item)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Class Item
        Property Parent As DTOEdiversaDesadv
        Property Lin As Integer
        Property Ean As DTOEan
        Property Ref As String
        Property Dsc As String
        Property Qty As Integer
        Property Sku As DTOProductSku
    End Class

    Shared Function GetFullDocNom(value As DTOEdiversaDesadv) As String
        Dim sb As New Text.StringBuilder
        If value.Bgm > "" Then
            sb.Append(value.Bgm & " ")
            If value.FchDoc <> Nothing Then
                sb.Append(" del ")
            End If
        End If
        If value.FchDoc <> Nothing Then
            sb.Append(value.FchDoc.ToShortDateString)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function GetPdcText(value As DTOEdiversaDesadv) As String
        Dim retval As String = ""
        If value.PurchaseOrder IsNot Nothing Then
            retval = String.Format("{0} del {1:dd/MM/yy}: {2}", value.PurchaseOrder.num, value.PurchaseOrder.fch, value.PurchaseOrder.concept)
        End If
        Return retval
    End Function

    Shared Function GetProveidorNom(value As DTOEdiversaDesadv) As String
        Dim retval As String = ""
        If value.Proveidor IsNot Nothing Then
            retval = value.Proveidor.FullNom
        End If
        Return retval
    End Function

    Shared Function GetEntregaNom(value As DTOEdiversaDesadv) As String
        Dim retval As String = ""
        If value.Entrega IsNot Nothing Then
            retval = value.Entrega.FullNom
        End If
        Return retval
    End Function


    Shared Function GetFchShip(value As DTOEdiversaDesadv) As String
        Dim retval As String = ""
        If value.FchShip <> Nothing Then
            retval = value.FchDoc.ToShortDateString
        End If
        Return retval
    End Function
End Class
