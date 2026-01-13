Public Class Frm_EDiversaExceptions
    Private _src As Object
    Private _values As List(Of DTOEdiversaException)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oSrc As Object)
        MyBase.New()
        Me.InitializeComponent()
        _src = oSrc
    End Sub

    Private Async Sub Frm_EDiversaExceptions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub Xl_EDiversaExceptions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EdiversaExceptions1.RequestToRefresh
        Dim oExceptions As New List(Of DTOEdiversaException)
        If TypeOf e.Argument Is DTOEdiversaOrder Then
            oExceptions = DirectCast(e.Argument, DTOEdiversaOrder).Exceptions
        ElseIf TypeOf e.Argument Is DTOEdiversaOrderItem Then
            oExceptions = DirectCast(e.Argument, DTOEdiversaOrderItem).Exceptions
        End If

        RaiseEvent AfterUpdate(Me, e)

        If oExceptions.Count = 0 Then
            Me.Close()
        Else
            Await refresca()
        End If
    End Sub

    Private Async Function refresca() As Task
        If TypeOf _src Is DTOEdiversaOrder Then
            Dim oOrder As DTOEdiversaOrder = _src
            Dim sCustomerFullNom As String = ""
            If oOrder.Customer Is Nothing Then
                sCustomerFullNom = "(client " & oOrder.CompradorEAN.Value & " no registrat)"
            Else
                sCustomerFullNom = oOrder.Customer.FullNom
            End If

            LabelSrc.Text = "font: " & vbCrLf & OrderCaption(oOrder)
            Await Xl_EdiversaExceptions1.Load(oOrder.Exceptions)
        ElseIf TypeOf _src Is DTOEdiversaOrderItem Then
            Dim oOrderItem As DTOEdiversaOrderItem = _src
            Dim sCustomerFullNom As String = ""
            If oOrderItem.Parent.Customer Is Nothing Then
                sCustomerFullNom = "(client " & oOrderItem.Parent.CompradorEAN.Value & " no registrat)"
            Else
                sCustomerFullNom = oOrderItem.Parent.Customer.FullNom
            End If
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("font:")
            sb.AppendLine(OrderCaption(oOrderItem.Parent))
            sb.AppendLine(OrderItem(oOrderItem))
            sb.AppendLine("preu: " & DTOAmt.CurFormatted(oOrderItem.Preu) & IIf(oOrderItem.Dto = 0, "", " descompte: " & oOrderItem.Dto & "%"))
            LabelSrc.Text = sb.ToString
            Await Xl_EdiversaExceptions1.Load(oOrderItem.Exceptions)
        ElseIf TypeOf _src Is List(Of DTOEdiversaException) Then
            Await Xl_EdiversaExceptions1.Load(_src)
        End If
    End Function

    Private Function OrderCaption(oOrder As DTOEdiversaOrder) As String
        Dim retval As String = oOrder.FchDoc.ToShortDateString & " Comanda " & oOrder.DocNum & " de " & CustomerNom(oOrder)
        Return retval
    End Function

    Private Function CustomerNom(oOrder As DTOEdiversaOrder) As String
        Dim retval As String = ""
        If oOrder.Customer Is Nothing Then
            retval = "(client " & oOrder.CompradorEAN.Value & " no registrat)"
        Else
            retval = oOrder.Customer.FullNom
        End If
        Return retval
    End Function

    Private Function OrderItem(item As DTOEdiversaOrderItem) As String
        Dim retval As String = ""
        If item.Sku Is Nothing Then
            retval = String.Format("{0} x {1} / {2} / {3}", item.Qty, item.Ean.Value, item.RefClient.Trim, item.RefProveidor.Trim)
        Else
            retval = String.Format("{0} x {1}", item.Qty, item.Sku.RefYNomLlarg.Tradueix(Current.Session.Lang))
        End If
        Return retval
    End Function


End Class