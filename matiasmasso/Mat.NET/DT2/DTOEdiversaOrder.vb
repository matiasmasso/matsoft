Public Class DTOEdiversaOrder

    Inherits DTOBaseGuid
    Public Property DocNum As String
    Public Property FchDoc As Date
    Public Property FchDeliveryMin As Date
    Public Property FchDeliveryMax As Date
    Public Property Tipo As String
    Public Property Funcion As Funcions
    Public Property Cur As DTOCur
    Public Property Obs As String
    Public Property Proveedor As DTOContact
    Public Property Customer As DTOCustomer
    Public Property Centro As String
    Public Property Departamento As String
    Public Property NumProveidor As String

    Public Property Comprador As DTOContact
    Public Property CompradorEAN As DTOEan
    Public Property FacturarA As DTOCustomer 'a qui es factura
    Public Property FacturarAEAN As DTOEan
    Public Property ReceptorMercancia As DTOContact
    Public Property ReceptorMercanciaEAN As DTOEan
    Public Property Amt As DTOAmt
    Public Property Result As DTOPurchaseOrder

    Public Property EdiversaFile As DTOEdiversaFile
    Public Property Items As List(Of DTOEdiversaOrderItem)
    Public Property Exceptions As List(Of DTOEdiversaException)

    Public Enum Funcions
        NotSet
    End Enum


    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOEdiversaOrderItem)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTOEdiversaOrderItem)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Function isDuplicate() As Boolean
        Dim retval As Boolean = _Exceptions.Any(Function(x) x.Cod = DTOEdiversaException.Cods.DuplicatedOrder)
        Return retval
    End Function

    Public Sub AddException(oCod As DTOEdiversaException.Cods, sMsg As String, Optional oTagCod As DTOEdiversaException.TagCods = DTOEdiversaException.TagCods.NotSet, Optional oTag As DTOBaseGuid = Nothing)
        Dim oException =DTOEdiversaException.Factory(oCod, oTag, sMsg)
        oException.TagCod = oTagCod
        _Exceptions.Add(oException)
    End Sub

    Public Function Report() As String
        Dim sb As New System.Text.StringBuilder
        For Each ex As DTOEdiversaException In _Exceptions
            sb.AppendLine(ex.Msg)
        Next
        For Each ex In _Items.SelectMany(Function(x) x.Exceptions)
            sb.AppendLine(ex.Msg)
        Next
        Return sb.ToString
    End Function

    Public Sub RestoreTagsToOriginalObjects()
        For Each ex In _Exceptions
            Select Case ex.TagCod
                Case DTOEdiversaException.TagCods.EdiversaOrder
                    ex.Tag = Me
            End Select
        Next
        For Each item In _Items
            item.Parent = Me
            For Each ex In item.Exceptions
                Select Case ex.TagCod
                    Case DTOEdiversaException.TagCods.EdiversaOrder
                        ex.Tag = Me
                    Case DTOEdiversaException.TagCods.EdiversaOrderItem
                        ex.Tag = item
                End Select
            Next
        Next
    End Sub


End Class


Public Class DTOEdiversaOrderItem
    Inherits DTOBaseGuid

    Public Property Parent As DTOEdiversaOrder
    Public Property Lin As Integer
    Public Property Ean As DTOEan
    Public Property RefProveidor As String
    Public Property RefClient As String
    Public Property Dsc As String
    Public Property Sku As DTOProductSku
    Public Property Qty As Integer
    Public Property Preu As DTOAmt

    Public Property PreuNet As DTOAmt
    Public Property Dto As Decimal
    Public Property SkipPreuValidationUser As DTOUser
    Public Property SkipPreuValidationFch As DateTime
    Public Property SkipDtoValidationUser As DTOUser
    Public Property SkipDtoValidationFch As DateTime
    Public Property SkipItemUser As DTOUser
    Public Property SkipItemFch As DateTime
    Public Property Exceptions As List(Of DTOEdiversaException)

    Public Sub New()
        MyBase.New()
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Sub AddException(oCod As DTOEdiversaException.Cods, sMsg As String, Optional oTagCod As DTOEdiversaException.TagCods = DTOEdiversaException.TagCods.NotSet, Optional oTag As DTOBaseGuid = Nothing)
        Dim oException =DTOEdiversaException.Factory(oCod, oTag, sMsg)
        oException.TagCod = oTagCod
        _Exceptions.Add(oException)
    End Sub
End Class



